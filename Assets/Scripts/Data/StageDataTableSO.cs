using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StageDataTable", menuName = "Scriptable Objects/Data/Stage Data Table")]
public class StageDataTableSO : ScriptableObject
{
    [SerializeField] private StageDataSO[] datas;

    public StageDataSO[] Datas => datas;
}
