using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI stageNameText;
    [SerializeField] private TextMeshProUGUI goldNumberText;

    public void ExitGame()
    {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("LobbyScene");
    }

    public void OnGameOver(string text, int gold)
    {
        gameOverPanel.SetActive(true);

        stageNameText.text = text;
        goldNumberText.text = gold.ToString();
    }
}
