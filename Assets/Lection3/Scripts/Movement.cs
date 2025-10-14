using UnityEngine;

/// <summary>
/// Movement component
/// </summary>
public class Movement : MonoBehaviour {

    /// <summary>
    /// Object speed
    /// </summary>
    [SerializeField]
    float _speed = 2.5f;

    /// <summary>
    /// Object rotation speed
    /// </summary>
    [SerializeField]
    float _rotation = 720f;

    /// <summary>
    /// Target position
    /// </summary>
    Vector3 _position = Vector3.zero;

    /// <summary>
    /// Set the target position for the object to move towards
    /// </summary>
    /// <param name="position">The target position</param>
    public void SetTargetPosition(Vector3 position) {
        _position = position;
    }

    /// <summary>
    /// Move the object to the specified target position
    /// </summary>
    public void Move() {
        var direction = (_position - transform.position).normalized;
        if (direction != Vector3.zero) {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), _rotation * Time.deltaTime);
        }
        transform.position = Vector3.MoveTowards(transform.position, _position, _speed * Time.deltaTime);
    }

    /// <summary>
    /// Stop the object movement
    /// </summary>
    public void Stop() {
        _position = transform.position;
    }
}
