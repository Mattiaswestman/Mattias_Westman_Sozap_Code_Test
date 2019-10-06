using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int playerCount = 2;
    private int currentRoundNumber = 1;

    private bool gameOver = false;

    
    // WIP: Starts a new game from the main menu. Is called from the Play button.
    //
    public void StartGame()
    {
        UIManager.instance.DisableCanvas(UIManager.instance.menuCanvas);

        UIManager.instance.EnableCanvas(UIManager.instance.gameCanvas);

        //StartRound(currentRoundNumber);
    }

    // Starts a new round with of the given round number. Is called when a new round should be initiated.
    //
    public void StartRound(int roundNumber)
    {
        // Reset player spawn.
        // UI Countdown.
        // Hide canvas.
        // Run game.
        // Score pause.
        


        StartCoroutine(PauseExecution(5f));

        UIManager.instance.EnableUIObject(UIManager.instance.scorePanel);
    }

    // Called from Menu button after game complete?
    //
    public void EndGame()
    {
        ResetGameValues();

        UIManager.instance.DisableCanvas(UIManager.instance.gameCanvas);

        UIManager.instance.EnableCanvas(UIManager.instance.menuCanvas);
    }

    private void ResetGameValues()
    {

    }

    // Increases the number of players, and requests updates of the UI. Is called from a button on the main menu.
    //
    public void IncreasePlayerCount()
    {
        if(playerCount < 4)
        {
            playerCount++;

            UIManager.instance.SetTextToInt(UIManager.instance.playerCountText, playerCount);
            UIManager.instance.SetPlayerPanelVisibility(playerCount);
        }
    }

    // Decreases the number of players, and requests updates of the UI. Is called from a button on the main menu.
    //
    public void DecreasePlayerCount()
    {
        if(playerCount > 2)
        {
            playerCount--;
            
            UIManager.instance.SetTextToInt(UIManager.instance.playerCountText, playerCount);
            UIManager.instance.SetPlayerPanelVisibility(playerCount);
        }
    }


    IEnumerator PauseExecution(float duration)
    {
        float totalTime = 0f;

        while(totalTime <= duration)
        {
            totalTime += Time.deltaTime;
            yield return null;
        }
    }
}
