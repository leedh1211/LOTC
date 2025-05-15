using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Customize Data Table")]
public class CustomizeDataTable : ScriptableObject
{
    [SerializeField] private List<CustomizeData> datas;

    public bool TryGetCustomizeData(string targetName, out CustomizeData targetData)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i].Name == targetName)
            {
                targetData = datas[i];

                return true;
            }
        }
        targetData = null;

        return false;
    }
    public bool TryGetCustomizeData(int id, out CustomizeData targetData)
    {
        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i].Id == id)
            {
                targetData = datas[i];

                return true;
            }
        }
        targetData = null;

        return false;
    }
    public int GetCustomizeDataCount()
    {
        return datas.Count;
    }
}
