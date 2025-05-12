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
            Debug.Log("�̹� ������ �������Դϴ�");
        }
        OwnedCustomizeItem.RuntimeValue[customizeItemId - 1] = true;
    }
    public void BuyCustomizeItem(string customName)
    {
        if (customizeDataTable.TryGetCustomizeData(customName, out var data))
        {
            if (OwnedCustomizeItem.RuntimeValue[data.Id - 1])
            {
                Debug.Log("�̹� ������ �������Դϴ�");
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
                Debug.Log("�ش� Ŀ���͸���¡�� �������� �ʽ��ϴ�");
        }
        else
            Debug.Log("�������� ���� �������Դϴ�");
    }
    public void SetCustomize(string customName)
    {
        if (customizeDataTable.TryGetCustomizeData(customName, out var data))
        {
            if (OwnedCustomizeItem.RuntimeValue[data.Id - 1])
                playerVisual.SetSpriteLibrary(data.SpriteLibraryAsset);
            else
                Debug.Log("�������� ���� �������Դϴ�");
        }
        else
            Debug.Log("�ش� Ŀ���͸���¡�� �������� �ʽ��ϴ�");
    }

}
