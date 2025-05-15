using UnityEngine;

public class CustomizeController : MonoBehaviour
{
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private CustomizeDataTable customizeDataTable;
    [SerializeField] private IntegerVariableSO currentCustomId;

    private void Start()
    {
        SetCustomize();
    }

    public void SetCustomize()
    {
        if (customizeDataTable.TryGetCustomizeData(currentCustomId.RuntimeValue, out var data))
            playerVisual.SetSpriteLibrary(data.SpriteLibraryAsset);
        else
            Debug.Log("해당 커스터마이징은 존재하지 않습니다");
    }
}
