using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private IntegerEventChannelSO playerHitEvent;
    [SerializeField] private IntegerEventChannelSO playerHealEvent;
    [SerializeField] private VoidEventChannelSO playerDeathEvent;

    [SerializeField] private int maxHp = 100;
    private int _currentHp;
    private Coroutine _sliderCoroutine;

    private void OnEnable()
    {
        playerHitEvent.OnEventRaised += TakeDamage;
        playerHealEvent.OnEventRaised += Heal;
    }

    private void OnDisable()
    {
        playerHitEvent.OnEventRaised -= TakeDamage;
        playerHealEvent.OnEventRaised -= Heal;
    }

    private void Start()
    {
        _currentHp = maxHp;

        healthBar.fillAmount = _currentHp / maxHp;
    }

    private void TakeDamage(int damage)
    {
        int newHp = Mathf.Max(_currentHp - damage, 0);

        if (_sliderCoroutine != null)
            StopCoroutine(_sliderCoroutine);

        _sliderCoroutine = StartCoroutine(SmoothSliderUpdate(_currentHp, newHp));

        if (newHp <= 0)
        {
            playerDeathEvent.Raise();
        }
    }

    private void Heal(int amount)
    {
        int newHp = Mathf.Min(_currentHp + amount, maxHp);

        if (_sliderCoroutine != null)
            StopCoroutine(_sliderCoroutine);

        _sliderCoroutine = StartCoroutine(SmoothSliderUpdate(_currentHp, newHp));
    }

    private IEnumerator SmoothSliderUpdate(int from, int to)
    {
        float duration = 0.4f;
        float elapsed = 0f;

        float startValue = from;
        float targetValue = to;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, targetValue, elapsed / duration);
            healthBar.fillAmount = newValue;
            yield return null;
        }

        healthBar.fillAmount = targetValue;
        _currentHp = to;
        _sliderCoroutine = null;
    }
    
    [ContextMenu("PlayerHit!!")]
    private void PlayerHit()
    {
        playerHitEvent.Raise(20);
    }
}
