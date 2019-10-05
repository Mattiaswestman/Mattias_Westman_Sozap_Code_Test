using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager = null;
    
    private int playerCount = 2;
    private int currentRoundNumber = 1;


    private void Awake()
    {
        if(uiManager == null)
        {
            Debug.LogError("Game Manager: No reference set to UI Manager.");
            enabled = false;
            return;
        }
    }

    // WIP: Starts a new game from the main menu. Is called from the Play button.
    //
    public void StartGame()
    {
        uiManager.SwapCurrentCanvas();
        StartRound(currentRoundNumber);
    }

    // Starts a new round with of the given round number. Is called when a new round should be initiated.
    //
    public void StartRound(int roundNumber)
    {

    }

    // Called from Menu button after game complete?
    //
    public void EndGame()
    {
        // Reset values.
        uiManager.SwapCurrentCanvas();
    }

    // Increases the number of players for the game, and requests updates of the UI. Is called from a button on the main menu.
    //
    public void IncreasePlayerCount()
    {
        if(playerCount < 4)
        {
            playerCount++;

            uiManager.SetPlayerCountText(playerCount);
            uiManager.SetPlayerPanelVisibility(playerCount);
        }
    }

    // Decreases the number of players for the game, and requests updates of the UI. Is called from a button on the main menu.
    //
    public void DecreasePlayerCount()
    {
        if(playerCount > 2)
        {
            playerCount--;

            uiManager.SetPlayerCountText(playerCount);
            uiManager.SetPlayerPanelVisibility(playerCount);
        }
    }
}
