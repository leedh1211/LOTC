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
                return new ReflectEffect();
            case SkillType.Shot:
                return new MultiShotEffect();
            case SkillType.Orbit:
                return new OrbitEffect();
            case SkillType.Sequnce:
                return new SequnceEffect();
            default:
                return new MultiShotEffect();

        }   
    }
   
}
