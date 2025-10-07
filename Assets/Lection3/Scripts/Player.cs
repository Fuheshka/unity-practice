using UnityEngine;

/// <summary>
/// Simple player movement
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

    /// <summary>
    /// Movement speed (units/second)
    /// </summary>
    [SerializeField]
    float _speed = 5f;

    /// <summary>
    /// Rotation speed (degrees/second)
    /// </summary>
    [SerializeField]
    float _rotationSpeed = 720f;

    /// <summary>
    /// Cached Rigidbody component
    /// </summary>
    Rigidbody _body = null;

    /// <summary>
    /// Initialize
    /// </summary>
    void Start() {
        _body = GetComponent<Rigidbody>();
        _body.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    /// <summary>
    /// Fixed update for physics calculations
    /// </summary>
    void FixedUpdate() {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var movement = new Vector3(horizontal, 0f, vertical).normalized;
        var velocity = movement * _speed;
        _body.MovePosition(_body.position + velocity * Time.fixedDeltaTime);
        if (movement != Vector3.zero) {
            var targetRotation = Quaternion.LookRotation(movement);
            _body.MoveRotation(Quaternion.RotateTowards(_body.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime));
        }
    }

    /// <summary>
    /// Raise when the player collides with another object
    /// </summary>
    void OnCollisionEnter(Collision collision) {
        Debug.Log($"Player collided with: {collision.gameObject.name}");
    }
}
