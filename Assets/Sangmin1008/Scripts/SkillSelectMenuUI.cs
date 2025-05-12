using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectMenuUI : MonoBehaviour
{
    [SerializeField] private Button skillMenu1;
    [SerializeField] private Button skillMenu2;
    [SerializeField] private Button skillMenu3;

    [SerializeField] private VoidEventChannelSO levelUpEvent;
    [SerializeField] private GameObject skillMenuPanel;

    private void OnEnable()
    {
        levelUpEvent.OnEventRaised += ShowPanel;
        
        skillMenu1.onClick.AddListener(() => OnSkillSelected("Skill 1"));
        skillMenu2.onClick.AddListener(() => OnSkillSelected("Skill 2"));
        skillMenu3.onClick.AddListener(() => OnSkillSelected("Skill 3"));
    }

    private void OnDisable()
    {
        levelUpEvent.OnEventRaised -= ShowPanel;
        
        skillMenu1.onClick.RemoveAllListeners();
        skillMenu2.onClick.RemoveAllListeners();
        skillMenu3.onClick.RemoveAllListeners();
    }

    private void ShowPanel()
    {
        Time.timeScale = 0f;
        skillMenuPanel.SetActive(true);
    }
    
    private void OnSkillSelected(string skillName)
    {
        Time.timeScale = 1f;
        Debug.Log($"{skillName} 선택 완료!");
        skillMenuPanel.SetActive(false);
    }
}
