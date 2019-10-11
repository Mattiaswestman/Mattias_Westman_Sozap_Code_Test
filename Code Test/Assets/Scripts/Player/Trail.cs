using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform trailOrigin = null;
    [SerializeField] private Transform trailSceneParent = null;
    [SerializeField] private Material trailMaterial = null;

    [Header("Trail")]
    [SerializeField] private float lineWidth = 0f;
    [SerializeField] private Color lineColor = default;
    
    [Header("Time")]
    [SerializeField] private float drawTime = 0f;
    [SerializeField][Range(0f, 1f)] private float maxDrawTimeDeviation = 0f;
    [SerializeField] private float pauseTime = 0f;

    private Health myHealth = null;

    private Coroutine drawRoutine = null;
    private Coroutine pauseRoutine = null;

    private bool isDrawing = false;
    public bool IsDrawing { set { isDrawing = value; } }
    private bool isPaused = true;
    public bool IsPaused { set { isPaused = value; } }


    private void Awake()
    {
        myHealth = GetComponent<Health>();
        if(myHealth == null)
        {
            Debug.LogError($"Trail: No Health component found on {gameObject.name}.");
            enabled = false;
            return;
        }
    }
    
    private void FixedUpdate()
    {
        if(!isPaused && !isDrawing && myHealth.IsAlive)
        {
            drawRoutine = StartCoroutine(DrawTrailRoutine());
        }
    }
    
    private IEnumerator DrawTrailRoutine()
    {
        isDrawing = true;

        var totalDrawTime = drawTime + (Random.Range(-maxDrawTimeDeviation, maxDrawTimeDeviation));
        var timer = 0f;

        GameObject trail = new GameObject("Trail", typeof(LineRenderer), typeof(EdgeCollider2D));
        trail.tag = "PlayerTrail";
        trail.transform.SetParent(trailSceneParent);

        LineRenderer trailRenderer = null;
        try
        {
            trailRenderer = trail.GetComponent<LineRenderer>();

            trailRenderer.startWidth = trailRenderer.endWidth = lineWidth;
            trailRenderer.startColor = trailRenderer.endColor = lineColor;
            trailRenderer.sharedMaterial = trailMaterial;

            trailRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            trailRenderer.receiveShadows = false;
        }
        catch(MissingReferenceException exception)
        {
            Debug.LogError(exception);
            isDrawing = false;
            StopCoroutine(drawRoutine);
        }

        EdgeCollider2D trailCollider = null;
        try
        {
            trailCollider = trail.GetComponent<EdgeCollider2D>();

            trailCollider.isTrigger = true;
            trailCollider.edgeRadius = lineWidth / 2;
        }
        catch(MissingReferenceException exception)
        {
            Debug.LogError(exception);
            isDrawing = false;
            StopCoroutine(drawRoutine);
        }

        var points = new List<Vector2>();

        while(timer < totalDrawTime && isDrawing)
        {
            points.Add(trailOrigin.position);
            trailRenderer.positionCount = points.Count;
            trailRenderer.SetPosition(trailRenderer.positionCount - 1, trailOrigin.position);

            if(points.Count > 1)
            {
                trailCollider.points = points.ToArray();
            }

            timer += Time.deltaTime;
            yield return null;
        }

        if(!isPaused)
        {
            pauseRoutine = StartCoroutine(PauseTrailRoutine());
        }
    }

    private IEnumerator PauseTrailRoutine()
    {
        isPaused = true;
        isDrawing = false;
        
        yield return new WaitForSeconds(pauseTime);

        isPaused = false;
    }
}
