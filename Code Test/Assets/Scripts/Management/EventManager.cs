using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;


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

    // UI Events.
    // ---------------
    /*
    public void OpenCloseStorage()
    {
        if(onOpenCloseStorage != null)
        {
            onOpenCloseStorage();
        }
    }
    
    public event Action<TMP_InputField> onToggleInputField;
    public void ToggleInputField(TMP_InputField inputField)
    {
        if(onToggleInputField != null)
        {
            onToggleInputField(inputField);
        }
    }

    public event Action<int> onUpdatePlayerCount;
    public void UpdatePlayerCount(TextMeshProUGUI textComponent, int newPlayerCount)
    {
        if(onUpdatePlayerCount != null)
        {
            onUpdatePlayerCount(newPlayerCount);
        }
    }
    */
}
