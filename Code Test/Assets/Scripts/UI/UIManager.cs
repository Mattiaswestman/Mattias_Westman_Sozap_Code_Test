using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] private Canvas menuCanvas = null;
    [SerializeField] private Canvas gameCanvas = null;

    [Header("Menu Panels")]
    [SerializeField] private GameObject playerOneMenuPanel = null;
    [SerializeField] private GameObject playerTwoMenuPanel = null;
    [SerializeField] private GameObject playerThreeMenuPanel = null;
    [SerializeField] private GameObject playerFourMenuPanel = null;

    [Header("Score Panels")]
    [SerializeField] private GameObject playerOneScorePanel = null;
    [SerializeField] private GameObject playerTwoScorePanel = null;
    [SerializeField] private GameObject playerThreeScorePanel = null;
    [SerializeField] private GameObject playerFourScorePanel = null;

    [Header("Various UI Elements")]
    [SerializeField] private TextMeshProUGUI playerCountText = null;


    private void Start()
    {
        menuCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);

        playerOneMenuPanel.SetActive(true);
        playerTwoMenuPanel.SetActive(true);
        playerThreeMenuPanel.SetActive(false);
        playerFourMenuPanel.SetActive(false);

        playerOneScorePanel.SetActive(true);
        playerTwoScorePanel.SetActive(true);
        playerThreeScorePanel.SetActive(false);
        playerFourScorePanel.SetActive(false);
    }

    public void SetPlayerCountText(int newPlayerCount)
    {
        playerCountText.text = newPlayerCount.ToString();
    }

    // TODO: Rework.
    public void SetPlayerPanelVisibility(int playerCount)
    {
        switch (playerCount)
        {
            case 2:
                playerThreeMenuPanel.SetActive(false);
                playerFourMenuPanel.SetActive(false);

                playerThreeScorePanel.SetActive(false);
                playerFourScorePanel.SetActive(false);
                break;

            case 3:
                playerThreeMenuPanel.SetActive(true);
                playerFourMenuPanel.SetActive(false);

                playerThreeScorePanel.SetActive(true);
                playerFourScorePanel.SetActive(false);
                break;

            case 4:
                playerThreeMenuPanel.SetActive(true);
                playerFourMenuPanel.SetActive(true);

                playerThreeScorePanel.SetActive(true);
                playerFourScorePanel.SetActive(true);
                break;
        }
    }

    public void ToggleNameChangeField(TMP_InputField nameChangeField)
    {
        nameChangeField.gameObject.SetActive(!nameChangeField.IsActive());
    }
}
