using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKillTestButton : MonoBehaviour
{
    [SerializeField] private SkillData skillData;
    [SerializeField] private Player player;

    public void OnClickTestSkill()
    {
        if (player != null && skillData != null)
        {
            player.SetSkillData(skillData);


        }
    }


}
