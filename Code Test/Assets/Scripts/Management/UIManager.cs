using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    [SerializeField] private Animation countdownAnimation = null;

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
    public TextMeshProUGUI playerCountText = null;
    public TextMeshProUGUI countdownText = null;
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
        if(isScoreMenuLoaded)
        {
            isScoreMenuLoaded = false;

            for(int i = 0; i < GameManager.instance.ActivePlayers.Count; i++)
            {
                playerPointsBars[i].UpdatePointsBar(int.Parse(playerScoreTexts[i].text));
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

    public void OpenMainMenu()
    {
        activeCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        SetCanvasEnabled(activeCanvas, false);
        
        activeCanvas = mainMenuCanvas;
        SetCanvasEnabled(activeCanvas, true);

        fadeInRoutine = StartCoroutine(FadeInRoutine(activeCanvas, 0.5f));
    }

    public void OpenScoreMenu()
    {
        activeCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        SetCanvasEnabled(activeCanvas, false);
        
        activeCanvas = scoreMenuCanvas;
        SetCanvasEnabled(activeCanvas, true);

        fadeInRoutine = StartCoroutine(FadeInRoutine(activeCanvas, 0.5f));
    }

    public void StartCountdown(int roundNumber)
    {
        fadeOutRoutine = StartCoroutine(FadeOutRoutine(activeCanvas, 0.25f));

        activeCanvas = gameCanvas;
        SetCanvasEnabled(activeCanvas, true);
        activeCanvas.GetComponent<CanvasGroup>().alpha = 1f;

        countdownRoutine = StartCoroutine(CountdownRoutine(roundNumber));
    }

    // TODO: Rewrite?
    //
    public void SetPlayerHasWon(int playerIndex, bool value)
    {
        scoreMenuPlayerPanels[playerIndex].GetComponentInChildren<Animator>().SetBool("hasWon", value);
        playerWinnerTexts[playerIndex].SetActive(value);
    }

    public void SetPlayerUIEnabled(int playerCount)
    {
        switch(playerCount)
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

    public void UpdateUIScore(List<int> playerScore)
    {
        for(int i = 0; i < playerScore.Count; i++)
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
    
    private IEnumerator FadeInRoutine(Canvas fadeInCanvas, float fadeDuration)
    {
        var timer = 0f;

        while(timer < fadeDuration)
        {
            float proportionCompleted = timer / fadeDuration;

            float alphaThisFrame = Mathf.Lerp(0f, 1f, proportionCompleted);

            fadeInCanvas.GetComponent<CanvasGroup>().alpha = alphaThisFrame;

            timer += Time.deltaTime;
            yield return null;
        }

        fadeInCanvas.GetComponent<CanvasGroup>().alpha = 1f;

        if(fadeInCanvas == scoreMenuCanvas)
        {
            isScoreMenuLoaded = true;
        }
    }
    
    private IEnumerator FadeOutRoutine(Canvas fadeOutCanvas, float fadeDuration)
    {
        var timer = 0f;

        while(timer < fadeDuration)
        {
            float proportionCompleted = timer / fadeDuration;

            float alphaThisFrame = Mathf.Lerp(1f, 0f, proportionCompleted);

            fadeOutCanvas.GetComponent<CanvasGroup>().alpha = alphaThisFrame;

            timer += Time.deltaTime;
            yield return null;
        }

        fadeOutCanvas.GetComponent<CanvasGroup>().alpha = 0f;
    }

    private IEnumerator CountdownRoutine(int roundNumber)
    {
        string roundIntro = "";

        if(roundNumber == 1)
        {
            roundIntro = "First to 150p Wins";
        }
        else
        {
            roundIntro = $"Round {roundNumber}";
        }

        for(int i = 0; i < 5; i++)
        {
            switch(i)
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
            
            while(countdownAnimation.isPlaying)
            {
                yield return null;
            }
        }

        gameCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        isCountdownOver = true;
    }
}

/*
private IEnumerator CountdownRoutine(int roundNumber, float duration)
    {
        string roundIntro = "";

        if(roundNumber == 1)
        {
            roundIntro = "First to 150p Wins";
        }
        else
        {
            roundIntro = $"Round {roundNumber}";
        }

        SetTextComponentToString(countdownText, roundIntro);


        float timer = duration;

        while(timer >= 0f)
        {

            timer -= Time.deltaTime;
            yield return null;
        }
        
        gameCanvas.GetComponent<CanvasGroup>().alpha = 0f;
        GameManager.instance.HasCountdownFinished = true;
    } 
*/
