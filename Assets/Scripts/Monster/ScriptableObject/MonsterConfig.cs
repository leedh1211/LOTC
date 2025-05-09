using System.Collections;
using System.Collections.Generic;
using Monster.ScriptableObject;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterConfig", menuName = "ScriptableObjects/MonsterConfig")]
public class MonsterConfig : ScriptableObject
{
    public MonsterName monsterName;
    public MonsterType monsterType;
    public MonsterStatData monsterStatData;
    public MonsterSkillData[] skillData;
    public AnimatorOverrideController AnimatorOverrideController;
    public Sprite SpriteOverride;
    public Collider2D ColliderOverride;
}