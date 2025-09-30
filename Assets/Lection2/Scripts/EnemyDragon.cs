using UnityEngine;

/// <summary>
/// Dragon class
/// </summary>
public class EnemyDragon : EnemyBase {

    /// <summary>
    /// Fly speed
    /// </summary>
    readonly float _flySpeed = 20f;

    /// <summary>
    /// Init values
    /// </summary>
    void Start() {
        Nick = $"Dragon_{Random.Range(1, 10000)}";
        Health = 300f;
        Speed = 1f;
        InvokeRepeating(nameof(Move), 0f, 2f);
        InvokeRepeating(nameof(Fly), 0f, 5f);
    }

    /// <summary>
    /// Move to
    /// </summary>
    public override void Move() {
        base.Move();
        Debug.Log($"{Nick} is moving with speed {Speed}");
    }

    /// <summary>
    /// Fly to
    /// </summary>
    public void Fly() {
        transform.position += Vector3.forward * _flySpeed;
        Debug.Log($"{Nick} is flying with speed {_flySpeed}");
    }
}
