using UnityEngine;

public class UniversalEnemySpawner : MonoBehaviour {
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private bool spawnDifferentTypes = true;

    private int currentEnemyCount = 0;

    private void Start() {
        InvokeRepeating(nameof(SpawnRandomEnemy), 0f, spawnInterval);
    }

    private void SpawnRandomEnemy() {
        if (enemyPrefabs == null || enemyPrefabs.Length == 0)
            return;

        // Check if we reached the maximum number of enemies
        if (currentEnemyCount >= maxEnemies) {
            Debug.Log("Maximum number of enemies reached!");
            return;
        }

        // Get random position within circle
        var randomPoint = Random.insideUnitCircle * spawnRadius;
        var spawnPosition = new Vector3(randomPoint.x, 0, randomPoint.y);

        // Get random enemy prefab
        int randomIndex;
        if (spawnDifferentTypes) {
            // Try to maintain balance between different enemy types
            var existingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            var typeDistribution = new int[enemyPrefabs.Length];

            foreach (var existingEnemy in existingEnemies) {
                for (int i = 0; i < enemyPrefabs.Length; i++) {
                    if (existingEnemy.GetComponent(enemyPrefabs[i].GetComponent<EnemyBase>().GetType())) {
                        typeDistribution[i]++;
                        break;
                    }
                }
            }

            // Find type with minimum count
            var minCount = int.MaxValue;
            var candidateIndices = new System.Collections.Generic.List<int>();

            for (int i = 0; i < typeDistribution.Length; i++) {
                if (typeDistribution[i] < minCount) {
                    minCount = typeDistribution[i];
                    candidateIndices.Clear();
                    candidateIndices.Add(i);
                } else if (typeDistribution[i] == minCount) {
                    candidateIndices.Add(i);
                }
            }

            randomIndex = candidateIndices[Random.Range(0, candidateIndices.Count)];
        } else {
            randomIndex = Random.Range(0, enemyPrefabs.Length);
        }

        var enemyPrefab = enemyPrefabs[randomIndex];
        var enemy = Instantiate(enemyPrefab, transform.position + spawnPosition, Quaternion.identity);
        currentEnemyCount++;

        // Subscribe to enemy death to update counter
        var enemyBase = enemy.GetComponent<EnemyBase>();
        enemyBase.OnEnemyDeath += HandleEnemyDeath;
    }

    private void HandleEnemyDeath() {
        currentEnemyCount--;
    }

    private void OnDrawGizmos() {
        // Visual debug for spawn area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
