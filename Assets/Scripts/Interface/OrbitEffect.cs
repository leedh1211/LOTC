using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitEffect : ISKillEffect, IPlayerApplicable
{

    public void ApplyToPlayer(SkillApplyContext context)
    {
        context.player.SetOrbitSkill();
    }


}
