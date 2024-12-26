using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    // 单例模式
    public static EnemyManager Instance { get; private set; }

    [Header("敌人刷新点")]
    public Transform[] spawnPoints;

    [Header("敌人巡逻点")]
    public Transform[] patrolPoints;

    public SceneExit exit;
    public bool isExitActive = true;

    [Header("关卡敌人")]
    public List<EnemyWave> enemyWaves;

    public int currentWaveIndex = 0; // 当前波数

    public int EnemyCount = 0;

    public bool GetLastWave() => currentWaveIndex == enemyWaves.Count;

    private void Awake()
    {
        Instance = this;
        exit = FindObjectOfType<SceneExit>();
    }

    public void Update()
    {
        exit.gameObject.SetActive(true);
        if (EnemyCount == 0 && !GetLastWave())
        { // 当前波数敌人是否全部死亡，是开始下一波
            StartCoroutine(nameof(startNextWaveCoroutine));
        }
        // else if (EnemyCount == 0 && GetLastWave() && !isExitActive)
        // {
        //     if (exit != null)
        //     {
        //         isExitActive = true;
        //     }
        // }
    }

    IEnumerator startNextWaveCoroutine()
    {
        if (currentWaveIndex >= enemyWaves.Count)
        {
            yield break;
        }

        List<EnemyData> enemies = enemyWaves[currentWaveIndex].enemies;

        foreach (EnemyData enemyData in enemies)
        {
            for (int i = 0; i < enemyData.waveEnemyCount; i++)
            {
                // EnemyCount++;
                // 第一个参数：敌人的预制体，第二个参数：生成敌人的位置，第三个参数：旋转
                GameObject enemy = Instantiate(enemyData.enemyPrefab, GetRandomSpawnPoint(), Quaternion.identity);
                if (patrolPoints != null)
                { // 巡逻点列表不为空，就将巡逻点列表赋值给敌人的巡逻点列表
                    enemy.GetComponent<Enemy>().patrolPoints = patrolPoints;
                }
                // enemy.GetComponent<Enemy>().patrolPoints = patrolPoints;
                yield return new WaitForSeconds(enemyData.spawnInterval);

            }
            currentWaveIndex++;
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex].position;
    }
}




[System.Serializable]
public class EnemyData
{
    public GameObject enemyPrefab; // 敌人预制体

    public float spawnInterval; // 刷新时间

    public float waveEnemyCount;  // 敌人数量
}

[System.Serializable]
public class EnemyWave
{
    public List<EnemyData> enemies; // 每一波敌人的数据
}