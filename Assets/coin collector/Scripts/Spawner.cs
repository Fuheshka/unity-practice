using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject coinPrefab;
    public float spawnInterval = 2f;
    public int coinCount = 10;

    [SerializeField] private Vector3 areaSize = new Vector3(10, 0, 10);

    void Start() {
        for (int i = 0; i < coinCount; i++) {
            Vector3 spawnPos = GetRandomPosition();
            Instantiate(coinPrefab, spawnPos, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPosition() {
        Vector3 center = transform.position;

        float x = Random.Range(center.x - areaSize.x / 2, center.x + areaSize.x / 2);
        float y = center.y;
        float z = Random.Range(center.z - areaSize.z / 2, center.z + areaSize.z / 2);

        return new Vector3(x, y, z);
    }
}
