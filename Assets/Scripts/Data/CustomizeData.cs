using UnityEngine;
using UnityEngine.U2D.Animation;

[CreateAssetMenu(menuName = "Scriptable Objects/Customize Data")]
public class CustomizeData : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string name;
    [SerializeField] private SpriteLibraryAsset spriteLibraryAsset;
    [SerializeField] private Sprite iconImage;
    [SerializeField] private Sprite previewImage;

    public int Id => id;
    public string Name => name;
    public SpriteLibraryAsset SpriteLibraryAsset => spriteLibraryAsset;
    public Sprite IconImage => iconImage;
    public Sprite PreviewImage => previewImage;

}
