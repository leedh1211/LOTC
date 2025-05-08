using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObject/StageData")]
public class StageData : ScriptableObject
{
    [System.Serializable]
    public class StageInfo
    {
        public int MainStage;
        public List<TileMapData> Maps;
    }

    [SerializeField] private List<StageInfo> stages;
    public List<StageInfo> Stages
    {
        get { return stages; }
    }
}
