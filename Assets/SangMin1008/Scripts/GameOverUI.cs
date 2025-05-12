using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    public void RetryGame()
    {
        gameOverPanel.SetActive(false);
        Debug.Log("Retry Stage");
    }

    public void ExitGame()
    {
        gameOverPanel.SetActive(false);
        Debug.Log("Lobby");
    }
}
