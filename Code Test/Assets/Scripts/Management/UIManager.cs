using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [Header("Canvases")]
    [SerializeField] public Canvas mainMenuCanvas = null;
    [SerializeField] public Canvas scoreMenuCanvas = null;
    [SerializeField] public Canvas countdownCanvas = null;
    /*
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

    [Header("Player Score")]
    [SerializeField] public TextMeshProUGUI playerOneScoreText = null;
    [SerializeField] public TextMeshProUGUI playerTwoScoreText = null;
    [SerializeField] public TextMeshProUGUI playerThreeScoreText = null;
    [SerializeField] public TextMeshProUGUI playerFourScoreText = null;
    */
    [Header("Player UI")]
    [SerializeField] private GameObject[] playerMainMenuPanels = null;
    [Space(10)]
    [SerializeField] private GameObject[] playerScoreMenuPanels = null;
    [Space(10)]
    public TextMeshProUGUI[] playerScoreTexts = null;

    [Header("Various UI Elements")]
    [SerializeField] public TextMeshProUGUI playerCountText = null;
    [SerializeField] public GameObject countdownText = null;
    [SerializeField] public GameObject nextRoundButton = null;
    [SerializeField] public GameObject menuButton = null;

    private Canvas activeCanvas = null;


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
        mainMenuCanvas.gameObject.SetActive(true);
        EnableCanvas(mainMenuCanvas);
        scoreMenuCanvas.gameObject.SetActive(true);
        DisableCanvas(scoreMenuCanvas);
        countdownCanvas.gameObject.SetActive(true);
        DisableCanvas(countdownCanvas);

        activeCanvas = mainMenuCanvas;
    }

    public void OpenMainMenu()
    {
        // Disable active canvas.
        DisableCanvas(activeCanvas);
        // Fade in main menu.
        activeCanvas = mainMenuCanvas;
        EnableCanvas(activeCanvas);
    }

    public void OpenScoreMenu()
    {
        // Disable active canvas.
        DisableCanvas(activeCanvas);
        // Fade in score menu.
        activeCanvas = scoreMenuCanvas;
        EnableCanvas(activeCanvas);
    }

    public void StartCountdown(int roundNumber)
    {
        // Fade out active canvas.
        // Disable active canvas.
        DisableCanvas(activeCanvas);
        // Set countdown canvas active.
        activeCanvas = countdownCanvas;
        EnableCanvas(activeCanvas);
        
        // *Run countdown*

        // Disable countdown canvas.
        DisableCanvas(activeCanvas);
    }

    public void StartWinAnimation()
    {

    }

    public void SetPlayerUIEnabled(int playerCount)
    {
        switch(playerCount)
        {
            case 2:
                DisableUIObject(playerMainMenuPanels[2]);
                DisableUIObject(playerMainMenuPanels[3]);

                DisableUIObject(playerScoreMenuPanels[2]);
                DisableUIObject(playerScoreMenuPanels[3]);
                break;

            case 3:
                EnableUIObject(playerMainMenuPanels[2]);
                DisableUIObject(playerMainMenuPanels[3]);

                EnableUIObject(playerScoreMenuPanels[2]);
                DisableUIObject(playerScoreMenuPanels[3]);
                break;

            case 4:
                EnableUIObject(playerMainMenuPanels[2]);
                EnableUIObject(playerMainMenuPanels[3]);

                EnableUIObject(playerScoreMenuPanels[2]);
                EnableUIObject(playerScoreMenuPanels[3]);
                break;

            default:
                break;
        }
    }

    public void UpdateUIScore(List<int> playerScore)
    {
        for(int i = 0; i < playerScore.Count; i++)
        {
            SetTextComponentToInt(playerScoreTexts[i], playerScore[i]);
        }
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

    public void ToggleUIObject(GameObject uiObject)
    {
        uiObject.SetActive(!uiObject.activeSelf);
    }

    public void SetTextComponentToString(TextMeshProUGUI textComponent, string text)
    {
        textComponent.SetText(text);
    }

    public void SetTextComponentToString(TextMeshPro textComponent, string text)
    {
        textComponent.SetText(text);
    }

    public void SetTextComponentToInt(TextMeshProUGUI textComponent, int number)
    {
        textComponent.SetText(number.ToString());
    }

    public void SetTextComponentToInt(TextMeshPro textComponent, int number)
    {
        textComponent.SetText(number.ToString());
    }
}
