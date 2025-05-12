using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMainUI : MonoBehaviour
{
    [SerializeField] private HorizontalSnapScrollView snapScrollView;
    
    [SerializeField] private TextMeshProUGUI stageText;
    
    [SerializeField] private Button startButton;

    [SerializeField] private IntegerVariableSO selectedStageLevel;
    
    private void Awake()
    {
        SetStageInfo(selectedStageLevel.RuntimeValue);
      
        startButton.onClick.AddListener(() => GameManager.Instance.StartMainGame());
    }

    private void Start()
    {
        snapScrollView.DirectUpdateItemList(1);
    }

    public void SetStageInfo(int level) => stageText.text = (level + 1).ToString();
}
