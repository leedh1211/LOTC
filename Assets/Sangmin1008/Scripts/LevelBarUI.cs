using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private IntegerEventChannelSO expChangedEvent;
    [SerializeField] private VoidEventChannelSO levelUpEvent;
    
    private int _currentExp = 0;
    private int _maxExp = 100;
    private Coroutine _sliderCoroutine;
    private bool _isLevelingUp = false;

    private void OnEnable()
    {
        expChangedEvent.OnEventRaised += SliderUpdate;
    }

    private void OnDisable()
    {
        expChangedEvent.OnEventRaised -= SliderUpdate;
    }

    private void Start()
    {
        slider.minValue = 0;
        slider.maxValue = _maxExp;
        slider.value = _currentExp;
    }

    private void SliderUpdate(int exp)
    {
        if (_sliderCoroutine != null)
        {
            StopCoroutine(_sliderCoroutine);
            _sliderCoroutine = null;
        }

        int targetExp = _currentExp + exp;
        
        if (targetExp >= _maxExp)
        {
            int overflowExp = targetExp - _maxExp;
            _sliderCoroutine = StartCoroutine(SmoothSliderUpdate(_maxExp, overflowExp));
        }
        else
        {
            _sliderCoroutine = StartCoroutine(SmoothSliderUpdate(targetExp, 0));
        }
    }

    private IEnumerator SmoothSliderUpdate(int targetExp, int overflowExp)
    {
        float duration = 0.7f;
        float elapsed = 0f;

        float startValue = slider.value;
        float targetValue = targetExp;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, targetValue, elapsed / duration);
            slider.value = newValue;
            yield return null;
        }

        slider.value = targetValue;
        _currentExp = targetExp;

        if (_currentExp >= _maxExp && !_isLevelingUp)
        {
            _isLevelingUp = true;
            yield return LevelUpRoutine(overflowExp);
            _isLevelingUp = false;
        }

        _sliderCoroutine = null;
    }
    
    private IEnumerator LevelUpRoutine(int overflowExp)
    {
        levelUpEvent.Raise();
        
        yield return new WaitForSeconds(0.2f);
        
        _currentExp = 0;
        slider.value = 0;
        
        if (overflowExp > 0)
        {
            yield return new WaitForSeconds(0.1f);
            
            _isLevelingUp = false;
            SliderUpdate(overflowExp);
        }
    }
}
