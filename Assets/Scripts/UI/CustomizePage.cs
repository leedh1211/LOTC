using UnityEngine;
using UnityEngine.UI;

public class CustomizePage : MonoBehaviour
{
    [SerializeField] private Image previewImage;
    [SerializeField] private Transform itemsParent;
    [SerializeField] private PurchasePopup purchasePopup;
    [SerializeField] private CustomizePageSlot slotPrefab;
    [SerializeField] private CustomizeDataTable customizeDataTable;
    [SerializeField] private IntegerVariableSO currentCustomId;
    [SerializeField] private BitArrayVariableSO ownedCustomItem;
    void Start()
    {
        if (customizeDataTable.TryGetCustomizeData(currentCustomId.RuntimeValue, out var image))
            previewImage.sprite = image.IconImage;
        for (int i = 0; i < customizeDataTable.GetCustomizeDataCount();i++)
        {
            var slot = Instantiate<CustomizePageSlot>(slotPrefab, itemsParent);
            if(customizeDataTable.TryGetCustomizeData(i + 1,out var data))
            {
                slot.Init(data,OnSlotClicked);
            }
        }
    }

    private void OnSlotClicked(int id)
    {
        if (ownedCustomItem.RuntimeValue[id-1])
        {
            if (customizeDataTable.TryGetCustomizeData(id, out var data))
            {
                previewImage.sprite = data.IconImage;
                currentCustomId.RuntimeValue = id;
                SaveManager.Instance.Save();
            }
            else
                Debug.Log("해당 커스터마이징은 존재하지 않습니다");
        }
        else
            purchasePopup.Init(id,OnPurchase);
    }
    private void OnPurchase(int id)
    {
        ownedCustomItem.RuntimeValue[id - 1] = true;
        SaveManager.Instance.Save();
    }
}
