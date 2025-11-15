using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Animator anim;
    public float waitTime = 1.5f;
    public float detectionRadius = 12f;
    public LayerMask droneLayer;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.2f;
    public float bulletForce = 600f;

    NavMeshAgent agent;
    float waitTimer = 0f;
    float lastFire = 0f;
    GameObject drone;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (patrolPoints.Length > 0) agent.SetDestination(patrolPoints[Random.Range(0, patrolPoints.Length)].position);
    }

    void Update()
    {
        // Check for drone in radius:
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, droneLayer);
        bool seeDrone = hits.Length > 0;
        if (seeDrone)
        {
            drone = hits[0].gameObject;
            agent.isStopped = true;
            anim.SetBool("Firing", true);
            // aim at drone
            Vector3 dir = drone.transform.position - firePoint.position;
            dir.y = 0;
            firePoint.rotation = Quaternion.LookRotation(dir);
            if (dir != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5f);

            // shoot
            if (Time.time - lastFire >= fireRate && bulletPrefab != null && firePoint != null)
            {
                GameObject b = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody br = b.GetComponent<Rigidbody>();
                if (br) br.AddForce(firePoint.forward * bulletForce);
                lastFire = Time.time;
            }
        }
        else
        {
            // Patrol logic
            agent.isStopped = false;
            anim.SetBool("Firing", false);
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                if (waitTimer <= 0f)
                {
                    waitTimer = waitTime;
                    //current = (current + 1) % patrolPoints.Length;
                    agent.SetDestination(patrolPoints[Random.Range(0, patrolPoints.Length)].position);
                }
                else waitTimer -= Time.deltaTime;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
