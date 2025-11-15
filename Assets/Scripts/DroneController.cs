using UnityEngine;

public class DroneController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;
    public float ascendSpeed = 5f;
    public float rotationSpeed = 5f;

    [Header("Missile")]
    public GameObject missilePrefab;
    public Transform missileSpawn;
    public float missileForce = 800f;
    public float fireCooldown = 0.5f;

    Rigidbody rb;
    float lastFireTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        HandleFire();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal"); // A/D
        float v = Input.GetAxis("Vertical");   // W/S

        Vector3 forward = transform.forward * v;
        Vector3 right = transform.right * h;
        Vector3 move = (forward + right).normalized * moveSpeed;

        float ascend = 0f;
        if (Input.GetKey(KeyCode.Q)) ascend = -1f;
        if (Input.GetKey(KeyCode.E)) ascend = 1f;
        move += Vector3.up * ascend * ascendSpeed;

        // Move using velocity
        Vector3 desiredVel = move;
        rb.linearVelocity = new Vector3(desiredVel.x, desiredVel.y, desiredVel.z);
    }

    void HandleFire()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastFireTime >= fireCooldown)
        {
            if (missilePrefab != null && missileSpawn != null)
            {
                GameObject m = Instantiate(missilePrefab, missileSpawn.position, missileSpawn.rotation);
                Rigidbody mr = m.GetComponent<Rigidbody>();
                if (mr != null) mr.AddForce(missileSpawn.forward * missileForce);
            }
            lastFireTime = Time.time;
        }
    }
}
