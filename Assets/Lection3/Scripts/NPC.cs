using UnityEngine;

/// <summary>
/// NPC
/// </summary>
[RequireComponent(typeof(Detector))]
[RequireComponent(typeof(FSM))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(UI))]
public class NPC : MonoBehaviour {

    /// <summary>
    /// Attack range
    /// </summary>
    [SerializeField]
    float _attackRange = 1f;

    /// <summary>
    /// Detector component
    /// </summary>
    Detector _detector = null;

    /// <summary>
    /// Finite State Machine
    /// </summary>
    FSM _fsm = null;

    /// <summary>
    /// Cached reference to the Movement component
    /// </summary>
    Movement _movement = null;

    /// <summary>
    /// Health component
    /// </summary>
    Health _health = null;

    /// <summary>
    /// UI component
    /// </summary>
    UI _ui = null;

    /// <summary>
    /// Start the NPC
    /// </summary>
    void Start() {
        TryGetComponent<Detector>(out _detector);
        TryGetComponent<FSM>(out _fsm);
        TryGetComponent<Movement>(out _movement);
        TryGetComponent<Health>(out _health);
        TryGetComponent<UI>(out _ui);
        _detector.OnDetected += OnDetected;
        _detector.OnLost += OnLost;
        _health.OnHealthChanged += OnHealthChanged;
    }

    /// <summary>
    /// Called when the health value changes
    /// </summary>
    /// <param name="value">Health value</param>
    void OnHealthChanged(int value) {
        _ui.SetHealth(value);
        if (value <= 0) {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Called when the detector loses a target
    /// </summary>
    void OnLost() {
        _fsm.ChangeState(FSMState.Idle);
    }

    /// <summary>
    /// Called when the detector detects a target
    /// </summary>
    /// <param name="vector">The position of the detected target</param>
    /// <param name="range">The distance to the detected target</param>
    void OnDetected(Vector3 vector, float range) {
        _movement.SetTargetPosition(vector);
        if (range <= _attackRange) {
            _fsm.ChangeState(FSMState.Attack);
        } else {
            EventBus.Publish<CustomEvents>(CustomEvents.PlayerDetected);
            _fsm.ChangeState(FSMState.Follow);
        }
    }
}
