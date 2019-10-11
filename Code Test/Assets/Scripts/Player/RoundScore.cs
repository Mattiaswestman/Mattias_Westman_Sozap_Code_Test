using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundScore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshPro scoreText = null;

    private int currentRoundScore = 0;
    public int CurrentRoundScore { get { return currentRoundScore; } }

    public void UpdateRoundScore()
    {
        currentRoundScore += 10;
        UIManager.instance.SetTextComponentToInt(scoreText, currentRoundScore);
    }

    public void ResetRoundScore()
    {
        currentRoundScore = 0;
        UIManager.instance.SetTextComponentToInt(scoreText, 0);
    }
}
