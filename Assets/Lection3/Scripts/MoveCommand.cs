using UnityEngine;

/// <summary>
/// Move command
/// </summary>
public class MoveCommand : ICommand {

    /// <summary>
    /// Target rigidbody
    /// </summary>
    Rigidbody _target = null;

    /// <summary>
    /// Movement direction
    /// </summary>
    Vector3 _direction = Vector3.zero;

    /// <summary>
    /// Movement velocity
    /// </summary>
    Vector3 _velocity = Vector3.zero;

    /// <summary>
    /// Rotation speed
    /// </summary>
    float _rotationSpeed = 0f;

    /// <summary>
    /// Move command constructor
    /// </summary>
    /// <param name="target">Target transform</param>
    /// <param name="direction">Movement direction</param>
    /// <param name="velocity">Movement velocity</param>
    /// <param name="rotationSpeed">Rotation speed</param>
    public MoveCommand(Rigidbody target, Vector3 direction, Vector3 velocity, float rotationSpeed) {
        _target = target;
        _direction = direction;
        _velocity = velocity;
        _rotationSpeed = rotationSpeed;
    }

    /// <summary>
    /// Execute the movement
    /// </summary>
    public void Execute() {
        _target.MovePosition(_target.position + _velocity * Time.fixedDeltaTime);
        if (_direction != Vector3.zero) {
            var targetRotation = Quaternion.LookRotation(_direction);
            _target.MoveRotation(Quaternion.RotateTowards(_target.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime));
        }
    }

    /// <summary>
    /// Undo the movement
    /// </summary>
    public void Undo() {
        _target.MovePosition(_target.position - _velocity * Time.fixedDeltaTime);
        if (_direction != Vector3.zero) {
            var targetRotation = Quaternion.LookRotation(_direction);
            _target.MoveRotation(Quaternion.RotateTowards(_target.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime));
        }
    }
}
