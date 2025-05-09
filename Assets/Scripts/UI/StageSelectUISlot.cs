using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUISlot : MonoBehaviour
{
    [SerializeField] private string stageName;
    [SerializeField] private Image spotLightImage;
    [SerializeField] private Image lockedImage;


    public void Init(bool isUnlocked, bool isSpotLight)
    {
        lockedImage.enabled = !isUnlocked;

        spotLightImage.enabled = isSpotLight;
    }
    
    public void OnSelected(out string stageName)
    {
        stageName = this.stageName;
        spotLightImage.enabled = true;
    }

    public void OnDeselected()
    {
        spotLightImage.enabled = false;
    }
}
