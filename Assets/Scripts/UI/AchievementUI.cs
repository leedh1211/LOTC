using System.Collections;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AchievementUI : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private RectTransform pavelRectTransform;
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text descriptionText;
    
        [SerializeField] private float slideDuration = 0.5f;
        [SerializeField] private float stayDuration = 2f;
        
        private Vector2 hiddenPos;
        private Vector2 visiblePos;

        private void Awake()
        {
            // UI 패널 위치 설정 (상단 밖 → 상단 안)
            // panel.SetActive(false);
            visiblePos = pavelRectTransform.anchoredPosition;
            hiddenPos = visiblePos + new Vector2(0, 200); // 위로 숨기기
            pavelRectTransform.anchoredPosition = hiddenPos;
        }

        public void ShowAchievement(AchievementSO achievement, AchievementStatus status)
        {
            panel.SetActive(true);
            iconImage.sprite = achievement.icon;
            titleText.text = achievement.title;
            descriptionText.text = MakeDesc(achievement, status);

            StopAllCoroutines();
            StartCoroutine(ShowRoutine());
        }
        
        public string MakeDesc(AchievementSO achievement , AchievementStatus status)
        {
            int requiredValue = achievement.levels[status.currentLevel-1];
            return achievement.description.Replace("{Level}", requiredValue.ToString());
        }

        private IEnumerator ShowRoutine()
        {
            
            yield return SlideTo(visiblePos);
            yield return new WaitForSeconds(stayDuration);
            
            yield return SlideTo(hiddenPos);
        }

        private IEnumerator SlideTo(Vector2 target)
        {
            float elapsed = 0f;
            Vector2 start = pavelRectTransform.anchoredPosition;

            while (elapsed < slideDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / slideDuration);
                pavelRectTransform.anchoredPosition = Vector2.Lerp(start, target, t);
                yield return null;
            }

            pavelRectTransform.anchoredPosition = target;
        }

    }
}