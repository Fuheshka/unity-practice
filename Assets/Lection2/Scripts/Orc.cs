using UnityEngine;

public class Orc : EnemyBase {
    private Vector3 targetPosition;
    private float nextPositionTime;

    protected override void Start() {
        base.Start();
        SetNewTargetPosition();
    }

    public override void Move() {
        if (Time.time >= nextPositionTime) {
            SetNewTargetPosition();
        }

        // Move towards target position
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        // Update movement animation state
        isMoving = (transform.position - targetPosition).sqrMagnitude > 0.1f;

        // Update rotation to face movement direction
        if (isMoving) {
            var direction = (targetPosition - transform.position).normalized;
            if (direction != Vector3.zero) {
                var targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
            }
        }

        Debug.Log($"{nick} is moving swiftly to {targetPosition}");
    }

    private void SetNewTargetPosition() {
        // Get random position within a circle
        var randomPoint = Random.insideUnitCircle * 10f;
        targetPosition = transform.position + new Vector3(randomPoint.x, 0, randomPoint.y);
        nextPositionTime = Time.time + Random.Range(3f, 6f);
    }

    private void OnTriggerEnter(Collider other) {
        // Orc specific attack behavior
        if (other.CompareTag("Enemy") && other.TryGetComponent<IAttackable>(out var attackable)) {
            Debug.Log($"{nick} performs a brutal attack!");
            // Orcs do extra damage on their attacks
            attackable.TakeDamage(attackDamage * 1.5f);
        }
    }
}

