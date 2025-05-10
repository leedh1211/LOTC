using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMainUI : MonoBehaviour
{
    [SerializeField] private List<RectTransform> mainItemList;

    private void Awake()
    {
        for (int i = 0; i < mainItemList.Count; i++)
        {
            mainItemList[i].sizeDelta = new(Screen.width, Screen.height);
        }
    }
}
