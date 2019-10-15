using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundScore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MeshRenderer scoreRenderer = null;
    [SerializeField] private TextMeshPro scoreText = null;

    private int currentRoundScore = 0;
    public int CurrentRoundScore { get { return currentRoundScore; } }

    // Updates the score displayed above the player during gameplay. Is called by a fired projectile that has hit another player.
    //
    public void UpdateScore()
    {
        currentRoundScore += 10;
        UIManager.instance.SetTextComponentToInt(scoreText, currentRoundScore);
    }

    public void SetScoreVisibility(bool value)
    {
        scoreRenderer.enabled = value;
    }

    public void ResetScore()
    {
        currentRoundScore = 0;
        UIManager.instance.SetTextComponentToInt(scoreText, 0);
    }
}
