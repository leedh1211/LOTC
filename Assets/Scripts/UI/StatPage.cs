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
    [SerializeField] private AnimationCurve easeOutCurve;
    private List<ProgressTweener> imageTweeners = new List<ProgressTweener>();
    private void Start()
    {
        foreach (var img in statImages)
            imageTweeners.Add(new ProgressTweener(this));
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
        int count = statImages.Count;

        SetImageAlpha(-1); // 모두 흐리게

        while (timer < spinDuration)
        {
            SetImageAlpha(currentIndex);

            float t = timer / spinDuration;
            float delay = Mathf.Lerp(spinInterval, spinInterval * 3f, t);
            yield return new WaitForSeconds(delay);

            currentIndex = (currentIndex + 1) % count;
            timer += delay;
        }

        int finalIndex = UnityEngine.Random.Range(0, count);
        SetImageAlpha(finalIndex);

        statUpgraders[finalIndex]?.Invoke();
        SaveManager.Instance.Save();
        yield return new WaitForSeconds(1f);
        RestoreImageAlpha();
    }
    private void SetImageAlpha(int highlightIndex)
    {
        for (int i = 0; i < statImages.Count; i++)
        {
            int index = i;
            var image = statImages[index];
            var color = image.color;

            color.a = (index == highlightIndex) ? 1f : 0.3f;
            image.color = color;

            // 개별 tweener로 각각 실행
            image.rectTransform.localScale = (index == highlightIndex) ? Vector3.zero : Vector3.one;

            imageTweeners[index]
                .SetCurve(easeOutCurve)
                .Play((ratio) =>
                {
                    if (index == highlightIndex)
                    {
                        image.rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, ratio);
                    }
                }, 0.1f);
        }
    }

    private void RestoreImageAlpha()
    {
        foreach (var img in statImages)
        {
            var color = img.color;
            color.a = 1f;
            img.color = color;
        }
    }
}
