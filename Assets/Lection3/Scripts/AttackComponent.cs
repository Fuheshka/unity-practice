using UnityEngine;

/// <summary>
/// Simple attack component: when asked, deals damage to a target that has HealthComponent.
/// Uses a cooldown between attacks.
/// </summary>
public class AttackComponent : MonoBehaviour {
    [SerializeField]
    int _damage = 10;

    [SerializeField]
    float _attackRange = 1.5f;

    [SerializeField]
    float _cooldown = 1.0f;

    float _lastAttackTime = -999f;

    /// <summary>
    /// Try to attack a target object if in range and cooldown elapsed.
    /// Returns true if attack happened.
    /// </summary>
    public bool TryAttack(GameObject target) {
        if (target == null)
        {
            return false;
        }

        if (Time.time - _lastAttackTime < _cooldown)
        {
            return false;
        }

        var dir = target.transform.position - transform.position;
        dir.y = 0f;
        if (dir.sqrMagnitude > _attackRange * _attackRange)
        {
            return false;
        }

        var health = target.GetComponent<HealthComponent>();
        if (health != null) {
            health.TakeDamage(_damage);
            _lastAttackTime = Time.time;
            Debug.Log($"{gameObject.name} attacked {target.name} for {_damage} dmg.");
            return true;
        }

        return false;
    }
}
