using System;
using UnityEngine;

/// <summary>
/// Simple health container for NPCs (and any other actor).
/// Provides events for health changes and death.
/// </summary>
public class HealthComponent : MonoBehaviour {
    [SerializeField]
    int _maxHealth = 100;

    int _currentHealth;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    /// <summary>
    /// Fired when health changes. Parameters: current, max
    /// </summary>
    public event Action<int, int> OnHealthChanged;

    /// <summary>
    /// Fired when the owner dies (health <= 0)
    /// </summary>
    public event Action OnDied;

    void Awake() {
        _currentHealth = _maxHealth;
    }

    /// <summary>
    /// Reduce health by amount. Triggers events.
    /// </summary>
    public void TakeDamage(int amount) {
        if (amount <= 0) {
            return;
        }

        _currentHealth -= amount;

        if (_currentHealth < 0) {
            _currentHealth = 0;
        }

        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth == 0) {
            OnDied?.Invoke();
            Debug.Log($"{gameObject.name} died.");
        } else {
            Debug.Log($"{gameObject.name} took {amount} damage. HP: {_currentHealth}/{_maxHealth}");
        }
    }

    /// <summary>
    /// Heal the owner.
    /// </summary>
    public void Heal(int amount) {
        if (amount <= 0) {
            return;
        }

        _currentHealth += amount;

        if (_currentHealth > _maxHealth) {
            _currentHealth = _maxHealth;
        }

        OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
}
