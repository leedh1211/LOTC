using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearUI : MonoBehaviour
{
    [SerializeField] private GameObject gameClearPanel;

    public void ExitGame()
    {
        gameClearPanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("LobbyScene");
    }
}
