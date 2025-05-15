using System;
using System.Collections.Generic;
using Monster.ScriptableObject;
using UnityEditor;
using UnityEngine;

namespace Monster
{
    public class MonsterFactory : MonoBehaviour
    {
        public MonsterController monsterBasePrefab;
        public List<MonsterConfig> monsterConfigs;

        public MonsterController SpawnMonster(MonsterName name, Vector2 spawnPos, GameObject player)
        {
            var config = monsterConfigs.Find(c => c.monsterName == name);
            var monster = Instantiate(monsterBasePrefab, spawnPos, Quaternion.identity);
            monster.Init(config, player);
            return monster;
        }
    }
}