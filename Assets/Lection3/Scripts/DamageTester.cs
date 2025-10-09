using UnityEngine;

/// <summary>
/// Simple helper: press keys to damage or heal the player for quick testing.
/// Attach to any GameObject in the scene (for example an empty GameObject named 'Debug').
/// </summary>
public class DamageTester : MonoBehaviour {
    [Tooltip("Amount of damage applied when pressing the damage key")]
    public int damageAmount = 25;

    [Tooltip("Key to deal damage")]
    public KeyCode damageKey = KeyCode.Space;

    [Tooltip("Optional: key to heal the player")]
    public KeyCode healKey = KeyCode.H;

    HealthComponent _playerHealth;

    void Start() {
        var player = GameObject.FindWithTag("Player");
        if (player != null) {
            _playerHealth = player.GetComponent<HealthComponent>();
        }
    }

    void Update() {
        if (_playerHealth == null) {
            return;
        }

        if (Input.GetKeyDown(damageKey)) {
            _playerHealth.TakeDamage(damageAmount);
        }

        if (Input.GetKeyDown(healKey)) {
            _playerHealth.Heal(damageAmount);
        }
    }
}
