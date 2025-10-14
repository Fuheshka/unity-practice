using UnityEngine;

/// <summary>
/// Finite State Machine for NPC
/// </summary>
public class FSM : MonoBehaviour {

    /// <summary>
    /// The current state of the FSM
    /// </summary>
    FSMState _current = FSMState.Idle;

    /// <summary>
    /// Cached reference to the Movement component
    /// </summary>
    Movement _movement = null;

    /// <summary>
    /// Cached reference to the Attack component
    /// </summary>
    Attack _attack = null;

    /// <summary>
    /// Start the FSM
    /// </summary>
    void Start() {
        TryGetComponent<Movement>(out _movement);
        TryGetComponent<Attack>(out _attack);
        if (_movement == null) {
            Debug.LogError($"[{nameof(FSM).ToUpperInvariant()}] missing component: {nameof(Movement)}");
            return;
        }
        if (_attack == null) {
            Debug.LogError($"[{nameof(FSM).ToUpperInvariant()}] missing component: {nameof(Attack)}");
            return;
        }
    }

    /// <summary>
    /// NPC behavior
    /// </summary>
    void Update() {
        switch (_current) {
            case FSMState.Idle:
                _movement.Stop();
                break;
            case FSMState.Follow:
                _movement.Move();
                break;
            case FSMState.Attack:
                _attack.Use();
                break;
        }
    }

    /// <summary>
    /// Change the current state of the FSM
    /// </summary>
    /// <param name="state">The new state to transition to</param>
    public void ChangeState(FSMState state) {
        _current = state;
    }
}
