using UnityEngine;

public class SpawnerEnemy : MonoBehaviour {
    public GameObject Enemy;
    [SerializeField] private Transform _transform;

    void Awake() {
        for (int i = 0; i < 5; i++) {
            Instantiate(Enemy, new Vector3(_transform.position.x, _transform.position.y + i * 2.0F, 0), Quaternion.identity);
        }
    }
}
