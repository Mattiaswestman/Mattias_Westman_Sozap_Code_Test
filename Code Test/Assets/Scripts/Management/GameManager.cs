using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("References")]
    [SerializeField] private Transform projectilesSceneParent = null;
    [SerializeField] private Transform trailsSceneParent = null;
    [SerializeField] private GameObject[] players = new GameObject[4];

    private List<GameObject> activePlayers = new List<GameObject>();
    private List<int> playerPoints = new List<int>();

    private int playerCount = 2;
    private int currentRoundNumber = 1;

    private bool hasCountdownFinished = false;
    public bool HasCountdownFinished { set { hasCountdownFinished = value; } }
    private bool hasGameRoundStarted = false;
    private bool isGameOver = false;


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
        UIManager.instance.SetTextComponentToInt(UIManager.instance.playerCountText, playerCount);
        UIManager.instance.SetPlayerUIEnabled(playerCount);
        SetPlayerObjectsActive(playerCount);
    }

    private void Update()
    {
        if(hasCountdownFinished)
        {
            hasCountdownFinished = false;

            SetPlayersCanMove(true);

            hasGameRoundStarted = true;
        }
        if(hasGameRoundStarted)
        {
            CheckIfRoundOver();
        }
    }

    // Controls how many players are still alive in the current game round. Will end it when 1 or less players is alive.
    //
    private void CheckIfRoundOver()
    {
        var playersAlive = activePlayers.Count;

        for(int i = 0; i < activePlayers.Count; i++)
        {
            if(!activePlayers[i].GetComponent<Health>().IsAlive)
            {
                playersAlive--;
            }
        }

        if(playersAlive <= 1)
        {
            hasGameRoundStarted = false;
            EndRound();
        }
    }

    // Starts a new game. 
    // Is called from the PLAY button on the main menu.
    //
    public void StartGame()
    {
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i].activeSelf)
            {
                activePlayers.Add(players[i]);
                playerPoints.Add(0);
            }
        }
        
        StartRound();
    }

    // Initializes and starts a new game round. When countdown is started the game logic is handled by the Update() function.
    // Is called from StartGame() or from the "Next round" button on the score menu.
    //
    public void StartRound()
    {
        ResetRound();

        SetPlayersIsControllable(true);

        UIManager.instance.StartCountdown(currentRoundNumber);

        //SetPlayersCanMove(true);

        //hasGameRoundStarted = true;
    }

    // Disables player movement, updates score and checks for a winner. Will lead to either a new game round or end of game.
    // Is called from the Update() function.
    //
    private void EndRound()
    {
        SetPlayersCanMove(false);
        SetPlayersIsControllable(false);
        DestroyChildObjects(projectilesSceneParent);

        UpdateScore();
        UIManager.instance.UpdateUIScore(playerPoints);

        if(isGameOver)
        {
            UIManager.instance.SetUIObjectActive(UIManager.instance.nextRoundButton, false);
            UIManager.instance.SetUIObjectActive(UIManager.instance.menuButton, true);
        }

        UIManager.instance.OpenScoreMenu();
    }

    public void EndGame()
    {
        ResetGame();

        UIManager.instance.OpenMainMenu();
    }

    private void ResetRound()
    {
        for(int i = 0; i < activePlayers.Count; i++)
        {
            activePlayers[i].GetComponent<PlayerManager>().ResetPlayer();
            activePlayers[i].GetComponent<Trail>().StopAllCoroutines();
        }

        DestroyChildObjects(trailsSceneParent);
    }

    private void ResetGame()
    {
        ResetRound();

        for(int playerIndex = 0; playerIndex < activePlayers.Count; playerIndex++)
        {
            UIManager.instance.SetPlayerHasWon(playerIndex, false);
        }

        activePlayers.Clear();
        playerPoints.Clear();

        UIManager.instance.SetUIObjectActive(UIManager.instance.menuButton, false);
        UIManager.instance.SetUIObjectActive(UIManager.instance.nextRoundButton, true);
        
        currentRoundNumber = 1;
        isGameOver = false;
    }

    private void StartCountdown(int roundNumber)
    {
        UIManager.instance.StartCountdown(currentRoundNumber);
    }

    private void UpdateScore()
    {
        for(int i = 0; i < activePlayers.Count; i++)
        {
            playerPoints[i] += activePlayers[i].GetComponent<RoundScore>().CurrentRoundPoints;

            if(activePlayers[i].GetComponent<Health>().IsAlive)
            {
                playerPoints[i] += 30;
            }

            if(playerPoints[i] >= 150)
            {
                UIManager.instance.SetPlayerHasWon(i, true);
                isGameOver = true;
            }
        }
    }

    // Increases the number of players, and requests updates of the UI. Is called from a button on the main menu.
    //
    public void IncreasePlayerCount()
    {
        if(playerCount < 4)
        {
            playerCount++;

            UIManager.instance.SetTextComponentToInt(UIManager.instance.playerCountText, playerCount);
            UIManager.instance.SetPlayerUIEnabled(playerCount);
            SetPlayerObjectsActive(playerCount);
        }
    }

    // Decreases the number of players, and requests updates of the UI. Is called from a button on the main menu.
    //
    public void DecreasePlayerCount()
    {
        if(playerCount > 2)
        {
            playerCount--;

            UIManager.instance.SetTextComponentToInt(UIManager.instance.playerCountText, playerCount);
            UIManager.instance.SetPlayerUIEnabled(playerCount);
            SetPlayerObjectsActive(playerCount);
        }
    }
    
    private void SetPlayerObjectsActive(int playerCount)
    {
        switch(playerCount)
        {
            case 2:
                players[2].SetActive(false);
                players[3].SetActive(false);
                break;

            case 3:
                players[2].SetActive(true);
                players[3].SetActive(false);
                break;

            case 4:
                players[2].SetActive(true);
                players[3].SetActive(true);
                break;

            default:
                break;
        }
    }

    private void SetPlayersIsControllable(bool value)
    {
        for(int i = 0; i < activePlayers.Count; i++)
        {
            activePlayers[i].GetComponent<PlayerManager>().SetPlayerIsControllable(value);
        }
    }

    private void SetPlayersCanMove(bool value)
    {
        for(int i = 0; i < activePlayers.Count; i++)
        {
            activePlayers[i].GetComponent<PlayerManager>().SetPlayerCanMove(value);
        }
    }

    private void DestroyChildObjects(Transform sceneObject)
    {
        var children = new List<GameObject>();

        foreach(Transform child in sceneObject)
        {
            children.Add(child.gameObject);
        }

        children.ForEach(child => Destroy(child));
    }
}
