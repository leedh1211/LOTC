using UnityEngine;
using UnityEngine.UI;

public class CustomizePageSlot : MonoBehaviour
{
    private CustomizeData data;
    private Button button;
    private Image image;


    public void Init(CustomizeData customizeData)
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        data = customizeData;

        image.sprite = data.IconImage;
    }
}
