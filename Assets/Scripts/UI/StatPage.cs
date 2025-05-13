using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatPage : MonoBehaviour
{
    [SerializeField] private Button runBtn;
    [SerializeField] private PlayerStatVariableSO permanentStat;
    [SerializeField] private List<Image> statImages;
    [SerializeField] private Image levelUpImage;
    [SerializeField] private TextMeshProUGUI levelUpText;
    [SerializeField] private Image levelUpStatImage;
    private readonly string[] statNames = { "최대 체력", "공격력", "공격 속도" };
    [SerializeField] private TextMeshProUGUI maxHpText;
    [SerializeField] private TextMeshProUGUI powerText;
    [SerializeField] private TextMeshProUGUI delayText;
    private float spinDuration = 1.5f;
    private float spinInterval = 0.1f;

    private List<Action> statUpgraders;
    private Coroutine rouletteCoroutine;
    [SerializeField] private AnimationCurve easeOutCurve;
    private List<ProgressTweener> imageTweeners = new List<ProgressTweener>();
    private ProgressTweener levelUpImageTweener;
    private void Start()
    {
        foreach (var img in statImages)
            imageTweeners.Add(new ProgressTweener(this));
        levelUpImageTweener = new(this);
        statUpgraders = new List<Action>
        {
            () => permanentStat.RuntimeValue.MaxHp++,
            () => permanentStat.RuntimeValue.Power++,
            () => permanentStat.RuntimeValue.Delay++
        };
        levelUpImage.GetComponent<Button>().onClick.AddListener(() => OnLevelUpImageClick());
        runBtn.onClick.RemoveAllListeners();
        runBtn.onClick.AddListener(() => RunRoulette());
        UpdateStatTexts();
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

        SetAnimation(-1); // 모두 흐리게

        while (timer < spinDuration)
        {
            SetAnimation(currentIndex);

            float t = timer / spinDuration;
            float delay = Mathf.Lerp(spinInterval, spinInterval * 3f, t);
            yield return new WaitForSeconds(delay);

            currentIndex = (currentIndex + 1) % count;
            timer += delay;
        }

        int finalIndex = UnityEngine.Random.Range(0, count);
        SetAnimation(finalIndex);

        statUpgraders[finalIndex]?.Invoke();
        SaveManager.Instance.Save();
        levelUpStatImage.sprite = statImages[finalIndex].sprite;
        levelUpStatImage.color = statImages[finalIndex].color;
        levelUpText.text = statNames[finalIndex] + "증가!";
        var startPos = levelUpImage.rectTransform.anchoredPosition;
        levelUpImage.gameObject.SetActive(true);
        UpdateStatTexts();
        levelUpImageTweener.SetCurve(easeOutCurve).Play((ratio) =>
                {
                    levelUpImage.rectTransform.anchoredPosition = 
                    Vector3.Lerp(startPos, Vector3.zero, ratio);
                }, 0.1f);
        yield return new WaitForSeconds(0.5f);
        RestoreImageAlpha();
    }
    private void SetAnimation(int highlightIndex)
    {
        for (int i = 0; i < statImages.Count; i++)
        {
            int index = i;
            var image = statImages[index];
            var color = image.color;

            color.a = (index == highlightIndex) ? 1f : 0.3f;
            image.color = color;
            Vector3 vec = new Vector3(0.7f, 0.7f);
            image.rectTransform.localScale = (index == highlightIndex) ? vec : Vector3.one;

            imageTweeners[index]
                .SetCurve(easeOutCurve)
                .Play((ratio) =>
                {
                    if (index == highlightIndex)
                    {
                        image.rectTransform.localScale = Vector3.Lerp(vec, Vector3.one, ratio);
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
    public void OnLevelUpImageClick()
    {
        Vector3 pos = levelUpImage.rectTransform.localPosition;
        pos.y = -1920f;
        levelUpImage.rectTransform.localPosition = pos;
        levelUpImage.gameObject.SetActive(false);
    }
    private void UpdateStatTexts()
    {
        maxHpText.text = $"Lv.{permanentStat.RuntimeValue.MaxHp}\n 최대 체력";
        powerText.text = $"Lv.{permanentStat.RuntimeValue.Power}\n 공격력";
        delayText.text = $"Lv.{permanentStat.RuntimeValue.Delay}\n 공격 속도";
    }
}
