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
            Debug.Log("�ش� Ŀ���͸���¡�� �������� �ʽ��ϴ�");
    }
}
