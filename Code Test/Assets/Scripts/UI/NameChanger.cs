using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameChanger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI[] nameTexts = null;

    private TMP_InputField myInputField = null;


    private void Awake()
    {
        myInputField = GetComponent<TMP_InputField>();
        if (myInputField == null)
        {
            Debug.LogError($"NameChanger: No TMP_InputField component found on {gameObject.name}.");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        myInputField.onEndEdit.AddListener(SetTextFromInput);
    }

    // Updates the referenced UI texts of this player when a new name is submitted.
    //
    public void SetTextFromInput(string input)
    {
        for (int i = 0; i < nameTexts.Length; i++)
        {
            UIManager.instance.SetTextComponentToString(nameTexts[i], input);
        }
        
        UIManager.instance.ToggleUIObject(gameObject);
    }
}
