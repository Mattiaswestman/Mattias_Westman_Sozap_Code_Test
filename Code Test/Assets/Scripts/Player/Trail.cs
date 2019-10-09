using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [SerializeField] private TrailRenderer myTrailRenderer = null;
    [SerializeField] private EdgeCollider2D myEdgeCollider2D = null;
    [SerializeField] private Transform trailOrigin = null;

    [SerializeField] private float drawTime = 0f;
    [SerializeField][Range(0f, 1f)] private float maxDrawTimeDeviation = 0f;
    [SerializeField] private float pauseTime = 0f;
    
    private bool isDrawing = false;
    private bool isPaused = false;

    
    private void Awake()
    {
        if(myTrailRenderer == null)
        {
            Debug.LogError($"Trail: No reference to TrailRenderer component set on {gameObject.name}.");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        myTrailRenderer.emitting = false;
    }

    private void FixedUpdate()
    {
        if(!isPaused && !isDrawing)
        {
            StartCoroutine("DrawTrailRoutine");
        }
    }
    
    IEnumerator DrawTrailRoutine()
    {
        isDrawing = true;

        var totalDrawTime = drawTime + (Random.Range(-maxDrawTimeDeviation, maxDrawTimeDeviation));
        var timer = 0f;
        
        while(timer < totalDrawTime)
        {
            myTrailRenderer.emitting = true;

            /*
            Vector3[] positions = new Vector3[myTrailRenderer.positionCount];
            Vector2[] positions2D = new Vector2[myTrailRenderer.positionCount];

            myTrailRenderer.GetPositions(positions);

            for(int i = 0; i < positions.Length; i++)
            {
                positions2D[i] = positions[i];
            }

            myEdgeCollider2D.points = positions2D;
            */
            timer += Time.deltaTime;
            yield return null;
        }

        StartCoroutine("PauseTrailRoutine");
    }

    IEnumerator PauseTrailRoutine()
    {
        isPaused = true;
        isDrawing = false;

        myTrailRenderer.emitting = false;

        yield return new WaitForSeconds(pauseTime);

        isPaused = false;
    }
}
