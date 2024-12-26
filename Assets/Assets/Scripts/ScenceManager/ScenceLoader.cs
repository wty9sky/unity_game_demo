using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
   //����ģʽ�����Ҳ�����
   public static SceneLoader Instance { get; private set; }

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

        DontDestroyOnLoad(gameObject);
    }

    //�л���������
    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(TransitionCoroutine(sceneName));
    }

    //�л�����Э��
    public IEnumerator TransitionCoroutine(string newSceneName)
    {
        GameManager.Instance.SaveData();

        yield return StartCoroutine(ScreenFader.Instance.FadeScreenOut());

        yield return SceneManager.LoadSceneAsync(newSceneName);

        GameManager.Instance.LoadData();

        SceneEntrance entrance = FindObjectOfType<SceneEntrance>();

        SetEnteringPosition(entrance);

        yield return StartCoroutine(ScreenFader.Instance.FadeScreenIn());
    }


    private void SetEnteringPosition(SceneEntrance entrance)
    {
        if (entrance == null)
            return;

        Transform entanceTransform = entrance.transform;
        Player.Instance.transform.position = entanceTransform.position;
    }
  
}
