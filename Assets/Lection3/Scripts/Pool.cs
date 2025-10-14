using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple object pooling system
/// </summary>
public class Pool : MonoBehaviour {

    /// <summary>
    /// Size of the pool
    /// </summary>
    public int Size => _size;

    /// <summary>
    /// Prefab to instantiate
    /// </summary>
    [SerializeField]
    GameObject _prefab;

    /// <summary>
    /// Number of objects to pool
    /// </summary>
    [SerializeField]
    int _size = 10;

    /// <summary>
    /// Object pool
    /// </summary>
    readonly Queue<GameObject> _pool = new Queue<GameObject>();

    /// <summary>
    /// Instantiate position
    /// </summary>
    Vector3 _position = Vector3.zero;

    /// <summary>
    /// Start pool
    /// </summary>
    public void Start() {
        for (var i = 0; i < _size; i++) {
            var obj = Instantiate(_prefab, _position, Quaternion.identity);
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    /// <summary>
    /// Set the instantiate position
    /// </summary>
    /// <param name="position">Position to instantiate objects</param>
    public void SetPosition(Vector3 position) {
        _position = position;
    }

    /// <summary>
    /// Get an object from the pool
    /// </summary>
    public GameObject Get() {
        if (_pool.Count == 0) {
            Debug.LogWarning($"[{nameof(Pool).ToUpperInvariant()}] pool is empty, wait");
            return null;
        }
        var obj = _pool.Dequeue();
        obj.transform.position = _position;
        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// Return an object to the pool
    /// </summary>
    /// <param name="obj">Object to return</param>
    public void Free(GameObject obj) {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }
}
