using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/Data/Stage Data")]
public class StageDataSO : ScriptableObject
{
    [System.Serializable]
    public class StageInfo
    {
        public List<TileMapData> Maps;
    }
    
    public string StageName => stageName;
    public Sprite MainImage => mainImage;
    public AnimatorOverrideController MainAnimator => mainAnimator;
    
    public List<StageInfo> Stages => stages;


    [Space(10f)] 
    [SerializeField] private string stageName;
    
    [Space(10f)]
    [SerializeField] private Sprite mainImage;
    
    [SerializeField] private AnimatorOverrideController mainAnimator;
    
    [Space(10f)]
    [SerializeField] private List<StageInfo> stages;
}
