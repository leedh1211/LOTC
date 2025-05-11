using System.Collections;
using UnityEngine;

public class CustomizeController : MonoBehaviour
{
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private CustomizeDataTable customizeDataTable;
    private PlayerData playerData;

    private void Awake()
    {
        playerData = GetComponent<Player>().PlayerData;
    } 

    public void BuyCustomizeItem(int customizeItemId)
    {
        if(playerData.OwnedCustomizeItem[customizeItemId - 1] == true)
        {
            Debug.Log("�̹� ������ �������Դϴ�");
        }
        playerData.OwnedCustomizeItem[customizeItemId - 1] = true;
    }
    public void BuyCustomizeItem(string customName)
    {
        if (customizeDataTable.TryGetCustomizeData(customName, out var data))
        {
            if (playerData.OwnedCustomizeItem[data.Id - 1] == true)
            {
                Debug.Log("�̹� ������ �������Դϴ�");
            }
            playerData.OwnedCustomizeItem[data.Id - 1] = true;
        }
            
    }
    public void SetCustomize(int customizeItemId)
    {
        if (playerData.OwnedCustomizeItem[customizeItemId - 1] == true)
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
            if (playerData.OwnedCustomizeItem[data.Id - 1])
                playerVisual.SetSpriteLibrary(data.SpriteLibraryAsset);
            else
                Debug.Log("�������� ���� �������Դϴ�");
        }
        else
            Debug.Log("�ش� Ŀ���͸���¡�� �������� �ʽ��ϴ�");
    }

}
