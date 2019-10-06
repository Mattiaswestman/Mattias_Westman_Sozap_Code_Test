using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [Header("Canvases")]
    [SerializeField] public Canvas menuCanvas = null;
    [SerializeField] public Canvas gameCanvas = null;

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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            EnableCanvas(menuCanvas);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DisableCanvas(menuCanvas);
        }
    }

    private void InitializeUI()
    {
        menuCanvas.gameObject.SetActive(true);
        menuCanvas.gameObject.GetComponent<Canvas>().enabled = true;
        gameCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.GetComponent<Canvas>().enabled = false;
        
        playerOneMenuPanel.SetActive(true);
        playerTwoMenuPanel.SetActive(true);
        playerThreeMenuPanel.SetActive(false);
        playerFourMenuPanel.SetActive(false);

        playerOneScorePanel.SetActive(true);
        playerTwoScorePanel.SetActive(true);
        playerThreeScorePanel.SetActive(false);
        playerFourScorePanel.SetActive(false);
    }
    
    public void EnableCanvas(Canvas canvas)
    {
        canvas.gameObject.GetComponent<Canvas>().enabled = true;
    }

    public void DisableCanvas(Canvas canvas)
    {
        canvas.gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void EnableUIObject(GameObject panelObject)
    {
        panelObject.SetActive(true);
    }

    public void DisableUIObject(GameObject panelObject)
    {
        panelObject.SetActive(false);
    }

    public void SetTextToString(TextMeshProUGUI textComponent, string text)
    {
        textComponent.text = text;
    }

    public void SetTextToInt(TextMeshProUGUI textComponent, int number)
    {
        textComponent.text = number.ToString();
    }

    // Toggles the input field for changing names of players. Is called when a change name button is pressed on the main menu.
    //
    public void ToggleInputField(TMP_InputField inputField)
    {
        inputField.gameObject.SetActive(!inputField.IsActive());
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

            default:
                break;
        }
    }
}
