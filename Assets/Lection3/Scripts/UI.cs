using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Health Bar UI
/// </summary>
public class UI : MonoBehaviour {

    /// <summary>
    /// Yellow color health threshold
    /// </summary>
    const int YELLOW_HEALTH_THRESHOLD = 75;

    /// <summary>
    /// Red color health threshold
    /// </summary>
    const int RED_HEALTH_THRESHOLD = 30;

    /// <summary>
    /// Health bar image
    /// </summary>
    [SerializeField]
    Image _bar = null;

    /// <summary>
    /// Offset position for the health bar
    /// </summary>
    [SerializeField]
    Vector3 _offset = new Vector3(0f, 2f, 0f);

    /// <summary>
    /// Cached bar transform
    /// </summary>
    Transform _target = null;

    /// <summary>
    /// Initialize the health bar
    /// </summary>
    void Start() {
        _target = _bar.transform;
    }

    /// <summary>
    /// Update the health bar position and rotation
    /// </summary>
    void Update() {
        _target.SetPositionAndRotation(transform.position + _offset, Quaternion.LookRotation(transform.position - Camera.main.transform.position));
    }

    /// <summary>
    /// Set the health value
    /// </summary>
    /// <param name="health">The health value</param>
    public void SetHealth(int health) {
        _bar.fillAmount = health / 100f;
        _bar.color = health switch {
            <= RED_HEALTH_THRESHOLD => Color.red,
            <= YELLOW_HEALTH_THRESHOLD => Color.yellow,
            _ => Color.green,
        };
        Debug.Log($"[{nameof(UI).ToUpperInvariant()}] health percent: {_bar.fillAmount}");
    }
}
