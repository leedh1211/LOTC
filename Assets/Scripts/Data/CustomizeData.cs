using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Objects/Customize Data")]
public class CustomizeData : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string name;
    [SerializeField] private SpriteLibraryAsset spriteLibraryAsset;
    [SerializeField] private Sprite iconImage;

    public int Id => id;
    public string Name => name;
    public SpriteLibraryAsset SpriteLibraryAsset => spriteLibraryAsset;
    public Sprite IconImage => iconImage;

}
