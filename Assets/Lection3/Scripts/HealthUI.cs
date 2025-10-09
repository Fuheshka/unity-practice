using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Simple health UI binder. Attach to a Canvas GameObject and set references.
/// It listens to a HealthComponent and updates Slider and optional Text.
/// </summary>
public class HealthUI : MonoBehaviour {
    [Tooltip("Health component to observe (if empty, will try to find on Player tag)")]
    public HealthComponent health;

    public Slider slider;
    public TextMeshProUGUI text;

    void Start() {
        if (health == null) {
            var player = GameObject.FindWithTag("Player");
            if (player != null) {
                health = player.GetComponent<HealthComponent>();
            }
        }

        if (health == null) {
            Debug.LogWarning("HealthUI: No HealthComponent found. Assign one in inspector or tag player as 'Player'.");
            return;
        }

        if (slider != null) {
            slider.maxValue = health.MaxHealth;
            slider.value = health.CurrentHealth;
        }

        if (text != null) {
            text.text = $"{health.CurrentHealth}/{health.MaxHealth}";
        }

        health.OnHealthChanged += OnHealthChanged;
    }

    void OnDestroy() {
        if (health != null)
        {
            health.OnHealthChanged -= OnHealthChanged;
        }
    }

    void OnHealthChanged(int current, int max) {
        if (slider != null)
        {
            slider.value = current;
        }

        if (text != null)
        {
            text.text = $"{current}/{max}";
        }
    }
}
