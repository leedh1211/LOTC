using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomizePageSlot : MonoBehaviour
{
    private CustomizeData data;
    private Button button;
    [SerializeField] private TextMeshProUGUI goldTxt;
    [SerializeField] private Image iconImage;

    public void Init(CustomizeData customizeData, bool isbought, Action<int> onClickAction = null)
    {
        button = GetComponent<Button>();
        goldTxt.text = isbought ? "구매 완료" : customizeData.Price.ToString();
        data = customizeData;

        iconImage.sprite = data.IconImage;
        
        iconImage.SetNativeSize();
        
        button.onClick.AddListener(() =>
        {
            onClickAction?.Invoke(data.Id);
        });
    }
    public void MarkAsPurchased()
    {
        goldTxt.text =  "구매 완료";
    }
}
