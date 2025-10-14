using UnityEngine;

/// <summary>
/// Demo Event Bus
/// </summary>
public class DemoEventBus : MonoBehaviour {

    /// <summary>
    /// Start the event bus
    /// </summary>
    void Start() {
        EventBus.Subscribe<CustomEvents>(OnCustomEvent);
    }

    /// <summary>
    /// Custom event handler
    /// </summary>
    /// <param name="data">Custom event data</param>
    void OnCustomEvent(CustomEvents data) {
        switch (data) {
            case CustomEvents.PlayerDetected:
                Debug.Log($"[{nameof(DemoEventBus).ToUpperInvariant()}] npc detected player");
                break;
            case CustomEvents.WallDetected:
                Debug.Log($"[{nameof(DemoEventBus).ToUpperInvariant()}] hero touched wall");
                break;
            default:
                break;
        }
    }
}
