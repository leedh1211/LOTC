using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    private Canvas canvas;
    [SerializeField] private Image fadeImage;
    public void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }
    public IEnumerator Fade(bool isFadeIn,float duration)
    {
        float startAlpha = isFadeIn ? 1 : 0;
        float targetAlpha = isFadeIn ? 0 : 1;

        float time = 0f;

        Color color = fadeImage.color;


        color.a = startAlpha;

        fadeImage.color = color;

        canvas.enabled = true;

        while (time < duration)
        {
            time += Time.deltaTime;

            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);

            color.a = alpha;

            fadeImage.color = color;

            yield return null;
        }

        canvas.enabled = !isFadeIn;
    }
    void OnEnable()
    {
        Debug.Log("[CanvasDebug] Canvas OnEnable");
    }

    void OnDisable()
    {
        Debug.Log("[CanvasDebug] Canvas OnDisable");
    }
}
