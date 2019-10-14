using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] private Animator countdownAnimator = null;
    [SerializeField] private Animation[] countdownAnimations = null;

    private Coroutine countdownRoutine = null;

    private bool isCountdownOver = false;

    public void UpdateCountdownText()
    {

    }

    public void StartCountdown(int roundNumber, float duration)
    {
        countdownRoutine = StartCoroutine(CountdownRoutine(roundNumber, duration));
    }

    private IEnumerator CountdownRoutine(int roundNumber, float duration)
    {
        float timer = duration;

        while(timer >= 0f)
        {

            timer -= Time.deltaTime;
            yield return null;
        }

        //gameCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        isCountdownOver = true;
    }
}
