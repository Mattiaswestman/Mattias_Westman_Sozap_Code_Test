using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [Header("Canvases")]
    [SerializeField] public Canvas titleMenuCanvas = null;
    [SerializeField] public Canvas gameplayCanvas = null;

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
    
    [Header("Various UI Elements")]
    [SerializeField] public GameObject scorePanel = null;
    [SerializeField] public GameObject countdownText = null;
    [SerializeField] public TextMeshProUGUI playerCountText = null;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            enabled = false;
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeUI();
    }
    
    private void InitializeUI()
    {
        titleMenuCanvas.gameObject.SetActive(true);
        DisableCanvas(titleMenuCanvas);
        gameplayCanvas.gameObject.SetActive(true);
        DisableCanvas(gameplayCanvas);
    }
    
    public void EnableCanvas(Canvas canvas)
    {
        canvas.gameObject.GetComponent<Canvas>().enabled = true;
    }

    public void DisableCanvas(Canvas canvas)
    {
        canvas.gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void EnableUIObject(GameObject uiObject)
    {
        uiObject.SetActive(true);
    }

    public void DisableUIObject(GameObject uiObject)
    {
        uiObject.SetActive(false);
    }

    // Toggles the input field for changing names of players. Is called when a change name button is pressed on the main menu.
    //
    public void ToggleUIObject(GameObject uiObject)
    {
        uiObject.SetActive(!uiObject.activeSelf);
    }

    public void SetTextAsString(TextMeshProUGUI textComponent, string text)
    {
        textComponent.text = text;
    }

    public void SetTextAsInt(TextMeshProUGUI textComponent, int number)
    {
        textComponent.text = number.ToString();
    }

    

    // Enables/disables UI for the third and fourth player, based on the current player count. Is called when player count is changed in the game manager.
    //
    public void UpdatePlayerUIVisibility(int playerCount)
    {
        switch(playerCount)
        {
            case 2:
                DisableUIObject(playerThreeMenuPanel);
                DisableUIObject(playerFourMenuPanel);

                DisableUIObject(playerThreeScorePanel);
                DisableUIObject(playerFourScorePanel);
                break;

            case 3:
                EnableUIObject(playerThreeMenuPanel);
                DisableUIObject(playerFourMenuPanel);

                EnableUIObject(playerThreeScorePanel);
                DisableUIObject(playerFourScorePanel);
                break;

            case 4:
                EnableUIObject(playerThreeMenuPanel);
                EnableUIObject(playerFourMenuPanel);

                EnableUIObject(playerThreeScorePanel);
                EnableUIObject(playerFourScorePanel);
                break;

            default:
                break;
        }
    }

    public void UpdateUIScore(int playerCount)
    {
        
    }
}
