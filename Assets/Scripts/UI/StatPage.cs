using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatPage : MonoBehaviour
{
    [SerializeField] private Button runBtn;
    [SerializeField] private PlayerStatVariableSO permanentStat;
    [SerializeField] private List<Image> statImages;
    private float spinDuration = 1.5f;
    private float spinInterval = 0.1f;

    private List<Action> statUpgraders;
    private Coroutine rouletteCoroutine;
    private void Start()
    {
        statUpgraders = new List<Action>
        {
            () => permanentStat.RuntimeValue.MaxHp++,
            () => permanentStat.RuntimeValue.Power++,
            () => permanentStat.RuntimeValue.Delay++
        };

        runBtn.onClick.RemoveAllListeners();
        runBtn.onClick.AddListener(() => RunRoulette());
    }
    public void RunRoulette()
    {
        if (rouletteCoroutine != null)
            StopCoroutine(rouletteCoroutine);

        rouletteCoroutine = StartCoroutine(SpinRoulette());
    }
    private IEnumerator SpinRoulette()
    {
        float timer = 0f;
        int currentIndex = 0;
        int totalCount = statImages.Count;

        HighlightImage(-1);

        while (timer < spinDuration)
        {
            HighlightImage(currentIndex);

            float t = timer / spinDuration;
            float delay = Mathf.Lerp(spinInterval, spinInterval * 3, t);
            yield return new WaitForSeconds(delay);

            currentIndex = (currentIndex + 1) % totalCount;
            timer += spinInterval;
        }

        int finalIndex = UnityEngine.Random.Range(0, totalCount);
        HighlightImage(finalIndex);

        statUpgraders[finalIndex]?.Invoke();
    }

    private void HighlightImage(int index)
    {
        for (int i = 0; i < statImages.Count; i++)
        {
            statImages[i].color = (i == index) ? Color.white : new Color(1, 1, 1, 0.3f);
        }
    }
}
