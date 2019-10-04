using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager = null;
    /*
    [Header("Player Objects")]
    [SerializeField] private GameObject playerOne = null;
    [SerializeField] private GameObject playerTwo = null;
    [SerializeField] private GameObject playerThree = null;
    [SerializeField] private GameObject playerFour = null;
    */
    private int playerCount = 2;


    private void Awake()
    {
        if(uiManager == null)
        {
            Debug.LogError("No reference set to UI Manager.");
            enabled = false;
        }
    }
    
    public void StartGame()
    {
        // Called from Play button?
    }

    public void IncreasePlayerCount()
    {
        if(playerCount < 4)
        {
            playerCount++;

            uiManager.SetPlayerCountText(playerCount);
            uiManager.SetPlayerPanelVisibility(playerCount);
        }
    }

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
