using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 25;
    public float lifeTime = 5f;

    void Start() => Destroy(gameObject, lifeTime);

    void OnCollisionEnter(Collision col)
    {
        var dmg  = col.gameObject.GetComponent<Damageable>();
        if (dmg  != null) dmg.TakeDamage(damage);

        Destroy(gameObject);
    }
}
