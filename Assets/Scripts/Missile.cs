using Unity.VisualScripting;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float lifeTime = 6f;
    public GameObject explosionPrefab;
    public float explodeRadius = 3f;
    public float explodeForce = 500f;
    public int damage = 100;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision coll)
    {
        var checkDrone = coll.gameObject.GetComponent<DroneController>();
        if (checkDrone != null) return;

        var checkDrone2 = coll.gameObject.GetComponent<DroneHealth>();
        if (checkDrone2 != null) return;
        
        // spawn explosion
        if (explosionPrefab)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // damage targets/enemies within radius
        Collider[] cols = Physics.OverlapSphere(transform.position, explodeRadius);
        foreach (var c in cols)
        {
            // enemy removal or target damage
            var dmg = c.GetComponent<Damageable>();
            if (dmg != null)
            {
                // try call TakeDamage if present
                dmg.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
