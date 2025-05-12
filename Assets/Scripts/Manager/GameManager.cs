using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int testGold;
    
    [SerializeField] private IntegerEventChannelSO rootedGold;
    [SerializeField] private TransformEventChannelSO rooting;
    [SerializeField] private Transform testRootingTarget;

    protected override void Awake()
    {
        base.Awake();

        rootedGold.OnEventRaised += (gold) => testGold += gold;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            rooting.Raise(testRootingTarget);
        }
    }

    public void StartMainGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
