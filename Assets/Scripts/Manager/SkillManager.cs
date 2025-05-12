using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;


public class SkillManager : Singleton<SkillManager> //�̱���
{
    private List<SkillData> LearnSkills = new List<SkillData>();

    private HashSet<SkillType> learnedSKillType = new HashSet<SkillType>();


    public void LearnSkill(SkillData data ,SkillApplyContext context)
    {
        if (learnedSKillType.Contains(data.type) && data.type == SkillType.Orbit)
        {
            return;
        }
        learnedSKillType.Add(data.type);
        ISKillEffect effect = SkillEffectFactory.CreateEffect(data.type);

        if(effect == null)
        {
            Debug.Log("there is no skill");
        }

        ApplySkillEffect(effect,context); //Apply skill

    }


    private void ApplySkillEffect(ISKillEffect effect , SkillApplyContext context)
    {
        if(effect is IPlayerApplicable player)
        {
            player.ApplyToPlayer(context);
        }

        if(effect is IWeaponApplicable weapon)
        {
            weapon.ApplyToWeapon(context);
        }

     

    }
   
   

   
    private void TryCombine(SkillData newSkill, SkillApplyContext context)
    {
        foreach (SkillType existing in learnedSKillType)
        {
            if (newSkill.combinable.Contains(existing) && newSkill.resultCombo.HasValue)
            {
                SkillType result = newSkill.resultCombo.Value;
                

                Debug.Log($"[조합 발생] {newSkill.type} + {existing} → {result}");

                ISKillEffect comboEffect = SkillEffectFactory.CreateEffect(result);
                if (comboEffect != null)
                {
                    ApplySkillEffect(comboEffect, context);
                    learnedSKillType.Add(result);
                }
            }
        }
    }
   
}
