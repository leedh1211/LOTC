using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMainUI : MonoBehaviour
{
    [SerializeField] private LobbyController lobbyController;
    
    [SerializeField] private HorizontalSnapScrollView snapScrollView;
    
    [SerializeField] private List<RectTransform> pageList;
    
    [SerializeField] private TextMeshProUGUI stageText;
    
    [SerializeField] private Button startButton;

    [SerializeField] private IntegerEventChannelSO selectedStageLevel;
    [SerializeField] private IntegerEventChannelSO startedMainGame;
    private void Awake()
    {
        SetStageInfo(lobbyController.testSelectStage);
        
        for (int i = 0; i < pageList.Count; i++)
        {
            pageList[i].sizeDelta = new(Screen.width, Screen.height);
        }
        
        startButton.onClick.AddListener(() => startedMainGame.Raise(lobbyController.testSelectStage));
    }

    private void Start()
    {
        snapScrollView.DirectUpdateItemList(1);

        selectedStageLevel.OnEventRaised += SetStageInfo;
    }

    private void OnDestroy()
    {
        selectedStageLevel.OnEventRaised -= SetStageInfo;
    }

    void SetStageInfo(int level) => stageText.text = (level + 1).ToString();

}
