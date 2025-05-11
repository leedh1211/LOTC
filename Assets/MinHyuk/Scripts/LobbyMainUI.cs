using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyMainUI : MonoBehaviour
{
    [SerializeField] private LobbyController lobbyController;
    
    [SerializeField] private HorizontalSnapScrollView snapScrollView;
    
    [SerializeField] private List<RectTransform> mainItemList;
    [SerializeField] private TextMeshProUGUI stageText;

    [SerializeField] private IntegerEventChannelSO selectStageChannel;
    private void Awake()
    {
        SetStageInfo(lobbyController.testSelectStage);
        
        for (int i = 0; i < mainItemList.Count; i++)
        {
            mainItemList[i].sizeDelta = new(Screen.width, Screen.height);
        }
    }

    private void Start()
    {
        snapScrollView.DirectUpdateItemList(1);

        selectStageChannel.OnEventRaised += SetStageInfo;
    }

    private void OnDestroy()
    {
        selectStageChannel.OnEventRaised -= SetStageInfo;
    }

    void SetStageInfo(int level) => stageText.text = (level + 1).ToString();

}
