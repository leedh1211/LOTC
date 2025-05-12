using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;


public class SkillManager : Singleton<SkillManager> //�̱���
{
    private List<SkillData> LearnSkills = new List<SkillData>();

    public void LearnSkill(SkillData data ,SkillApplyContext context)
    {
        ISKillEffect effect = SkillEffectFactory.CreateEffect(data.type);

        if(effect == null)
        {
            Debug.Log("there is no skill");
        }

        ApplySkill(effect,context); //Apply skill

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
   
   

   //will Erase Funtion
    public void ApplySkill(object effectInstance,SkillApplyContext context)
    {
        if (effectInstance is IPlayerApplicable playerEffect)
        {
            playerEffect.ApplyToPlayer(context);
        #if UNITY_EDITOR
            Debug.Log("�÷��̾�� ��ų �����");
        #endif
        }

        if (effectInstance is IWeaponApplicable weaponEffect)
        {
            weaponEffect.ApplyToWeapon(context);
        #if UNITY_EDITOR
            Debug.Log("���⿡ ��ų �����");
        #endif

        }
    }

/*    private void TryCombine(SkillData newSkill, SkillApplyContext context)
    {
        foreach (SkillType existing in LearnSkills)
        {
            if (newSkill.combinableWith.Contains(existing) && newSkill.resultCombo.HasValue)
            {
                SkillType result = newSkill.resultCombo.Value;
                if (learnedSkillTypes.Contains(result)) return;

                Debug.Log($"[조합 발생] {newSkill.type} + {existing} → {result}");

                ISkillEffect comboEffect = SkillEffectFactory.CreateEffect(result);
                if (comboEffect != null)
                {
                    ApplyEffect(comboEffect, context);
                    learnedSkillTypes.Add(result);
                }
            }
        }
    }
    */
}
