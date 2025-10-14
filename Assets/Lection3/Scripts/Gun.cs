using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gun class for shooting bullets
/// </summary>
[RequireComponent(typeof(Pool))]
public class Gun : MonoBehaviour {

    /// <summary>
    /// Bullet speed 
    /// </summary>
    [SerializeField]
    float _bulletSpeed = 20f;

    /// <summary>
    /// Bullet offset from the player position
    /// </summary>
    [SerializeField]
    Vector3 _bulletOffset = new Vector3(0f, 1f, 0f);

    /// <summary>
    /// Object pool
    /// </summary>
    Pool _pool = null;

    /// <summary>
    /// Stack of bullet objects
    /// </summary>
    Stack<GameObject> _bullets = new Stack<GameObject>();

    /// <summary>
    /// Initialize
    /// </summary>
    void Start() {
        _pool = GetComponent<Pool>();
    }

    /// <summary>
    /// Shoot a bullet
    /// </summary>
    public void Shoot() {
        _pool.SetPosition(transform.position + _bulletOffset);
        var obj = _pool.Get();
        if (obj != null) {
            _bullets.Push(obj);
            var body = obj.GetComponent<Rigidbody>();
            body.AddForce(transform.forward * _bulletSpeed, ForceMode.Impulse);
        } else {
            Debug.LogWarning($"[{nameof(Gun).ToUpperInvariant()}] no bullets, need reload, press R");
        }
    }

    /// <summary>
    /// Reload the gun
    /// </summary>
    public void Reload() {
        while (_bullets.Count > 0) {
            var obj = _bullets.Pop();
            _pool.Free(obj);
        }
    }
}
