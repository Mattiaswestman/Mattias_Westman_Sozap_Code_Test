using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailCreator : MonoBehaviour
{
    [SerializeField] private float drawTime = 0f;
    [SerializeField] [Range(0f, 1f)] private float maxDrawTimeDeviation = 0f;
    [SerializeField] private float pauseTime = 0f;

    private bool isDrawing = false;
    private bool isPaused = false;


    private void Update()
    {
        if(!isPaused && !isDrawing)
        {
            StartCoroutine("DrawTrailRoutine");
        }
    }

    IEnumerator DrawTrailRoutine()
    {
        isDrawing = true;

        // Create new trail. 
        // Start new trail.

        var drawTimeThisRoutine = drawTime + (Random.Range(-maxDrawTimeDeviation, maxDrawTimeDeviation));
        yield return new WaitForSeconds(drawTimeThisRoutine);

        // End trail.

        StartCoroutine("PauseTrailRoutine");
    }
    
    IEnumerator PauseTrailRoutine()
    {
        isPaused = true;
        isDrawing = false;

        yield return new WaitForSeconds(pauseTime);

        isPaused = false;
    }
}
