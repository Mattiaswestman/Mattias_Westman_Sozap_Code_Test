using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    //public int playerCount = 2;


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

        DontDestroyOnLoad(gameObject);
    }

    public void IncreasePlayerCount()
    {
        Debug.Log("Increase players");
    }

    public void DecreasePlayerCount()
    {
        Debug.Log("Decrease players");
    }
}
