using System;
using UnityEngine;

/// <summary>
/// Detector component
/// </summary>
public class Detector : MonoBehaviour {

    /// <summary>
    /// Called when a target is detected
    /// </summary>
    public Action<Vector3, float> OnDetected = delegate { };

    /// <summary>
    /// Called when a target is lost
    /// </summary>
    public Action OnLost = delegate { };

    /// <summary>
    /// Target transform to follow
    /// </summary>
    [SerializeField]
    Transform _target = null;

    /// <summary>
    /// Detection range
    /// </summary>
    [SerializeField]
    float _detectionRange = 5f;

    /// <summary>
    /// Last known position of the detected target
    /// </summary>
    Vector3 _last = Vector3.zero;

    /// <summary>
    /// Update the detector state
    /// </summary>
    void Update() {
        if (_target != null) {
            var distance = Vector3.Distance(transform.position, _target.position);
            if (_target.position != _last) {
                if (distance < _detectionRange) {
                    OnDetected(_target.position, distance);
                } else {
                    OnLost();
                }
                _last = _target.position;
            }
        }
    }
}
