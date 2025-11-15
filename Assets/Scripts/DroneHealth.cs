using UnityEngine;

public class DroneHealth : MonoBehaviour, Damageable
{
    public int health = 200;
    public GameObject explosionPrefab;

    public void TakeDamage(int damage)
    {
        health -= damage;
        MenuUI.Instance.SetHealth(health);

        if (health <= 0) Die();
    }

    void Die()
    {
        // explosion + disable controls
        if (explosionPrefab)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
