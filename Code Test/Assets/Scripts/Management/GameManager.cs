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
    public List<GameObject> ActivePlayers { get { return activePlayers; } }
    private List<int> playerScore = new List<int>();
    public List<int> PlayerScore { get { return playerScore; } }

    private int playerCount = 2;
    private int currentRoundNumber = 1;

    private bool hasRoundStarted = false;
    private bool isGameOver = false;


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
        UIManager.instance.SetTextComponentToInt(UIManager.instance.playerCountText, playerCount);
        UIManager.instance.SetPlayerUIEnabled(playerCount);
        SetPlayerObjectsActive(playerCount);
    }
    
    private void Update()
    {
        if (UIManager.instance.IsCountdownOver)
        {
            UIManager.instance.IsCountdownOver = false;

            SetPlayersCanMove(true);

            hasRoundStarted = true;
        }
        if (hasRoundStarted)
        {
            CheckIfRoundOver();
        }
    }

    // Checks how many players are still alive in the current game round. Will end when 1 or less players is alive.
    //
    private void CheckIfRoundOver()
    {
        var playersAlive = activePlayers.Count;

        for (int i = 0; i < activePlayers.Count; i++)
        {
            if (!activePlayers[i].GetComponent<Health>().IsAlive)
            {
                playersAlive--;
            }
        }

        if (playersAlive <= 1)
        {
            hasRoundStarted = false;
            EndRound();
        }
    }

    // Starts a new game. Is called from the "PLAY" button on the Main Menu.
    //
    public void StartGame()
    {
        // When called, initialize active player list and score list with the players that are active. 
        // Is done to avoid wasteful calculations on players that are not apart of the current game session.
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].activeSelf)
            {
                activePlayers.Add(players[i]);
                playerScore.Add(0);
            }
        }
        
        StartRound();
    }

    // Initializes and starts a new game round. When countdown is over the game logic is handled by the Update() function.
    // Is called from StartGame() or from the "Next round" button on the Score Menu.
    //
    public void StartRound()
    {
        ResetRound();

        SetPlayersIsControllable(true);

        UIManager.instance.StartCountdown(currentRoundNumber);
    }

    // Does post round work before opening the Score Menu. Is called by the Update() function when a round is over.
    //
    private void EndRound()
    {
        currentRoundNumber++;

        SetPlayersCanMove(false);
        SetPlayersIsControllable(false);
        DestroyChildObjects(projectilesSceneParent);

        UpdateScore();
        UIManager.instance.UpdateUIScore(playerScore);

        if (isGameOver)
        {
            UIManager.instance.SetUIObjectActive(UIManager.instance.nextRoundButton, false);
            UIManager.instance.SetUIObjectActive(UIManager.instance.menuButton, true);
        }

        UIManager.instance.OpenScoreMenu();
    }

    // Resets the game and opens the Main Menu. Is called from the "Menu" button on the Score Menu.
    //
    public void EndGame()
    {
        ResetGame();

        UIManager.instance.OpenMainMenu();
    }

    // Resets values needed to start a new round.
    //
    private void ResetRound()
    {
        for (int i = 0; i < activePlayers.Count; i++)
        {
            activePlayers[i].GetComponent<PlayerManager>().ResetPlayer();
        }

        DestroyChildObjects(trailsSceneParent);
    }

    // Resets values needed to start a new game.
    //
    private void ResetGame()
    {
        ResetRound();

        // Sets winners to false and resets all point bars on Score Menu.
        for (int i = 0; i < activePlayers.Count; i++)
        {
            UIManager.instance.SetWinnerUI(i, false);
            UIManager.instance.playerPointsBars[i].ResetPointsBar();
        }

        activePlayers.Clear();
        playerScore.Clear();

        // Change "Menu" button to "Next round" button for next time Score Menu is opened.
        UIManager.instance.SetUIObjectActive(UIManager.instance.menuButton, false);
        UIManager.instance.SetUIObjectActive(UIManager.instance.nextRoundButton, true);
        
        currentRoundNumber = 1;
        isGameOver = false;
    }
    
    // Updates the score of all active players and checks for winners.
    //
    private void UpdateScore()
    {
        for (int i = 0; i < activePlayers.Count; i++)
        {
            playerScore[i] += activePlayers[i].GetComponent<RoundScore>().CurrentRoundScore;

            if (activePlayers[i].GetComponent<Health>().IsAlive)
            {
                playerScore[i] += 30;
            }
            
            if (playerScore[i] >= 150)
            {
                playerScore[i] = 150;
                UIManager.instance.SetWinnerUI(i, true);
                isGameOver = true;
            }
        }
    }

    // Increases the player count, and requests updates of the UI.
    // Is called from arrow button on the Main Menu.
    //
    public void IncreasePlayerCount()
    {
        if (playerCount < 4)
        {
            playerCount++;

            UIManager.instance.SetTextComponentToInt(UIManager.instance.playerCountText, playerCount);
            UIManager.instance.SetPlayerUIEnabled(playerCount);
            SetPlayerObjectsActive(playerCount);
        }
    }

    // Decreases the player count, and requests updates of the UI.
    // Is called from arrow button on the Main Menu.
    //
    public void DecreasePlayerCount()
    {
        if (playerCount > 2)
        {
            playerCount--;

            UIManager.instance.SetTextComponentToInt(UIManager.instance.playerCountText, playerCount);
            UIManager.instance.SetPlayerUIEnabled(playerCount);
            SetPlayerObjectsActive(playerCount);
        }
    }

    // Sets the active state of the player spaceships based on player count.
    //
    private void SetPlayerObjectsActive(int playerCount)
    {
        switch (playerCount)
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

    // Gives active players turn control if true. Called to let players change start direction during countdown.
    //
    private void SetPlayersIsControllable(bool value)
    {
        for (int i = 0; i < activePlayers.Count; i++)
        {
            activePlayers[i].GetComponent<PlayerManager>().SetPlayerIsControllable(value);
        }
    }

    // Gives active players total control if true, including shooting, invincibility, trail generation, etc. Called when gameplay starts.
    //
    private void SetPlayersCanMove(bool value)
    {
        for (int i = 0; i < activePlayers.Count; i++)
        {
            activePlayers[i].GetComponent<PlayerManager>().SetPlayerCanMove(value);
        }
    }

    // Destroys all children of a given scene object. Used to remove projectiles and trails from their scene parent.
    //
    private void DestroyChildObjects(Transform sceneParentObject)
    {
        var children = new List<GameObject>();

        foreach (Transform child in sceneParentObject)
        {
            children.Add(child.gameObject);
        }

        children.ForEach(child => Destroy(child));
    }
}
