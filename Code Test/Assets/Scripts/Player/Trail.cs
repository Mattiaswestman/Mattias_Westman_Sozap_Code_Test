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

    private bool isDrawing = false;
    private bool isPaused = true;
    

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

        List<Vector2> points = new List<Vector2>();

        while(timer < totalDrawTime)
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
