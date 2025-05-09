using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAttackEffect : IWeaponApplicable
{
    public void ApplyToWeapon(SkillApplyContext context)
    {
        context.weaponHandler.Power += 10;
    }
}
