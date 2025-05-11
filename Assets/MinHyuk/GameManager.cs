using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private IntegerEventChannelSO startedMainGame;

    protected override void Awake()
    {
        base.Awake();

        startedMainGame.OnEventRaised += LoadMainGame;
    }

    void LoadMainGame(int level)
    {
        SceneManager.LoadScene("MainGameScene");
    }
}
