using UnityEngine;

/// <summary>
/// Component that moves a GameObject towards a target point using Rigidbody.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class MovementComponent : MonoBehaviour {
    [SerializeField]
    float _speed = 3f;

    Rigidbody _body;

    Vector3? _target = null;

    void Awake() {
        _body = GetComponent<Rigidbody>();
        // keep rotation on X and Z frozen like Player does
        _body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate() {
        if (_target.HasValue == false) {
            return;
        }

        var dir = (_target.Value - _body.position);
        dir.y = 0f; // stay on the same plane
        var distance = dir.magnitude;
        if (distance < 0.01f) {
            return;
        }

        var movement = dir.normalized * _speed;
        _body.MovePosition(_body.position + movement * Time.fixedDeltaTime);

        if (movement != Vector3.zero) {
            var targetRotation = Quaternion.LookRotation(movement);
            _body.MoveRotation(Quaternion.RotateTowards(_body.rotation, targetRotation, 720f * Time.fixedDeltaTime));
        }
    }

    /// <summary>
    /// Set global target position to move to. Pass null to stop.
    /// </summary>
    public void SetTarget(Vector3? worldPosition) {
        _target = worldPosition;
    }

    /// <summary>
    /// Immediately stop movement.
    /// </summary>
    public void Stop() {
        _target = null;
    }
}
