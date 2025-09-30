using UnityEngine;

public class Troll : EnemyBase {
    [SerializeField] private float regenerationAmount = 10f;
    [SerializeField] private float regenerationInterval = 5f;

    private Vector3 targetPosition;
    private float nextPositionTime;
    private float lastRegenerationTime;

    protected override void Start() {
        base.Start();
        SetNewTargetPosition();
        InvokeRepeating(nameof(Regenerate), regenerationInterval, regenerationInterval);
    }

    public override void Move() {
        if (Time.time >= nextPositionTime) {
            SetNewTargetPosition();
        }

        // Trolls move slower but are more tanky
        var step = speed * 0.7f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // Update movement animation state
        isMoving = (transform.position - targetPosition).sqrMagnitude > 0.1f;

        // Update rotation to face movement direction
        if (isMoving) {
            var direction = (targetPosition - transform.position).normalized;
            if (direction != Vector3.zero) {
                var targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime);
            }
        }

        Debug.Log($"{nick} is moving heavily to {targetPosition}");
    }

    private void SetNewTargetPosition() {
        // Trolls prefer shorter movement distances
        var randomPoint = Random.insideUnitCircle * 5f;
        targetPosition = transform.position + new Vector3(randomPoint.x, 0, randomPoint.y);
        nextPositionTime = Time.time + Random.Range(4f, 8f);
    }

    public void Regenerate() {
        if (!IsAlive()) return;

        currentHealth = Mathf.Min(currentHealth + regenerationAmount, health);
        Debug.Log($"{nick} regenerated {regenerationAmount} health. Current health: {currentHealth}");
    }

    public override void TakeDamage(float damage) {
        // Trolls have natural damage resistance
        base.TakeDamage(damage * 0.8f);
    }

    private void OnTriggerEnter(Collider other) {
        // Troll specific attack behavior
        if (other.CompareTag("Enemy") && other.TryGetComponent<IAttackable>(out var attackable)) {
            Debug.Log($"{nick} performs a crushing attack!");
            // Trolls do normal damage but have a chance to heal when attacking
            attackable.TakeDamage(attackDamage);
            if (Random.value < 0.3f) {
                Regenerate();
            }
        }
    }
}

