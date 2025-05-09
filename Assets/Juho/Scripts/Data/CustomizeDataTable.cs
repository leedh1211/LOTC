using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Customize Data Table")]
public class CustomizeDataTable : ScriptableObject
{
    [SerializeField] private List<CustomizeData> customizeData;
}
