using UnityEngine;

public class EnemyHealth : MonoBehaviour, Damageable
{
    public int health = 100;
    public Animator anim;
    public GameObject deathExplosionPrefab;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        // Spawn explosion if assigned
        if (deathExplosionPrefab != null)
            Instantiate(deathExplosionPrefab, transform.position, Quaternion.identity);

        // Destroy enemy object
        anim.SetTrigger("Dead");
        Destroy(gameObject, 4);
    }
}
