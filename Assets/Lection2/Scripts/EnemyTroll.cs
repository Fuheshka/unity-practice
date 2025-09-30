using UnityEngine;

/// <summary>
/// Troll class
/// </summary>
public class EnemyTroll : EnemyBase, IAttackable {

    /// <summary>
    /// Init values
    /// </summary>
    void Start() {
        Nick = $"Troll_{Random.Range(1, 10000)}";
        Health = 100f;
        Speed = 2f;
        Damage = 15f;
        InvokeRepeating(nameof(Move), 0f, 2f);
    }

    /// <summary>
    /// Move to
    /// </summary>
    public override void Move() {
        base.Move();
        transform.position += Vector3.back;
        Debug.Log($"{Nick} is moving with speed {Speed}");
    }

    /// <summary>
    /// Called when the entity is attacked
    /// </summary>
    /// <param name="damage">Damage value</param>
    public void OnAttack(float damage) {
        Health -= damage / 2f;
        Debug.LogWarning($"{Nick} has been attacked and took {damage} damage, he is now at {Health} health");
        if (Health <= 0f) {
            Debug.LogError($"{Nick} has been defeated");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Called when the entity collides with another object
    /// </summary>
    /// <param name="collision">Collision data</param>
    public void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.TryGetComponent<IAttackable>(out var attackable)) {
            attackable.OnAttack(Damage);
        }
    }
}
