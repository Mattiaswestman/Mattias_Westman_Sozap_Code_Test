using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] private Canvas menuCanvas = null;
    [SerializeField] private Canvas gameCanvas = null;

    [Header("Menu Panels")]
    [SerializeField] private GameObject playerOneMenuPanel = null;
    [SerializeField] private GameObject playerTwoMenuPanel = null;
    [SerializeField] private GameObject playerThreeMenuPanel = null;
    [SerializeField] private GameObject playerFourMenuPanel = null;

    [Header("Score Panels")]
    [SerializeField] private GameObject playerOneScorePanel = null;
    [SerializeField] private GameObject playerTwoScorePanel = null;
    [SerializeField] private GameObject playerThreeScorePanel = null;
    [SerializeField] private GameObject playerFourScorePanel = null;

    [Header("Player Names")]
    [SerializeField] private TextMeshProUGUI playerOneName = null;
    [SerializeField] private TextMeshProUGUI playerTwoName = null;
    [SerializeField] private TextMeshProUGUI playerThreeName = null;
    [SerializeField] private TextMeshProUGUI playerFourName = null;

    [Header("Various UI Elements")]
    [SerializeField] private TextMeshProUGUI playerCountText = null;

    private Canvas currentCanvas = null;


    private void Start()
    {
        InitializeUI();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            ToggleCurrentCanvas();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SwapCurrentCanvas();
        }
    }

    private void InitializeUI()
    {
        menuCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
        currentCanvas = menuCanvas;

        playerOneMenuPanel.SetActive(true);
        playerTwoMenuPanel.SetActive(true);
        playerThreeMenuPanel.SetActive(false);
        playerFourMenuPanel.SetActive(false);

        playerOneScorePanel.SetActive(true);
        playerTwoScorePanel.SetActive(true);
        playerThreeScorePanel.SetActive(false);
        playerFourScorePanel.SetActive(false);
    }

    // Toggles current canvas on or off.
    //
    public void ToggleCurrentCanvas()
    {
        currentCanvas.gameObject.SetActive(!currentCanvas.gameObject.activeSelf);
    }

    // Swaps current canvas between the two existing.
    //
    public void SwapCurrentCanvas()
    {
        if(currentCanvas == menuCanvas)
        {
            currentCanvas = gameCanvas;
            currentCanvas.gameObject.SetActive(menuCanvas.gameObject.activeSelf);
            menuCanvas.gameObject.SetActive(false);
        }
        else
        {
            currentCanvas = menuCanvas;
            currentCanvas.gameObject.SetActive(gameCanvas.gameObject.activeSelf);
            gameCanvas.gameObject.SetActive(false);
        }
    }

    // Sets the player count number on the main menu to given parameter. Is called when player count is changed in the game manager.
    //
    public void SetPlayerCountText(int newPlayerCount)
    {
        playerCountText.text = newPlayerCount.ToString();
    }

    // Enables/disables UI for the third and fourth player, based on the current player count. Is called when player count is changed in the game manager.
    //
    public void SetPlayerPanelVisibility(int playerCount)
    {
        switch(playerCount)
        {
            case 2:
                playerThreeMenuPanel.SetActive(false);
                playerFourMenuPanel.SetActive(false);

                playerThreeScorePanel.SetActive(false);
                playerFourScorePanel.SetActive(false);
                break;

            case 3:
                playerThreeMenuPanel.SetActive(true);
                playerFourMenuPanel.SetActive(false);

                playerThreeScorePanel.SetActive(true);
                playerFourScorePanel.SetActive(false);
                break;

            case 4:
                playerThreeMenuPanel.SetActive(true);
                playerFourMenuPanel.SetActive(true);

                playerThreeScorePanel.SetActive(true);
                playerFourScorePanel.SetActive(true);
                break;
        }
    }
    
    // Toggles the input field for changing names of players. Is called when a change name button is pressed on the main menu.
    //
    public void ToggleNameChangeField(TMP_InputField nameChangeField)
    {
        nameChangeField.gameObject.SetActive(!nameChangeField.IsActive());
    }

    public void SetPlayerName(TextMeshProUGUI nameText)
    {
        
    }
}
