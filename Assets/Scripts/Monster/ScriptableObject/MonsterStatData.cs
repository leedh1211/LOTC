using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterStatData", menuName = "ScriptableObjects/MonsterStatData")]
public class MonsterStatData : ScriptableObject
{
    public float maxhealth;
    public float attackPower;
    public float moveSpeed;
    public float attackRange;
    public float skillCooldown;
    public float knockbackDuration;
    public float knockbackCooldown;
}