using UnityEngine;

public abstract class EnemyBase : MonoBehaviour, IAttackable {
    [SerializeField] protected string nick;
    [SerializeField] protected float health = 100f;
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float attackDamage = 20f;
    [SerializeField] protected float attackRadius = 2f;
    [SerializeField] protected float detectionRadius = 10f;
    [SerializeField] protected Material normalMaterial;
    [SerializeField] protected Material damageMaterial;

    protected Renderer enemyRenderer;
    protected bool isMoving;
    protected float currentHealth;
    protected Transform currentTarget;
    protected float nextTargetSearchTime;
    protected float targetSearchInterval = 1f;

    // Event for death notification
    public delegate void EnemyDeathHandler();
    public event EnemyDeathHandler OnEnemyDeath;

    protected virtual void Start() {
        currentHealth = health;
        enemyRenderer = GetComponent<Renderer>();
        if (enemyRenderer != null) {
            normalMaterial = enemyRenderer.material;
        }

        // Ensure we have required components for collision detection
        if (GetComponent<Collider>() == null) {
            var collider = gameObject.AddComponent<SphereCollider>();
            ((SphereCollider)collider).radius = attackRadius;
            collider.isTrigger = true;
        }

        if (GetComponent<Rigidbody>() == null) {
            var rb = gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = true; // We'll control movement via transform
            rb.useGravity = false;
        }

        // Set the enemy tag if not set
        if (gameObject.tag != "Enemy") {
            gameObject.tag = "Enemy";
        }

        InvokeRepeating(nameof(Move), 0.5f, 1f);
    }

    public abstract void Move();

    protected virtual void Update() {
        if (isMoving) {
            // Visual feedback for movement
            transform.Rotate(Vector3.up, 50f * Time.deltaTime);
        }

        // Search for nearest enemy periodically
        if (Time.time >= nextTargetSearchTime) {
            FindNearestEnemy();
            nextTargetSearchTime = Time.time + targetSearchInterval;
        }

        // Move towards target if we have one
        if (currentTarget != null && IsAlive()) {
            var distance = Vector3.Distance(transform.position, currentTarget.position);

            // If within detection radius but outside attack radius, move towards target
            if (distance <= detectionRadius && distance > attackRadius) {
                var direction = (currentTarget.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;

                // Rotate to face target
                var targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);

                isMoving = true;
            } else {
                isMoving = false;
            }
        }
    }

    protected virtual void FindNearestEnemy() {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        var nearestDistance = float.MaxValue;
        Transform nearestEnemy = null;

        foreach (var enemy in enemies) {
            // Skip self and dead enemies
            if (enemy == gameObject) continue;

            var enemyComponent = enemy.GetComponent<EnemyBase>();
            if (enemyComponent == null || !enemyComponent.IsAlive()) continue;

            var distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < nearestDistance && distance <= detectionRadius) {
                nearestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        currentTarget = nearestEnemy;
    }

    public virtual void TakeDamage(float damage) {
        currentHealth -= damage;
        Debug.Log($"{nick} took {damage} damage. Health: {currentHealth}");

        // Visual feedback for damage
        if (enemyRenderer != null && damageMaterial != null) {
            StartCoroutine(ShowDamageEffect());
        }

        if (currentHealth <= 0) {
            Die();
        }
    }

    public float GetHealth() {
        return currentHealth;
    }

    public bool IsAlive() {
        return currentHealth > 0;
    }

    protected virtual void Die() {
        Debug.Log($"{nick} has been defeated!");
        OnEnemyDeath?.Invoke();
        Destroy(gameObject);
    }

    protected virtual void OnTriggerStay(Collider other) {
        // Check if the other object is our current target
        if (currentTarget != null && other.gameObject == currentTarget.gameObject) {
            var attackable = other.GetComponent<IAttackable>();
            if (attackable != null && other.CompareTag("Enemy")) {
                // Only attack if within attack radius
                var distance = Vector3.Distance(transform.position, other.transform.position);
                if (distance <= attackRadius) {
                    Debug.Log($"{nick} attacks {other.gameObject.GetComponent<EnemyBase>().nick} at distance {distance}");
                    attackable.TakeDamage(attackDamage);
                }
            }
        }
    }

    private System.Collections.IEnumerator ShowDamageEffect() {
        enemyRenderer.material = damageMaterial;
        yield return new WaitForSeconds(0.2f);
        enemyRenderer.material = normalMaterial;
    }

    private void OnDrawGizmos() {
        // Visual debug for attack radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}

