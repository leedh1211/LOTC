using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReflectEffect : ISKillEffect , IWeaponApplicable
{
    public void ApplyToWeapon(SkillApplyContext context)
    {
        context.weaponHandler.ReflectCount+=1;
    }
}
