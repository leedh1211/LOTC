using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : Singleton<SceneLoadManager>
{
    [SerializeField] FadeUI fadeUIPrefab;
    FadeUI screenFadeUI;
    protected override void Awake()
    {
        base.Awake();
        if (screenFadeUI == null)
        {
            screenFadeUI = Instantiate(fadeUIPrefab);

            DontDestroyOnLoad(screenFadeUI);
        }
    }


    public void LoadScene(string sceneName) => StartCoroutine(LoadSceneCoroutine(sceneName));

    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return screenFadeUI.Fade(false,0.5f);

        yield return SceneManager.LoadSceneAsync(sceneName);

        yield return screenFadeUI.Fade(true,0.2f);

    }
}
