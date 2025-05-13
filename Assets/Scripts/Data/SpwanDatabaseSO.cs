using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "SpawnDatabase", menuName = "Stage/SpawnDatabase")]
    public class SpawnDatabaseSO : ScriptableObject
    {
        public List<StageRoomSpawnDataSO> roomSpawnDataList;

        public StageRoomSpawnDataSO GetSpawnData(int stageLevel, int roomIndex)
        {
            return roomSpawnDataList.Find(data =>
                data.stageLevel == stageLevel && data.roomIndex == roomIndex);
        }
    }
}