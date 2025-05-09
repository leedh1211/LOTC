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
            Debug.Log("�ش� Ŀ���͸���¡�� �������� �ʽ��ϴ�");
    }

}
