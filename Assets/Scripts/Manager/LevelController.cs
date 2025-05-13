using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int maxExp = 100;

    [SerializeField] private IntegerEventChannelSO expGainedEvent;
    [SerializeField] private IntegerEventChannelSO expChangedEvent;

   
    private int _currentExp = 0;

    private void OnEnable()
    {
        expGainedEvent.OnEventRaised += OnExpGained;
    }

    private void OnDisable()
    {
        expGainedEvent.OnEventRaised -= OnExpGained;
    }

    private void Start()
    {
        Init(0);
    }

   
    private void OnExpGained(int exp)
    {
        expChangedEvent.Raise(exp);
        
        _currentExp = (_currentExp + exp) % maxExp;
    }

    private void Init(int startExp)
    {
        _currentExp = Mathf.Clamp(startExp, 0, maxExp);
        expChangedEvent.Raise(_currentExp);
    }

    [ContextMenu("Up")]
    private void Up()
    {
        OnExpGained(70);
    }
}
