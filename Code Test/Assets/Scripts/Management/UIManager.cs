using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [Header("Canvases")]
    public Canvas mainMenuCanvas = null;
    public Canvas scoreMenuCanvas = null;
    public Canvas gameCanvas = null;
    
    [Header("Player UI")]
    [SerializeField] private GameObject[] mainMenuPlayerPanels = null;
    [Space(10)]
    [SerializeField] private GameObject[] scoreMenuPlayerPanels = null;
    [Space(10)]
    public TextMeshProUGUI[] playerScoreTexts = null;
    [Space(10)]
    public PointsBar[] playerPointsBars = null;
    [Space(10)]
    public GameObject[] playerWinnerTexts = null;

    [Header("Various UI Elements")]
    [SerializeField] private Animation countdownAnimation = null;
    [SerializeField] private TextMeshProUGUI countdownText = null;
    public TextMeshProUGUI playerCountText = null;
    public GameObject nextRoundButton = null;
    public GameObject menuButton = null;
    
    private Canvas activeCanvas = null;

    private Coroutine fadeInRoutine = null;
    private Coroutine fadeOutRoutine = null;
    private Coroutine countdownRoutine = null;

    private bool isCountdownOver = false;
    public bool IsCountdownOver { get { return isCountdownOver; } set { isCountdownOver = value; } }
    private bool isScoreMenuLoaded = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
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
        if (isScoreMenuLoaded)
        {
            isScoreMenuLoaded = false;

            for (int i = 0; i < GameManager.instance.ActivePlayers.Count; i++)
            {
                playerPointsBars[i].UpdatePointsBar(GameManager.instance.playerScore[i]);
                //playerPointsBars[i].UpdatePointsBar(30);
            }
        }
    }

    private void InitializeUI()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        SetCanvasEnabled(mainMenuCanvas, true);
        scoreMenuCanvas.gameObject.SetActive(true);
        SetCanvasEnabled(scoreMenuCanvas, false);
        gameCanvas.gameObject.SetActive(true);
        SetCanvasEnabled(gameCanvas, false);

        activeCanvas = mainMenuCanvas;
    }

    // Closes the current canvas and fades in the Main Menu. Is called from the "Menu" button on the Score Menu.
    //
    public void OpenMainMenu()
    {
        activeCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        activeCanvas.GetComponent<CanvasGroup>().interactable = false;
        SetCanvasEnabled(activeCanvas, false);
        
        activeCanvas = mainMenuCanvas;
        SetCanvasEnabled(activeCanvas, true);

        fadeInRoutine = StartCoroutine(FadeInRoutine(activeCanvas, 0.5f));
    }

    // Closes the current canvas and fades in the Score Menu. Is called by the GameManager when a round ends.
    //
    public void OpenScoreMenu()
    {
        activeCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        activeCanvas.GetComponent<CanvasGroup>().interactable = false;
        SetCanvasEnabled(activeCanvas, false);
        
        activeCanvas = scoreMenuCanvas;
        SetCanvasEnabled(activeCanvas, true);

        fadeInRoutine = StartCoroutine(FadeInRoutine(activeCanvas, 0.5f));
    }

    // Fades out the current canvas and starts countdown to the next round. Notifies GameManager when countdown is over.
    //
    public void StartCountdown(int roundNumber)
    {
        activeCanvas.GetComponent<CanvasGroup>().interactable = false;
        fadeOutRoutine = StartCoroutine(FadeOutRoutine(activeCanvas, 0.25f));

        activeCanvas = gameCanvas;
        SetCanvasEnabled(activeCanvas, true);
        activeCanvas.GetComponent<CanvasGroup>().alpha = 1f;

        countdownRoutine = StartCoroutine(CountdownRoutine(roundNumber));
    }

    // Sets the winning animation and "Winner" text for the given player index. Is called "true" by the GameManager when a winning player is found.
    //
    public void SetWinnerUI(int playerIndex, bool value)
    {
        scoreMenuPlayerPanels[playerIndex].GetComponentInChildren<Animator>().SetBool("hasWon", value);
        playerWinnerTexts[playerIndex].SetActive(value);
    }

    // Sets the visibility of player UIs based on the current player count.
    //
    public void SetPlayerUIEnabled(int playerCount)
    {
        switch (playerCount)
        {
            case 2:
                SetUIObjectActive(mainMenuPlayerPanels[2], false);
                SetUIObjectActive(mainMenuPlayerPanels[3], false);

                SetUIObjectActive(scoreMenuPlayerPanels[2], false);
                SetUIObjectActive(scoreMenuPlayerPanels[3], false);
                break;

            case 3:
                SetUIObjectActive(mainMenuPlayerPanels[2], true);
                SetUIObjectActive(mainMenuPlayerPanels[3], false);

                SetUIObjectActive(scoreMenuPlayerPanels[2], true);
                SetUIObjectActive(scoreMenuPlayerPanels[3], false);
                break;

            case 4:
                SetUIObjectActive(mainMenuPlayerPanels[2], true);
                SetUIObjectActive(mainMenuPlayerPanels[3], true);

                SetUIObjectActive(scoreMenuPlayerPanels[2], true);
                SetUIObjectActive(scoreMenuPlayerPanels[3], true);
                break;

            default:
                break;
        }
    }

    // Updates the UI score text with the current player score.
    //
    public void UpdateUIScore(List<int> playerScore)
    {
        for (int i = 0; i < playerScore.Count; i++)
        {
            SetTextComponentToInt(playerScoreTexts[i], playerScore[i]);
        }
    }
    
    public void SetCanvasEnabled(Canvas canvas, bool value)
    {
        canvas.gameObject.GetComponent<Canvas>().enabled = value;
    }
    
    public void SetUIObjectActive(GameObject uiObject, bool value)
    {
        uiObject.SetActive(value);
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

    // Fades in the given canvas over a given duration. Is done by modifying the alpha of its CanvasGroup.
    //
    private IEnumerator FadeInRoutine(Canvas fadeInCanvas, float fadeDuration)
    {
        var timer = 0f;

        while (timer < fadeDuration)
        {
            float proportionCompleted = timer / fadeDuration;

            float alphaThisFrame = Mathf.Lerp(0f, 1f, proportionCompleted);

            fadeInCanvas.GetComponent<CanvasGroup>().alpha = alphaThisFrame;

            timer += Time.deltaTime;
            yield return null;
        }

        fadeInCanvas.GetComponent<CanvasGroup>().alpha = 1f;
        fadeInCanvas.GetComponent<CanvasGroup>().interactable = true;

        // If the canvas being faded in is the Score Menu, set bool to true. Is used to delay the points bar increasing until the Score Menu is fully visible.
        if (fadeInCanvas == scoreMenuCanvas)
        {
            isScoreMenuLoaded = true;
        }
    }

    // Fades out the given canvas over a given duration. Is done by modifying the alpha of its CanvasGroup.
    //
    private IEnumerator FadeOutRoutine(Canvas fadeOutCanvas, float fadeDuration)
    {
        var timer = 0f;

        while (timer < fadeDuration)
        {
            float proportionCompleted = timer / fadeDuration;

            float alphaThisFrame = Mathf.Lerp(1f, 0f, proportionCompleted);

            fadeOutCanvas.GetComponent<CanvasGroup>().alpha = alphaThisFrame;

            timer += Time.deltaTime;
            yield return null;
        }

        fadeOutCanvas.GetComponent<CanvasGroup>().alpha = 0f;
    }

    // Starts a countdown with the given round number. I'm unfortunately not 100% satisfied with how this works to be honest (Which I probably shouldn't reveal..).
    //
    private IEnumerator CountdownRoutine(int roundNumber)
    {
        string roundIntro = "";

        // Decides what the intro to the countdown should say, based on the given round number.
        if (roundNumber == 1)
        {
            // If round number is 1, present the goal of the game.
            roundIntro = "First to 150p Wins";
        }
        else
        {
            // Else present which round it currently is.
            roundIntro = $"Round {roundNumber}";
        }

        // Loop through all five steps of the countdown, change the countdown text depending on index, and play its respective animation.
        for (int i = 0; i < 5; i++)
        {
            switch (i)
            {
                case 0:
                    SetTextComponentToString(countdownText, roundIntro);
                    countdownAnimation.Play("RoundIntro");
                    break;

                case 1:
                    SetTextComponentToInt(countdownText, 3);
                    countdownAnimation.Play("Three");
                    break;

                case 2:
                    SetTextComponentToInt(countdownText, 2);
                    countdownAnimation.Play("Two");
                    break;
                    
                case 3:
                    SetTextComponentToInt(countdownText, 1);
                    countdownAnimation.Play("One");
                    break;

                case 4:
                    SetTextComponentToString(countdownText, "Start");
                    countdownAnimation.Play("Start");
                    break;

                default:
                    break;
            }
            
            // Pause each iteration until its animation is done playing.
            while (countdownAnimation.isPlaying)
            {
                yield return null;
            }
        }

        // Hide the game canvas.
        gameCanvas.GetComponent<CanvasGroup>().alpha = 0f;

        // Set countdown is over, which the GameManager checks to know when the gameplay should start.
        isCountdownOver = true;
    }
}
