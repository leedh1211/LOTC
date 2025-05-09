using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SkillManager : MonoBehaviour
{
     public static SkillManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
   
    public void ApplySkill(object effectInstance,SkillApplyContext context)
    {
        if (effectInstance is IPlayerApplicable playerEffect)
        {
            playerEffect.ApplyToPlayer(context);
        #if UNITY_EDITOR
            Debug.Log("플레이어에게 스킬 적용됨");
        #endif
        }

        if (effectInstance is IWeaponApplicable weaponEffect)
        {
            weaponEffect.ApplyToWeapon(context);
        #if UNITY_EDITOR
            Debug.Log("무기에 스킬 적용됨");
        #endif

        }
    }
}
