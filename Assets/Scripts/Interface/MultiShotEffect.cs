using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotEffect : ISKillEffect, IWeaponApplicable
{

    public void ApplyToWeapon(SkillApplyContext context)
    {
        context.weaponHandler.ProjectileCount += 1; 
    }

}
