using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectMenuUI : MonoBehaviour
{
    [SerializeField] private List<SkillData> skillDatas;

    [SerializeField] private Button skillMenu1;
    [SerializeField] private Button skillMenu2;
    [SerializeField] private Button skillMenu3;

    [SerializeField] private VoidEventChannelSO levelUpEvent;
    [SerializeField] private GameObject skillMenuPanel;


    private void Start()
    {
        levelUpEvent.OnEventRaised += ShowPanel;
       
    }

    private void OnEnable()
    {
       
             
    }
    /*
     * 
    private void OnDisable()
    {
        levelUpEvent.OnEventRaised -= ShowPanel;
        
        skillMenu1.onClick.RemoveAllListeners();
        skillMenu2.onClick.RemoveAllListeners();
        skillMenu3.onClick.RemoveAllListeners();
    }
     */

    private void ShowPanel()
    {
        SettingSkillUI();
        skillMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    
    private void OnSkillSelected(string skillName)
    {
        Debug.Log($"{skillName} 선택 완료!");
        skillMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }


    private void SettingSkillUI()
    {
        RandomSkill();
        SkillData s1 = skillDatas[0];
        SkillData s2 = skillDatas[1];
        SkillData s3 = skillDatas[2];

        skillMenu1.onClick.AddListener(() => OnSkillSelected(s1));
        skillMenu2.onClick.AddListener(() => OnSkillSelected(s2));
        skillMenu3.onClick.AddListener(() => OnSkillSelected(s3));


        skillMenu1.GetComponentInChildren<TextMeshProUGUI>().text = s1.name;
        skillMenu2.GetComponentInChildren<TextMeshProUGUI>().text = s2.name;
        skillMenu3.GetComponentInChildren<TextMeshProUGUI>().text = s3.name;




    }

    private void RandomSkill()
    {
        for (int i = 0; i < skillDatas.Count; i++)
        {
            SkillData temp = skillDatas[i];
            int randomIndex = UnityEngine.Random.Range(i, skillDatas.Count);
            skillDatas[i] = skillDatas[randomIndex];
            skillDatas[randomIndex] = temp;

        }
    }

    private void OnSkillSelected(SkillData selectedSkill)
    {
       

        // 컨텍스트 생성
        var context = new SkillApplyContext
        {
            player = FindObjectOfType<Player>(),
            weaponHandler = FindObjectOfType<WeaponHandler>()
        };

       
        SkillManager.Instance.LearnSkill(selectedSkill, context); //매니저에게 전달

     
        skillMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

}
