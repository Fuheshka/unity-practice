using UnityEngine;

/// <summary>
/// Simple NPC controller that composes HealthComponent, MovementComponent and AttackComponent.
/// Behavior: follow player if within followDistance, attack if within attackDistance.
/// </summary>
public class NPCController : MonoBehaviour {
    [SerializeField]
    Transform _player;

    [SerializeField]
    float _followDistance = 10f;

    [SerializeField]
    float _attackDistance = 1.8f;

    HealthComponent _health;
    MovementComponent _movement;
    AttackComponent _attack;

    void Awake() {
        _health = GetComponent<HealthComponent>();
        _movement = GetComponent<MovementComponent>();
        _attack = GetComponent<AttackComponent>();

        if (_health == null) {
            Debug.LogWarning("NPC missing HealthComponent");
        }

        if (_movement == null) {
            Debug.LogWarning("NPC missing MovementComponent");
        }

        if (_attack == null) {
            Debug.LogWarning("NPC missing AttackComponent");
        }
    }

    void Update() {
        if (_player == null) {
            return;
        }

        var dist = Vector3.Distance(transform.position, _player.position);

        if (dist <= _attackDistance) {
            // stop moving and attack
            _movement?.Stop();
            _attack?.TryAttack(_player.gameObject);
        } else if (dist <= _followDistance) {
            // follow
            _movement?.SetTarget(_player.position);
        } else {
            // idle
            _movement?.Stop();
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _followDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackDistance);
    }
}
