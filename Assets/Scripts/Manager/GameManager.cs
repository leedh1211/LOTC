using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    
    [SerializeField] private IntegerEventChannelSO rootedGold;
    [SerializeField] private IntegerVariableSO gold;

    protected override void Awake()
    {
        base.Awake();

        rootedGold.OnEventRaised += (inputGold) => gold.RuntimeValue += inputGold;
    }


    public void StartMainGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
