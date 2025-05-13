using System;
using UnityEngine;
using UnityEngine.UI;

public class CustomizePageSlot : MonoBehaviour
{
    private CustomizeData data;
    private Button button;
    
    [SerializeField] private Image iconImage;

    public void Init(CustomizeData customizeData, Action<int> onClickAction = null)
    {
        button = GetComponent<Button>();
        data = customizeData;

        iconImage.sprite = data.IconImage;
        
        iconImage.SetNativeSize();
        
        button.onClick.AddListener(() =>
        {
            onClickAction?.Invoke(data.Id);
        });
    }
}
