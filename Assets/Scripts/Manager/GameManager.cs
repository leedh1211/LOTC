using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    
    [SerializeField] private IntegerEventChannelSO rootedGold;

    protected override void Awake()
    {
        base.Awake();
    }


    public void StartMainGame()
    {
        SceneLoadManager.Instance.LoadScene("GameScene");
    }
}
