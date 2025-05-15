using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageNameText;
    [SerializeField] private TextMeshProUGUI goldNumberText;

    public void ExitGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("LobbyScene");
    }

    public void OnGameClear(int totalGold, string stageName)
    {
        gameObject.gameObject.SetActive(true);
        stageNameText.text = stageName;
        goldNumberText.text = totalGold.ToString();
    }
    
}
