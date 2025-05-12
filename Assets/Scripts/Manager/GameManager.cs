using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int testGold;
    
    [SerializeField] private IntegerEventChannelSO startedMainGame;
    [SerializeField] private IntegerEventChannelSO rootedGold;
    [SerializeField] private TransformEventChannelSO rooting;
    [SerializeField] private Transform testRootingTarget;

    
    [SerializeField] private IntegerVariableSO selectedStageLevel;

    protected override void Awake()
    {
        base.Awake();

        rootedGold.OnEventRaised += (gold) => testGold += gold;
        
        startedMainGame.OnEventRaised += LoadMainGame;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            rooting.Raise(testRootingTarget);
        }
    }

    void LoadMainGame(int level)
    {
        selectedStageLevel.RuntimeValue = level;
        
        SceneManager.LoadScene("Test2MainGame");
    }
}
