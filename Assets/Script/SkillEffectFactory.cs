using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillEffectFactory
{
    public static object CreateEffect(SkillType type)
    {
        switch (type)
        {
            case SkillType.State:
                return null;
            case SkillType.ChangeArrow:
                return null;
            case SkillType.MultiShot:
                return new MultiShotEffect();
            default:
                return new MultiShotEffect();

        }   
    }
   
}
