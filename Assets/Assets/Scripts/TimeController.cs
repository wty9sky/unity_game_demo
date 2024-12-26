using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TimeController : MonoBehaviour
{

    public static TimeController Instance { get; private set; }
    [Range(0f, 2f)] public float defaultTimeScale = 1; // 默认游戏时间速度

    [Header("子弹时间")]
    [SerializeField, Range(0f, 2f)] float bulletTimeScale; // 子弹时间速度

    [SerializeField] private float timeRecoveryDuration; // 过渡回默认游戏时间的持续时间

    private GUIStyle labelStyle;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = defaultTimeScale;
    }

    private void Start()
    {
        labelStyle = new GUIStyle();
        labelStyle.fontSize = 20;
        labelStyle.normal.textColor = Color.white;
    }

    private void OnGUI()
    {
        // GUI.Label(new Rect(10, 10, 100, 30), "Time Scale: " + Time.timeScale, labelStyle); // 显示当前游戏时间速度 (保留两位小数)
    }


    public void SetBulletTime()
    {
        Time.timeScale = bulletTimeScale;
        StartCoroutine(nameof(TimeRecoveryCoroutine));
    }


    protected virtual IEnumerator TimeRecoveryCoroutine()
    {
        float ratio = 0f;
        while (ratio < 1f)
        {
            ratio += Time.deltaTime / timeRecoveryDuration;
            Time.timeScale = Mathf.Lerp(bulletTimeScale, defaultTimeScale, ratio);

            yield return null;
        }
    }



}
