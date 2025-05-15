using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectUISlot : MonoBehaviour
{
    [SerializeField] private string stageName;
    [SerializeField] private Image lockedImage;


    public void Init(bool isUnlocked, bool isSpotLight)
    {
        lockedImage.enabled = !isUnlocked;
    }
}
