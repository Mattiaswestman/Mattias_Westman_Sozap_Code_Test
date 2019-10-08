using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] private Transform trailStartTransform = null;

    [SerializeField] private float drawForSeconds = 0f;
    [SerializeField] private float pauseForSeconds = 0f;

    private bool isPaused = false;


    private void Update()
    {
        StartCoroutine("DrawSplineRoutine");
    }

    IEnumerator DrawSplineRoutine()
    {

        yield return new WaitForSeconds(drawForSeconds);

        StartCoroutine("PauseSplineRoutine");
    }

    IEnumerator PauseSplineRoutine()
    {
        isPaused = true;

        yield return new WaitForSeconds(pauseForSeconds);

        isPaused = false;
    }
}
