using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotEffect : ISKillEffect, IWeaponApplicable
{

    public void ApplyToWeapon(SkillApplyContext context)
    {
        context.weaponHandler.projectileCount += 1; //�� ���� 
        Debug.Log(" MultiShot �����! ���� ȭ�� ��: " + context.weaponHandler.projectileCount);
    }

}
