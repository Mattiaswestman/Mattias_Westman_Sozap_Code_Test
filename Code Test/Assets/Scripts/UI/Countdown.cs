﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] private Animator countdownAnimator = null;
    [SerializeField] private Animation[] countdownAnimations = null;

    private Coroutine countdownRoutine = null;

    public void StartCountdown(int roundNumber, float duration)
    {
        countdownRoutine = StartCoroutine(CountdownRoutine(roundNumber, duration));
    }

    private IEnumerator CountdownRoutine(int roundNumber, float duration)
    {
        string text = default;

        if(roundNumber == 1)
        {
            text = "First to 150p Wins";
        }
        else
        {
            text = $"Round {roundNumber}";
        }

        float timer = duration;

        while(timer >= 0f)
        {
            


            timer -= Time.deltaTime;
            yield return null;
        }

        GameManager.instance.HasCountdownFinished = true;
    }
}
