using System;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePopup : MonoBehaviour
{
    [SerializeField] private Button submitBtn;
    [SerializeField] private Button cancelBtn;

    public void Init(int id,Action<int> action)
    {
        gameObject.SetActive(true);
        submitBtn.onClick.RemoveAllListeners();
        cancelBtn.onClick.RemoveAllListeners();
        submitBtn.onClick.AddListener(()=>
        {
            action?.Invoke(id);
            gameObject.SetActive(false);
        });
        cancelBtn.onClick.AddListener(()=>gameObject.SetActive(false));
    }
}
