using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private VoidEventChannelSO levelUpEvent;

    private int displayLevel = 1;

    private void OnEnable()
    {
        levelUpEvent.OnEventRaised += OnLevelUp;
    }

    private void OnDisable()
    {
        levelUpEvent.OnEventRaised -= OnLevelUp;
    }

    private void OnLevelUp()
    {
        displayLevel++;
        levelText.text = $"Lv {displayLevel}";
    }

    private void Start()
    {
        levelText.text = $"Lv {displayLevel}";
    }
}
