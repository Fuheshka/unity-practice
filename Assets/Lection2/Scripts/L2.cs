using UnityEngine;

/// <summary>
/// Demo for lection 2
/// </summary>
public class L2 : MonoBehaviour {

    /// <summary>
    /// Orc prefab
    /// </summary>
    [SerializeField]
    GameObject _orcPrefab;

    /// <summary>
    /// Troll prefab
    /// </summary>
    [SerializeField]
    GameObject _trollPrefab;

    /// <summary>
    /// Dragon prefab
    /// </summary>
    [SerializeField]
    GameObject _dragonPrefab;

    /// <summary>
    /// Spawn interval for enemies
    /// </summary>
    [SerializeField]
    float _spawnInterval = 5f;

    /// <summary>
    /// Spawn enemies
    /// </summary>
    void Start() {
        InvokeRepeating(nameof(DoSpawn), 0f, _spawnInterval);
    }

    /// <summary>
    /// Spawner
    /// Make random position
    /// Call different spawn methods
    /// </summary>
    void DoSpawn() {
        var position = Vector3.zero;
        position.x = Random.Range(-10f, 10f);
        SpawnEnemy<EnemyOrc>(position);
        SpawnEnemy(_orcPrefab, position);
        position.x = Random.Range(-5f, 5f);
        SpawnEnemy<EnemyTroll>(position);
        SpawnEnemy(_trollPrefab, position);
        position.x = Random.Range(-3f, 3f);
        position.y = 2f;
        SpawnEnemy<EnemyDragon>(position);
        SpawnEnemy(_dragonPrefab, position);
    }

    /// <summary>
    /// Spawn enemy of type T
    /// </summary>
    /// <param name="position">Initial position</param>
    /// <typeparam name="T">Enemy type</typeparam>
    void SpawnEnemy<T>(Vector3 position) where T : EnemyBase {
        var obj = new GameObject(typeof(T).Name);
        obj.transform.position = position;
        obj.AddComponent<T>();
    }

    /// <summary>
    /// Another method to spawn enemy by prefab
    /// </summary>
    /// <param name="prefab">Prefab to spawn</param>
    /// <param name="position">Initial position</param>
    void SpawnEnemy(GameObject prefab, Vector3 position) {
        var obj = Instantiate(prefab, position, Quaternion.identity);
        obj.name = prefab.name;
    }
}
