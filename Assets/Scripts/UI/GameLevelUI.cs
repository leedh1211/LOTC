using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameLevelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private Slider slider;
    
    [SerializeField] private float testGainExp;
    
    [SerializeField] private VoidEventChannelSO onLevelUp;


    private int displayLevel = 1;
    
    private float _currentExp = 0;

    private float _maxExp = 100;
    
    
    private Coroutine _sliderCoroutine;


    private void Start()
    {
        levelText.text = $"Lv {displayLevel}";
        slider.value = 0;
    }

  
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnGainExp(testGainExp);
        }
    }

    private void OnGainExp(float gainExp)
    {
        if (_sliderCoroutine != null)
        {
            StopCoroutine(_sliderCoroutine);
            _sliderCoroutine = null;
        }

        float targetExp = _currentExp + gainExp;
        
        if (targetExp >= _maxExp)
        {
            float overflowExp = targetExp - _maxExp;
            
            _sliderCoroutine = StartCoroutine(SmoothSliderUpdate(_maxExp, overflowExp));
        }
        else
        {
            _sliderCoroutine = StartCoroutine(SmoothSliderUpdate(targetExp, 0));
        }
    }
    
    private IEnumerator SmoothSliderUpdate(float targetExp, float overflowExp)
    {
        float duration = 0.7f;
        
        float elapsed = 0f;

        float startValue = slider.value;
        
        float targetValue = targetExp / _maxExp;
        

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            
            float newValue = Mathf.Lerp(startValue, targetValue, elapsed / duration);
            
            slider.value = newValue;
            
            yield return null;
        }

        slider.value = targetValue;
        
        _currentExp = targetExp;

        if (_currentExp >= _maxExp)
        {
            yield return LevelUpRoutine(overflowExp);
        }

        _sliderCoroutine = null;
    }
    
    private IEnumerator LevelUpRoutine(float overflowExp)
    {
        _currentExp = 0;
        
        slider.value = 0;
        
        levelText.text = $"Lv {displayLevel += 1}";

        onLevelUp.Raise();
        
        yield return new WaitForSeconds(0.2f);
        
        if (overflowExp > 0)
        {
            yield return new WaitForSeconds(0.1f);
            
            OnGainExp(overflowExp);
        }
    }
}
