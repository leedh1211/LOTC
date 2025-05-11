using JetBrains.Annotations;
using Monster.ScriptableObject;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterSkillData", menuName = "ScriptableObjects/MonsterSkillData")]
public class MonsterSkillData : ScriptableObject
{
    public string skillName;
    public MonsterSkillType Type;
    public float AttackRank;
    public float Cooldown;
    public float Damage;
    public int Range;
    [CanBeNull] public GameObject projectile;
    [CanBeNull] public GameObject GroundEffectPrefab;
}
