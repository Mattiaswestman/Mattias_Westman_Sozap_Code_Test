using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] players = new GameObject[4];

    private int[] playerScore = new int[4];

    private int playerCount = 2;
    private int currentRoundNumber = 1;

    //private bool roundOver = false;



    public int score_TESTING = 0;



    private void Start()
    {
        UIManager.instance.SetTextAsInt(UIManager.instance.playerCountText, playerCount);
        UIManager.instance.SetPlayerUIEnabled(playerCount);
        SetPlayerObjectEnabled(playerCount);
    }
    
    public void StartGame()
    {
        UIManager.instance.DisableCanvas(UIManager.instance.titleMenuCanvas);
        UIManager.instance.EnableCanvas(UIManager.instance.gameplayCanvas);

        StartRound();
    }

    public void StartRound() // Parameter: int roundNumber
    {
        // *Countdown*

        UIManager.instance.DisableCanvas(UIManager.instance.gameplayCanvas);

        //StartCoroutine(playRoutine_TESTING(5f));
        
        //EndRound();
    }

    private void EndRound()
    {
        // *Get round winner*
        // *Update score*
        //score_TESTING += 30;

        UpdateScore();

        ResetRound();

        // *Check for game winner*
        // *Update UI*
        if(score_TESTING >= 150)
        {
            UIManager.instance.DisableUIObject(UIManager.instance.nextRoundButton);
            UIManager.instance.EnableUIObject(UIManager.instance.menuButton);
        }

        // *Show UI*
        UIManager.instance.EnableCanvas(UIManager.instance.gameplayCanvas);

        // *New round or Menu*
    }

    public void EndGame()
    {
        ResetGame();

        UIManager.instance.DisableCanvas(UIManager.instance.gameplayCanvas);
        UIManager.instance.EnableCanvas(UIManager.instance.titleMenuCanvas);
    }

    private void ResetRound()
    {
        // Reset players.
        for(int i = 0; i < players.Length; i++)
        {

        }
    }

    private void ResetGame()
    {
        currentRoundNumber = 1;
        score_TESTING = 0;
    }

    private void UpdateScore()
    {
        for(int i = 0; i < players.Length; i++)
        {
            playerScore[i] += players[i].GetComponent<RoundScore>().CurrentRoundScore;

            if(players[i].GetComponent<Health>().IsAlive)
            {
                playerScore[i] += 30;
            }
        }

        //UIManager.instance.SetTextAsInt(UIManager.instance.player)
    }

    // Increases the number of players, and requests updates of the UI. Is called from a button on the main menu.
    //
    public void IncreasePlayerCount()
    {
        if(playerCount < 4)
        {
            playerCount++;

            UIManager.instance.SetTextAsInt(UIManager.instance.playerCountText, playerCount);
            UIManager.instance.SetPlayerUIEnabled(playerCount);
            SetPlayerObjectEnabled(playerCount);
        }
    }

    // Decreases the number of players, and requests updates of the UI. Is called from a button on the main menu.
    //
    public void DecreasePlayerCount()
    {
        if(playerCount > 2)
        {
            playerCount--;
            
            UIManager.instance.SetTextAsInt(UIManager.instance.playerCountText, playerCount);
            UIManager.instance.SetPlayerUIEnabled(playerCount);
            SetPlayerObjectEnabled(playerCount);
        }
    }
    /*
    IEnumerator playRoutine_TESTING(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        EndRound();
    }
    */

    private void SetPlayerObjectEnabled(int playerCount)
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
}
