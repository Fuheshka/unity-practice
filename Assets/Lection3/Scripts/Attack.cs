using UnityEngine;

/// <summary>
/// Attack component
/// </summary>
public class Attack : MonoBehaviour {

    /// <summary>
    /// Attack damage
    /// </summary>
    [SerializeField]
    int _damage = 10;

    /// <summary>
    /// Attack cooldown time in seconds
    /// </summary>
    [SerializeField]
    float _delay = 0.5f;

    /// <summary>
    /// Health component
    /// </summary>
    Health _health = null;

    /// <summary>
    /// Time when the next attack can be performed
    /// </summary>
    float _nextAttack = 0f;

    /// <summary>
    /// Start the attack
    /// </summary>
    void Start() {
        TryGetComponent<Health>(out _health);
        if (_health == null) {
            Debug.LogError($"[{nameof(Attack).ToUpperInvariant()}] missing component: {nameof(Health)}");
            return;
        }
    }

    /// <summary>
    /// Uses the attack
    /// </summary>
    public void Use() {
        if (Time.time < _nextAttack) {
            return;
        }
        _health.TakeDamage(_damage);
        _nextAttack = Time.time + _delay;
    }
}
