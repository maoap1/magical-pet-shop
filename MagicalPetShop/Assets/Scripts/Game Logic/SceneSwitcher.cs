using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneSwitcher : MonoBehaviour
{
    public Image BlackBG;
    public float FadeDuration;
    public bool switching = false;

    Dictionary<string, SoundType> transitionSounds = new Dictionary<string, SoundType>() {
        { "Lab", SoundType.Steps },
        { "Shop", SoundType.CustomerAppear },
        { "Yard", SoundType.Door }
    };

    public void LoadScene(string sceneName)
    {
        if (switching) return;
        switching = true;

        if (transitionSounds.ContainsKey(sceneName)) {
            FindObjectOfType<AudioManager>().Play(transitionSounds[sceneName]);
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        BlackBG.gameObject.SetActive(true);
        BlackBG.color = Color.black.WithA(0f);
        BlackBG.DOColor(Color.black, FadeDuration).OnComplete(() => { StartCoroutine(LoadAsyncScene(asyncLoad)); });
        
    }

    private IEnumerator LoadAsyncScene(AsyncOperation asyncOperation)
    {
        while (!asyncOperation.isDone)
        {
            asyncOperation.allowSceneActivation = true;
            DOTween.KillAll();
            yield return null;
        }
    }

    public void OnSceneLoad()
    {
        BlackBG.gameObject.SetActive(true);
        BlackBG.color = Color.black;
        BlackBG.DOFade(0f, FadeDuration).OnComplete(() => { BlackBG.gameObject.SetActive(false); });
    }

    public void Start()
    {
        OnSceneLoad();
    }
}
