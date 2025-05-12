using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData")]
public class StageData : ScriptableObject
{
    [System.Serializable]
    public class StageInfo
    {
        public List<TileMapData> Maps;
    }

    [SerializeField] private List<StageInfo> stages;
    public List<StageInfo> Stages
    {
        get { return stages; }
    }
    
    
}
