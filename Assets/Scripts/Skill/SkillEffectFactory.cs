using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillEffectFactory
{
    public static ISKillEffect CreateEffect(SkillType type)
    {
        switch (type)
        {
            case SkillType.State:
                return new HealEffect();
            case SkillType.ChangeArrow:
                return null;
            case SkillType.Shot:
                return new MultiShotEffect();
            case SkillType.Orbit:
                return new OrbitEffect();
            default:
                return new MultiShotEffect();

        }   
    }
   
}
