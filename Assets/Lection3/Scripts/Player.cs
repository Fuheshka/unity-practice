using UnityEngine;

/// <summary>
/// Simple player movement
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Gun))]
public class Player : MonoBehaviour {

    /// <summary>
    /// Wall object name
    /// </summary>
    const string WALL_OBJECT = "Wall";

    /// <summary>
    /// NPC object name
    /// </summary>
    const string NPC_OBJECT = "NPC";

    /// <summary>
    /// Movement speed (units/second)
    /// </summary>
    [SerializeField]
    float _speed = 5f;

    /// <summary>
    /// Rotation speed (degrees/second)
    /// </summary>
    [SerializeField]
    float _rotationSpeed = 720f;

    /// <summary>
    /// Contact score for NPCs
    /// </summary>
    [SerializeField]
    int _contactScore = 10;

    /// <summary>
    /// Reference to the demo history
    /// </summary>
    [SerializeField]
    DemoHistory _history = null;

    /// <summary>
    /// Cached Rigidbody component
    /// </summary>
    Rigidbody _body = null;

    /// <summary>
    /// Gun component
    /// </summary>
    Gun _gun = null;

    /// <summary>
    /// Initialize
    /// </summary>
    void Start() {
        _body = GetComponent<Rigidbody>();
        _gun = GetComponent<Gun>();
    }

    /// <summary>
    /// Fixed update for physics calculations
    /// </summary>
    void FixedUpdate() {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var movement = new Vector3(horizontal, 0f, vertical).normalized;
        var velocity = movement * _speed;
        if (velocity != Vector3.zero) {
            var cmd = new MoveCommand(_body, movement, velocity, _rotationSpeed);
            _history.Add(cmd);
            cmd.Execute();
        }
    }

    /// <summary>
    /// Using gun example and object pooling
    /// </summary>
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            _gun.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            _gun.Reload();
        }
    }

    /// <summary>
    /// Raise when the player collides with another object
    /// </summary>
    void OnCollisionEnter(Collision collision) {
        var item = collision.gameObject.name;
        Debug.Log($"[{nameof(Player).ToUpperInvariant()}] collided with: {item}");
        if (item.Equals(WALL_OBJECT)) {
            EventBus.Publish<CustomEvents>(CustomEvents.WallDetected);
        }
        if (item.Equals(NPC_OBJECT)) {
            Score.Add(_contactScore);
        }
    }
}
