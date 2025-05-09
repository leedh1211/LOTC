using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotEffect : IWeaponApplicable
{

    public void ApplyToWeapon(SkillApplyContext context)
    {
        context.weaponHandler.projectileCount += 1; //값 증가 
        Debug.Log(" MultiShot 적용됨! 현재 화살 수: " + context.weaponHandler.projectileCount);
    }

}
