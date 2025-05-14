using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatPage : MonoBehaviour
{
    private readonly string[] statNames = { "최대 체력", "공격력", "공격 속도" };
    private readonly Vector3[] lightPos = { new Vector3(-285,292.5f), new Vector3(-20, 292.5f), new Vector3(250, 292.5f) };
    [SerializeField] private Button runBtn;
    [SerializeField] private PlayerStatVariableSO permanentStat;
    [SerializeField] private List<Image> statImages;
    [SerializeField] private Image levelUpImage;
    [SerializeField] private TextMeshProUGUI levelUpText;
    [SerializeField] private Image levelUpStatImage;
    [SerializeField] private TextMeshProUGUI maxHpText;
    [SerializeField] private TextMeshProUGUI powerText;
    [SerializeField] private TextMeshProUGUI delayText;
    [Space(10f)]
    [SerializeField] private Image lightImg;
    [Space(10f)]
    [SerializeField] private IntegerVariableSO gold;
    [SerializeField] private VoidEventChannelSO onGoldChanged;
    [SerializeField] private TextMeshProUGUI goldText;
    private int price = 500;
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
        lightImg.gameObject.SetActive(false);
        goldText.text = price.ToString();
    }

    public void RunRoulette()
    {
        if (gold.RuntimeValue < price)
        {
            Debug.Log("돈 부족");
            return;
        }
        gold.RuntimeValue -= price;
        AchievementManager.Instance.ChangeProgress(6,gold.RuntimeValue);
        onGoldChanged.Raise();
        if (rouletteCoroutine != null)
            StopCoroutine(rouletteCoroutine);

        rouletteCoroutine = StartCoroutine(SpinRoulette());
    }
    private IEnumerator SpinRoulette()
    {
        float timer = 0f;
        int currentIndex = 0;
        int count = statImages.Count;
        lightImg.gameObject.SetActive(true);
        SetAnimation(-1); 

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
        if(highlightIndex != -1)
            lightImg.transform.localPosition = lightPos[highlightIndex];
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
