using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSetter : MonoBehaviour
{
    //[Header("Player Colors")]
    [SerializeField] private Color[] playerColors = new Color[4];
    //[SerializeField] private Color playerTwoColor = default;
    //[SerializeField] private Color playerThreeColor = default;
    //[SerializeField] private Color playerFourColor = default;

    [Space(10)]
    [SerializeField] private GameObject[] playerShipFronts = null;
    [SerializeField] private Image[] scorePanelBackgrounds = null;
    

    public void SetPlayerColor()
    {
        for(int i = 0; i < playerShipFronts.Length; i++)
        {
            playerShipFronts[i].GetComponent<SpriteRenderer>().color = playerColors[i];
        }
        for(int i = 0; i < scorePanelBackgrounds.Length; i++)
        {
            scorePanelBackgrounds[i].color = playerColors[i];
        }
    }
}
