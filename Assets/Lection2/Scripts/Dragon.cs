using UnityEngine;

public class Dragon : EnemyBase {
    [SerializeField] private float flyingHeight = 5f;
    [SerializeField] private float swoopSpeed = 15f;
    [SerializeField] private float hoverSpeed = 3f;

    private Vector3 targetPosition;
    private float nextPositionTime;
    private bool isFlying;
    private float originalY;

    protected override void Start() {
        base.Start();
        originalY = transform.position.y;
        SetNewTargetPosition();
        InvokeRepeating(nameof(Fly), 1f, 10f);
    }

    public override void Move() {
        if (Time.time >= nextPositionTime) {
            SetNewTargetPosition();
        }

        float currentSpeed = isFlying ? swoopSpeed : speed;
        var step = currentSpeed * Time.deltaTime;

        // Calculate target Y position based on flying state
        float targetY = isFlying ? originalY + flyingHeight : originalY;
        Vector3 currentTarget = new Vector3(targetPosition.x, targetY, targetPosition.z);

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);

        // Update movement animation state
        isMoving = (transform.position - currentTarget).sqrMagnitude > 0.1f;

        // Update rotation to face movement direction
        if (isMoving) {
            var direction = (currentTarget - transform.position).normalized;
            if (direction != Vector3.zero) {
                var targetRotation = Quaternion.LookRotation(direction);
                // Faster rotation while flying
                float rotationSpeed = isFlying ? 8f : 5f;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        Debug.Log($"{nick} is {(isFlying ? "flying" : "moving on the ground")} to {targetPosition}");
    }

    private void SetNewTargetPosition() {
        // Dragons can move in a larger area
        var randomPoint = Random.insideUnitCircle * 15f;
        targetPosition = transform.position + new Vector3(randomPoint.x, 0, randomPoint.y);
        nextPositionTime = Time.time + Random.Range(3f, 7f);
    }

    public void Fly() {
        isFlying = !isFlying;
        Debug.Log($"{nick} {(isFlying ? "takes off into the air!" : "lands on the ground.")}");
    }

    public override void TakeDamage(float damage) {
        // Dragons take less damage while flying
        float actualDamage = isFlying ? damage * 0.7f : damage;
        base.TakeDamage(actualDamage);
    }

    private void OnTriggerEnter(Collider other) {
        // Dragon specific attack behavior
        if (other.CompareTag("Enemy") && other.TryGetComponent<IAttackable>(out var attackable)) {
            Debug.Log($"{nick} performs a {(isFlying ? "swooping" : "fierce")} attack!");
            // Dragons do more damage while flying
            float actualDamage = isFlying ? attackDamage * 1.8f : attackDamage;
            attackable.TakeDamage(actualDamage);
        }
    }

    protected override void Update() {
        base.Update();

        // Add hovering motion while flying
        if (isFlying) {
            float hoverOffset = Mathf.Sin(Time.time * hoverSpeed) * 0.5f;
            transform.position += Vector3.up * hoverOffset * Time.deltaTime;
        }
    }
}
