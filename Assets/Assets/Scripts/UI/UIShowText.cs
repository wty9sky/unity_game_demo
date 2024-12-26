using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// UI浮动显示文字效果
/// </summary>
public class UIShowText : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        transform.DOMoveY(transform.position.y + 20, 0.5f);
        // transform.DOScale(transform.localScale * 2f, 0.2f);
        Destroy(gameObject, 0.6f);
    }

    public void ShowText(string str, Color color)
    {
        text.color = color;
        text.text = str;
    }
}
