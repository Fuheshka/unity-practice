using System;
using UnityEngine;

/// <summary>
/// Health component
/// </summary>
public class Health : MonoBehaviour {

    /// <summary>
    /// Health value
    /// </summary>
    public int Value = 100;

    /// <summary>
    /// Action called when health changes
    /// </summary>
    public Action<int> OnHealthChanged = delegate { };

    /// <summary>
    /// Take damage
    /// </summary>
    /// <param name="damage">The amount of damage to take</param>
    public void TakeDamage(int damage) {
        Value -= damage;
        OnHealthChanged.Invoke(Value);
    }

    /// <summary>
    /// Heal
    /// </summary>
    /// <param name="amount">The amount to heal</param>
    public void Heal(int amount) {
        Value += amount;
        OnHealthChanged.Invoke(Value);
    }
}
