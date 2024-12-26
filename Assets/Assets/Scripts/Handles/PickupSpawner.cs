using UnityEngine;

public class PickupSpawner : MonoBehaviour
{

    public DropPrefab[] dropPrefabs;

    public void DropItems()
    {
        foreach (DropPrefab dropPrefab in dropPrefabs)
        {
            if (Random.Range(0, 100) <= dropPrefab.dropPercentage)
            {
                // Instantiate(dropPrefab.prefab, transform.position, Quaternion.identity);
                // 在周边随机地方掉落，如果圆形内有刚体，需要在刚体之外生成
                Vector2 randomPosition = Random.insideUnitCircle * 2;
                Instantiate(dropPrefab.prefab, transform.position + new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity);
                // 在圆形内随机生成
                // Vector2 randomPosition = Random.insideUnitCircle * dropPrefab.prefab.GetComponent<CircleCollider2D>().radius;
                // Instantiate(dropPrefab.prefab, transform.position + new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity);
            }
        }
    }

}

[System.Serializable]
public class DropPrefab
{
    public GameObject prefab;
    [Range(0, 100)] public int dropPercentage;
}
