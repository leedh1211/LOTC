using System.Collections;
using System.Collections.Generic;
using Monster.ScriptableObject;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterConfig", menuName = "Scriptable Objects/MonsterConfig")]
public class MonsterConfig : ScriptableObject
{
    public MonsterName monsterName;
    public MonsterType monsterType;
    public MonsterStatData monsterStatData;
    public BaseMonsterSkillData[] skillData;
    public AnimatorOverrideController AnimatorOverrideController;
    public Sprite SpriteOverride;
    public int goldQuantity;
    public bool isFly;
    public float colliderSizeX = 0.8f;
    public float colliderSizeY = 1.2f;
    public float colliderOffX = 0;
    public float colliderOffY = -0.2f;
}