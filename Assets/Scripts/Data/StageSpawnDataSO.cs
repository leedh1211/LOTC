using System;
using System.Collections.Generic;
using Monster.ScriptableObject;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "StageRoomSpawnData", menuName = "Stage/RoomMonsterSpawnData")]
    public class StageRoomSpawnDataSO : ScriptableObject
    {
        public int stageLevel; // 스테이지 번호
        public int roomIndex; // 방 번호
        public List<MonsterSpawnInfo> spawns; // 스폰 정보 리스트
    }

    [Serializable]
    public class MonsterSpawnInfo
    {
        public MonsterName monsterName;
        public Vector2 spawnPosition;
    }
}