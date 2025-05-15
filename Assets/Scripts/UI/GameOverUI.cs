using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageNameText;
    [SerializeField] private TextMeshProUGUI goldNumberText;

    public void ExitGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("LobbyScene");
    }

    public void OnGameOver(string text, int gold)
    {
        gameObject.SetActive(true);

        stageNameText.text = text;
        goldNumberText.text = gold.ToString();
    }
}
