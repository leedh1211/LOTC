using Manager;
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

    [SerializeField] private IntegerVariableSO gold;
    [SerializeField] private VoidEventChannelSO onGoldChanged;
    void Start()
    {
        if (customizeDataTable.TryGetCustomizeData(currentCustomId.RuntimeValue, out var image))
            previewImage.sprite = image.PreviewImage;
        
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
                previewImage.sprite = data.PreviewImage;
                currentCustomId.RuntimeValue = id;
                SaveManager.Instance.Save();
            }
            else
                Debug.Log("�ش� Ŀ���͸���¡�� �������� �ʽ��ϴ�");
        }
        else
            purchasePopup.Init(id,OnPurchase);
    }
    private void OnPurchase(int id)
    {
        if (!customizeDataTable.TryGetCustomizeData(id, out var data))
            return;
        if (gold.RuntimeValue < data.Price)
        {
            Debug.Log("골드 부족");
            return;
        }
        AchievementManager.Instance.AddProgress(2, 1);
        gold.RuntimeValue -= data.Price;
        AchievementManager.Instance.ChangeProgress(6,gold.RuntimeValue);
        ownedCustomItem.RuntimeValue[id - 1] = true;
        SaveManager.Instance.Save();
        onGoldChanged.Raise();
    }
}
