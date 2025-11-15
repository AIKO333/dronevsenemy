using UnityEngine;

public class Target : MonoBehaviour, Damageable
{
    public int health = 1;
    public Animator anim;

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            anim.SetTrigger("Hit");
            Destroy(gameObject, 1);
        }
    }
}
