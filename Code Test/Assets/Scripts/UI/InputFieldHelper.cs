using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldHelper : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI[] textToModify = null;

    private TMP_InputField inputField = null;


    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        if(inputField == null)
        {
            Debug.LogError($"InputFieldHelper: No TMP_InputField component found on {gameObject.name}.");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        inputField.onEndEdit.AddListener(SetTextFromInput);
    }

    public void SetTextFromInput(string input)
    {
        for(int i = 0; i < textToModify.Length; i++)
        {
            UIManager.instance.SetTextComponentToString(textToModify[i], input);
        }
        
        UIManager.instance.ToggleUIObject(gameObject);
    }
}
