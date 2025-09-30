using UnityEngine;

/// <summary>
/// Orc class
/// </summary>
public class EnemyOrc : EnemyBase, IAttackable {

    /// <summary>
    /// Init values
    /// </summary>
    void Start() {
        Nick = $"Orc_{Random.Range(1, 10000)}";
        Health = 50f;
        Speed = 4f;
        Damage = 5f;
        InvokeRepeating(nameof(Move), 0f, 2f);
        InvokeRepeating(nameof(Attack), 0f, 5f);
    }

    /// <summary>
    /// Move to
    /// </summary>
    public override void Move() {
        base.Move();
        Debug.Log($"{Nick} is moving with speed {Speed}");
    }

    /// <summary>
    /// Attack the target
    /// </summary>
    public void Attack() {
        Debug.Log($"{Nick} is attacking");
    }

    /// <summary>
    /// Called when the entity is attacked
    /// </summary>
    /// <param name="damage">Damage value</param>
    public void OnAttack(float damage) {
        Health -= damage;
        Debug.LogWarning($"{Nick} has been attacked and got {damage} damage, he is now at {Health} health");
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
