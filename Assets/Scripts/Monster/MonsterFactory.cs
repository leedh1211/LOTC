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
            SpawnMonster(MonsterName.Mob4, new Vector2(-3f, 12f) , player );
            // SpawnMonster(MonsterName.FlyMob1, new Vector2(-1f, 12f) , player );
            // SpawnMonster(MonsterName.FlyMob2, new Vector2(0f, 12f) , player );
            // SpawnMonster(MonsterName.Mob3, new Vector2(1f, 12f) , player );
            // SpawnMonster(MonsterName.Mob2, new Vector2(2f, 12f) , player );
            // SpawnMonster(MonsterName.Mob1, new Vector2(-2f, 11f) , player );
            // SpawnMonster(MonsterName.FlyMob1, new Vector2(-1f, 11f) , player );
            // SpawnMonster(MonsterName.FlyMob2, new Vector2(0f, 11f) , player );
            // SpawnMonster(MonsterName.Mob3, new Vector2(1f, 11f) , player );
        }

        [MenuItem("Tolls/SpwanMonster")]
        public GameObject SpawnMonster(MonsterName name, Vector2 spawnPos, GameObject player)
        {
            var config = monsterConfigs.Find(c => c.monsterName == name);
            var monster = Instantiate(monsterBasePrefab, spawnPos, Quaternion.identity);
            monster.GetComponent<MonsterController>().Init(config, player);
            return monster;
        }
    }
}