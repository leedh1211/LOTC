using System;
using System.Collections.Generic;
using Monster.ScriptableObject;
using UnityEditor;
using UnityEngine;

namespace Monster
{
    public class MonsterFactory : MonoBehaviour
    {
        public GameObject monsterBasePrefab;
        public List<MonsterConfig> monsterConfigs;
        public GameObject player;

        public void Start()
        {
            // SpawnMonster(MonsterName.Mob1, new Vector2(0f, 12f), player);
            SpawnMonster(MonsterName.Boss2, new Vector2(1f, 15f) , player );
        }

        public GameObject SpawnMonster(MonsterName name, Vector2 spawnPos, GameObject player)
        {
            var config = monsterConfigs.Find(c => c.monsterName == name);
            var monster = Instantiate(monsterBasePrefab, spawnPos, Quaternion.identity);
            monster.GetComponent<MonsterController>().Init(config, player);
            return monster;
        }
    }
}