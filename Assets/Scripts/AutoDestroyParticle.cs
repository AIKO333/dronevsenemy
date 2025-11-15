using UnityEngine;

public class AutoDestroyParticle : MonoBehaviour
{
    void Start()
    {
        var ps = GetComponent<ParticleSystem>();
        if (ps) Destroy(gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
        else Destroy(gameObject, 3f);
    }
}
