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
    
    private bool isDrawing = false;
    public bool IsDrawing { set { isDrawing = value; } }
    private bool isPaused = true;
    public bool IsPaused { set { isPaused = value; } }

    private Coroutine drawRoutine = null;
    private Coroutine pauseRoutine = null;


    private void Awake()
    {
        myHealth = GetComponent<Health>();
        if (myHealth == null)
        {
            Debug.LogError($"Trail: No Health component found on {gameObject.name}.");
            enabled = false;
            return;
        }
    }
    
    private void FixedUpdate()
    {
        if (!isPaused && !isDrawing && myHealth.IsAlive)
        {
            drawRoutine = StartCoroutine(DrawTrailRoutine());
        }
    }
    
    // Initializes and returns a new Trail object.
    //
    private GameObject CreateTrailObject()
    {
        GameObject trail = new GameObject("Trail", typeof(LineRenderer), typeof(EdgeCollider2D));
        trail.tag = "PlayerTrail";
        trail.transform.SetParent(trailSceneParent);

        LineRenderer trailRenderer = trail.GetComponent<LineRenderer>();

        trailRenderer.startWidth = trailRenderer.endWidth = lineWidth;
        trailRenderer.startColor = trailRenderer.endColor = lineColor;
        trailRenderer.sharedMaterial = trailMaterial;

        trailRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        trailRenderer.receiveShadows = false;

        EdgeCollider2D trailCollider = trail.GetComponent<EdgeCollider2D>();

        trailCollider.isTrigger = true;
        trailCollider.edgeRadius = lineWidth / 2;

        return trail;
    }

    // Creates a trail object consisting of a LineRenderer and an EdgeCollider2D, that both is feed positions the player passes over.
    //
    private IEnumerator DrawTrailRoutine()
    {
        isDrawing = true;

        // Set the time this trail should be drawn.
        var totalDrawTime = drawTime + (Random.Range(-maxDrawTimeDeviation, maxDrawTimeDeviation));
        var timer = 0f;

        GameObject trail = CreateTrailObject();

        // Create a list that holds the positions this trail is built with.
        var points = new List<Vector2>();

        // Draw the trail until total draw time has been reached.
        while (timer < totalDrawTime)
        {
            // Start adding player positions to points list.
            points.Add(trailOrigin.position);

            // Add the current player position to the LineRenderer.
            trail.GetComponent<LineRenderer>().positionCount = points.Count;
            trail.GetComponent<LineRenderer>().SetPosition(trail.GetComponent<LineRenderer>().positionCount - 1, trailOrigin.position);

            // Update the collider with the points list.
            if (points.Count > 1)
            {
                trail.GetComponent<EdgeCollider2D>().points = points.ToArray();
            }

            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        
        pauseRoutine = StartCoroutine(PauseTrailRoutine());
    }

    // Pauses trail drawing for a given time.
    //
    private IEnumerator PauseTrailRoutine()
    {
        isPaused = true;
        isDrawing = false;
        
        yield return new WaitForSeconds(pauseTime);

        isPaused = false;
    }
}
