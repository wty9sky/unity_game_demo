using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [Header("UI")]
    public GameObject gameOverPanel;
    public GameObject gamePassPanel;

    public Button btnRestart;
    public Button btnPass;

    public Slider hpSlider;


    private void Awake(){
        Instance = this;
    }


    void Start()
    {
        // 给按钮添加事件监听
        btnRestart.onClick.AddListener(OnRestartGameClick);
        btnPass.onClick.AddListener(OnRestartGameClick);
    }

    // void Update()
    // {
    //     // 通关条件，最后一波敌人全部死亡
    //     // if (EnemyManager.Instance.GetLastWave() && EnemyManager.Instance.EnemyCount == 0)
    //     // {
    //     //     gamePassPanel.SetActive(true); //  显示通关面板
    //     // }
    // }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void UpdateHpSlider(float currentHealth, float maxHealth)
    {
        hpSlider.maxValue = maxHealth;
        hpSlider.value = currentHealth;
    }



    public void OnRestartGameClick()
    {
    }

}
