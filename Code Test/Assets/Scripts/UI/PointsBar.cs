using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class PointsBar : MonoBehaviour
{
    [SerializeField] private float barIncreaseSpeed = 100f;

    private Slider pointSlider = null;

    private Coroutine increasePointsRoutine = null;
    

    private void Awake()
    {
        pointSlider = GetComponent<Slider>();
        if (pointSlider == null)
        {
            Debug.LogError($"PointsBar: No Slider component found on {gameObject.name}.");
            enabled = false;
            return;
        }
    }

    // Starts coroutine that moves the bar from the current score to the new score. Is called from the UIManager when the Score Menu has been loaded.
    //
    public void UpdatePointsBar(int newScore)
    {
        increasePointsRoutine = StartCoroutine(IncreasePointsRoutine(newScore));
    }

    public void ResetPointsBar()
    {
        pointSlider.value = 0;
    }

    private IEnumerator IncreasePointsRoutine(int newScore)
    {
        while (pointSlider.value < newScore)
        {
            pointSlider.value = Mathf.MoveTowards(pointSlider.value, newScore, barIncreaseSpeed * Time.deltaTime);

            yield return null;
        }

        pointSlider.value = newScore;
    }
}
