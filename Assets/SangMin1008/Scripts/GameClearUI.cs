using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearUI : MonoBehaviour
{
    [SerializeField] private GameObject gameClearPanel;
    public void NextGame()
    {
        gameClearPanel.SetActive(false);
        Debug.Log("Next Stage");
    }

    public void ExitGame()
    {
        gameClearPanel.SetActive(false);
        Debug.Log("Lobby");
    }
}
