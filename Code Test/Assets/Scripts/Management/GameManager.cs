using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform projectilesSceneParent = null;
    [SerializeField] private Transform trailsSceneParent = null;
    [SerializeField] private GameObject[] players = new GameObject[4];

    private List<GameObject> activePlayers = new List<GameObject>();
    private List<int> playerScore = new List<int>();

    private int playerCount = 2;
    private int currentRoundNumber = 1;

    private bool isGameRoundStarted = false;
    private bool isGameOver = false;


    private void Start()
    {
        UIManager.instance.SetTextComponentToInt(UIManager.instance.playerCountText, playerCount);
        UIManager.instance.SetPlayerUIEnabled(playerCount);
        SetPlayerObjectsActive(playerCount);
    }

    private void Update()
    {
        if(isGameRoundStarted)
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
            isGameRoundStarted = false;
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
                playerScore.Add(0);
            }
        }
        
        StartRound();
    }

    // Initializes and starts a new game round. When started, the game logic is controlled by the Update() function.
    // Is called from StartGame() or from the "Next round" button on the score menu. 
    //
    public void StartRound()
    {
        ResetRound();

        SetPlayersIsControllable(true);

        UIManager.instance.StartCountdown(currentRoundNumber);

        SetPlayersCanMove(true);

        isGameRoundStarted = true;
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
        UIManager.instance.UpdateUIScore(playerScore);

        if(isGameOver)
        {
            UIManager.instance.DisableUIObject(UIManager.instance.nextRoundButton);
            UIManager.instance.EnableUIObject(UIManager.instance.menuButton);
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
        DestroyChildObjects(trailsSceneParent);

        for(int i = 0; i < activePlayers.Count; i++)
        {
            activePlayers[i].GetComponent<PlayerManager>().ResetPlayer();
        }
    }

    private void ResetGame()
    {
        ResetRound();

        activePlayers.Clear();
        playerScore.Clear();

        UIManager.instance.DisableUIObject(UIManager.instance.menuButton);
        UIManager.instance.EnableUIObject(UIManager.instance.nextRoundButton);

        currentRoundNumber = 1;
        isGameOver = false;
    }

    private void UpdateScore()
    {
        for(int i = 0; i < activePlayers.Count; i++)
        {
            playerScore[i] += activePlayers[i].GetComponent<RoundScore>().CurrentRoundScore;

            if(activePlayers[i].GetComponent<Health>().IsAlive)
            {
                playerScore[i] += 30;
            }

            if(playerScore[i] >= 150)
            {
                UIManager.instance.StartWinAnimation();
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
            activePlayers[i].GetComponent<InputManager>().IsControllable = value;
        }
    }

    private void SetPlayersCanMove(bool value)
    {
        for(int i = 0; i < activePlayers.Count; i++)
        {
            activePlayers[i].GetComponent<InputManager>().CanMove = value;
            activePlayers[i].GetComponent<Trail>().IsPaused = !value;
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
