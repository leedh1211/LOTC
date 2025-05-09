using UnityEngine;

public class CustomizeController : MonoBehaviour
{
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private CustomizeDataTable customizeDataTable;

    public void SetCustomizeByName(string customName)
    {
        if (customizeDataTable.TryGetCustomizeData(customName, out var data))
            playerVisual.SetSpriteLibrary(data.SpriteLibraryAsset);
        else
            Debug.Log("해당 커스터마이징은 존재하지 않습니다");
    }

}
