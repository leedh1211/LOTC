using JetBrains.Annotations;
using Monster.ScriptableObject;
using UnityEngine;

public abstract class BaseMonsterSkillData : ScriptableObject
{
    public string skillName;
    public float Cooldown;
    public float AfterDelay;
    
    public abstract void Excute(MonsterConfig MonsterConfig , Transform self,Transform target);
}
