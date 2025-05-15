using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISKillEffect {}


public interface IPlayerApplicable
{
    void ApplyToPlayer(SkillApplyContext context);
}

public interface IWeaponApplicable
{
    void ApplyToWeapon(SkillApplyContext context);
}




