using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsBar : MonoBehaviour
{
    [SerializeField] private float barIncreaseSpeed = 1f;

    private Slider pointSlider = null;

    private Coroutine increasePointsRoutine = null;
    

    private void Awake()
    {
        pointSlider = GetComponent<Slider>();
        if(pointSlider == null)
        {
            Debug.LogError($"PointsBar: No Slider component found on {gameObject.name}.");
            enabled = false;
            return;
        }
    }

    public void UpdatePointsBar(int newScore)
    {
        increasePointsRoutine = StartCoroutine(IncreasePointsRoutine(newScore));
    }

    private IEnumerator IncreasePointsRoutine(int newScore)
    {
        while(pointSlider.value < newScore)
        {
            pointSlider.value = Mathf.MoveTowards(pointSlider.value, newScore, barIncreaseSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
