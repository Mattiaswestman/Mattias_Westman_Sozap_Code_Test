using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundScore : MonoBehaviour
{
    [Header("References")]
    public MeshRenderer scoreRenderer = null;
    [SerializeField] private TextMeshPro scoreText = null;

    private int currentRoundPoints = 0;
    public int CurrentRoundPoints { get { return currentRoundPoints; } }

    public void UpdateRoundScore()
    {
        currentRoundPoints += 10;
        UIManager.instance.SetTextComponentToInt(scoreText, currentRoundPoints);
    }

    public void ResetRoundScore()
    {
        currentRoundPoints = 0;
        UIManager.instance.SetTextComponentToInt(scoreText, 0);
    }
}
