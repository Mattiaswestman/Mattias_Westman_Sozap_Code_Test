using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance = null;


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

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
