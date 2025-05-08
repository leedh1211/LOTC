using System.Collections;
using System.Collections.Generic;
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
    public void ApplySkill(object effectInstance, Player player, WeaponHandler weaponHandler)
    {
        if (effectInstance is IPlayerApplicable playerEffect)
        {
            playerEffect.ApplyToPlayer(player);
        #if UNITY_EDITOR
            Debug.Log("�÷��̾�� ��ų �����");
        #endif
        }

        if (effectInstance is IWeaponApplicable weaponEffect)
        {
            weaponEffect.ApplyToWeapon(weaponHandler);
        #if UNITY_EDITOR
            Debug.Log("���⿡ ��ų �����");
        #endif

        }
    }
}
