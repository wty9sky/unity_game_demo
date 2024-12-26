using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����˳�
/// </summary>
public class SceneExit : MonoBehaviour
{
    public string newSceneName;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    //����ҽ��봥����
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TransitionInternal();
        }
    }

    //���ó����л�����
    public void TransitionInternal()
    {
        SceneLoader.Instance.TransitionToScene(newSceneName);
    }
}
