using System;
using UnityEngine;
using UnityEngine.UI;

public class CustomizePageSlot : MonoBehaviour
{
    private CustomizeData data;
    private Button button;
    private Image image;

    public void Init(CustomizeData customizeData, Action<int> onClickAction = null)
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        data = customizeData;

        image.sprite = data.IconImage;
        button.onClick.AddListener(() =>
        {
            onClickAction?.Invoke(data.Id);
        });
    }
}
