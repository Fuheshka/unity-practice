using UnityEngine;

public interface IAttackable {
    void TakeDamage(float damage);
    float GetHealth();
    bool IsAlive();
}
