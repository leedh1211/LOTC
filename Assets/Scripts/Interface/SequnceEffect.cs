using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequnceEffect : ISKillEffect, IWeaponApplicable
{

    public void ApplyToWeapon(SkillApplyContext context)
    {
        context.weaponHandler.IsSequnce = true;
        
    }
}
