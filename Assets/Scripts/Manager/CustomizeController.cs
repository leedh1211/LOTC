using UnityEngine;

public class CustomizeController : MonoBehaviour
{
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private CustomizeDataTable customizeDataTable;
    [SerializeField] private BitArrayVariableSO OwnedCustomizeItem;

    public void BuyCustomizeItem(int customizeItemId)
    {
        if(OwnedCustomizeItem.RuntimeValue[customizeItemId - 1])
        {
            Debug.Log("이미 구매한 아이템입니다");
        }
        OwnedCustomizeItem.RuntimeValue[customizeItemId - 1] = true;
    }
    public void BuyCustomizeItem(string customName)
    {
        if (customizeDataTable.TryGetCustomizeData(customName, out var data))
        {
            if (OwnedCustomizeItem.RuntimeValue[data.Id - 1])
            {
                Debug.Log("이미 구매한 아이템입니다");
            }
            OwnedCustomizeItem.RuntimeValue[data.Id - 1] = true;
        }
            
    }
    public void SetCustomize(int customizeItemId)
    {
        if (OwnedCustomizeItem.RuntimeValue[customizeItemId - 1])
        {
            if (customizeDataTable.TryGetCustomizeData(customizeItemId, out var data))
                playerVisual.SetSpriteLibrary(data.SpriteLibraryAsset);
            else
                Debug.Log("해당 커스터마이징은 존재하지 않습니다");
        }
        else
            Debug.Log("구매하지 않은 아이템입니다");
    }
    public void SetCustomize(string customName)
    {
        if (customizeDataTable.TryGetCustomizeData(customName, out var data))
        {
            if (OwnedCustomizeItem.RuntimeValue[data.Id - 1])
                playerVisual.SetSpriteLibrary(data.SpriteLibraryAsset);
            else
                Debug.Log("구매하지 않은 아이템입니다");
        }
        else
            Debug.Log("해당 커스터마이징은 존재하지 않습니다");
    }

}
