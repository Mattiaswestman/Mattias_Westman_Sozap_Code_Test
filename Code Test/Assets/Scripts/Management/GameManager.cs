using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Score score;

    private int playerCount = 2;
    private int currentRoundNumber = 1;

    private bool gameOver = false;


    private void Awake()
    {
        score = GetComponent<Score>();
    }

    private void Start()
    {
        UIManager.instance.SetTextAsInt(UIManager.instance.playerCountText, playerCount);
        // Disable and hide spaceships.
        UIManager.instance.UpdatePlayerUIVisibility(playerCount);
    }
    
    public void StartGame()
    {
        UIManager.instance.DisableCanvas(UIManager.instance.titleMenuCanvas);
        UIManager.instance.EnableCanvas(UIManager.instance.gameplayCanvas);

        StartRound(currentRoundNumber);
    }

    public void StartRound(int roundNumber)
    {
        // Reset player spawn.
        // UI Countdown.
        // Hide canvas.
        // Run game.
        // Score pause.
    }

    public void EndGame()
    {
        ResetGameValues();

        UIManager.instance.DisableCanvas(UIManager.instance.gameplayCanvas);
        UIManager.instance.EnableCanvas(UIManager.instance.titleMenuCanvas);
    }

    private void ResetGameValues()
    {
        currentRoundNumber = 1;
    }

    // Increases the number of players, and requests updates of the UI. Is called from a button on the main menu.
    //
    public void IncreasePlayerCount()
    {
        if(playerCount < 4)
        {
            playerCount++;

            UIManager.instance.SetTextAsInt(UIManager.instance.playerCountText, playerCount);
            // Disable and hide spaceships.
            UIManager.instance.UpdatePlayerUIVisibility(playerCount);
        }
    }

    // Decreases the number of players, and requests updates of the UI. Is called from a button on the main menu.
    //
    public void DecreasePlayerCount()
    {
        if(playerCount > 2)
        {
            playerCount--;
            
            UIManager.instance.SetTextAsInt(UIManager.instance.playerCountText, playerCount);
            // Disable and hide spaceships.
            UIManager.instance.UpdatePlayerUIVisibility(playerCount);
        }
    }
}
