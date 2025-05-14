using System.Collections;
using TMPro;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private void Start()
    {
        StartCoroutine(SceneAnimation());
    }
    private void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
           SceneLoadManager.Instance.LoadScene("LobbyScene");
        }
    }
    IEnumerator SceneAnimation()
    {
        while (true)
        {
            while (text.alpha > 0f)
            {
                text.alpha -= 0.01f;
                yield return null;
            }

            while (text.alpha < 1f)
            {
                text.alpha += 0.02f;
                yield return null;
            }
        }
    }
}
