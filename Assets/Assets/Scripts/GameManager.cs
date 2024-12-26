using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject uiShowText;
    public static GameManager Instance { get; private set; }

    public int CoinCount { get; private set; }

    public float PlayerCurrentHealth { get; set; }

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

        DontDestroyOnLoad(gameObject); // 加载新场景时不销毁
    }

    public void UpdateCoin(int value)
    {
        CoinCount += value;
        if (CoinCount < 0)
        {
            CoinCount = 0;
        }
        UICoinCountText.UpdateText(CoinCount);
    }

    public void SaveData()
    {
        PlayerCurrentHealth = Player.Instance.currentHealth;
    }

    public void LoadData()
    {
        Player.Instance.currentHealth = PlayerCurrentHealth;
        UICoinCountText.UpdateText(CoinCount);
    }

    public void ShowText(string str, Vector2 pos, Color color)
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(pos);
        GameObject text = Instantiate(uiShowText, screenPosition, Quaternion.identity);
        text.transform.SetParent(GameObject.Find("HUD").transform);
        text.GetComponent<UIShowText>().ShowText(str, color);
    }
}
