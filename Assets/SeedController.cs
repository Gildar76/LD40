using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{
    ParticleSystem ps;
    ParticleSystem.Particle[] parts;
    List<ParticleCollisionEvent> collisionEvents;
    private void Awake()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        ps = GetComponent<ParticleSystem>();
    }


    private void OnEnable()
    {
        ps.Stop();
    }

    public void DestroyParticle(List<Collider> colliders)
    {


    }

    //private void LateUpdate()
    //{


    //    // GetParticles is allocation free because we reuse the m_Particles buffer between updates



    //    for (int i = 0; i < numParticlesAlive; i++)
    //    {
    //        m_Particles[i].velocity += Vector3.up * m_Drift;
    //    }

    //    // Apply the particle changes to the particle system
    //    m_System.SetParticles(m_Particles, numParticlesAlive);
    //}

    void InitializeIfNeeded()
    {
        if (ps == null)
            ps = GetComponent<ParticleSystem>();

        if (parts == null || parts.Length < ps.main.maxParticles)
            parts = new ParticleSystem.Particle[ps.main.maxParticles];
    }

    public void OnParticleCollision(GameObject other)
    {
        Collider[] cols = other.GetComponents<Collider>();

        if (other.tag != "Player") return;

        int collCount = ps.GetSafeCollisionEventSize();



        int eventCount = ps.GetCollisionEvents(other, collisionEvents);
        InitializeIfNeeded();
        int numParticlesAlive = ps.GetParticles(parts);




        for (int i = 0; i < eventCount; i++)
        {

            for (int j = 0; j < parts.Length; j++ )
            {
                foreach (Collider col in cols)
                {


                    if (col.bounds.Contains(parts[j].position))
                    {
                        Debug.Log("Killing particle");
                        parts[j].remainingLifetime = 0.01f;
                        other.GetComponent<PlayerController>().NumSeeds++;
                       
                        
                    }
                }




            }

            ps.SetParticles(parts, ps.main.maxParticles);
            
        }
    }

}
