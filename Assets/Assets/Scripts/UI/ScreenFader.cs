using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance { get; private set; }
    public CanvasGroup fadeCanvasGroup;

    public float fadeDuration = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); // 加载新场景时，告诉进程不销毁该对象
    }


    public IEnumerator FadeScreenIn()
    {
        yield return StartCoroutine(Fade(0f, fadeCanvasGroup));
        fadeCanvasGroup.gameObject.SetActive(false);
    }

    public IEnumerator FadeScreenOut()
    {
        fadeCanvasGroup.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1f, fadeCanvasGroup));
    }

    public IEnumerator Fade(float finalAlpha, CanvasGroup canvasGroup)
    {
        yield return canvasGroup.DOFade(finalAlpha, fadeDuration).WaitForCompletion();
    }

}
