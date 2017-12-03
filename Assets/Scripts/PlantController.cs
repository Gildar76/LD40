using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    Animator plantAnimation;
    public float animationDuration = 0.0f;
    public float animSpeed = 0.1f;
    public float seedTimer = 0.0f;
    public float timetStartSeeding = 5.0f;
    bool watingToSeed = false;
    ParticleSystem seeder;
    private void Awake()
    {

        plantAnimation = GetComponent<Animator>();
        plantAnimation.speed = animSpeed;
        
    }

    private void OnEnable()
    {




    }

    private void Update()
    {
        if (seeder == null)
        {
            seeder = transform.parent.GetComponentInChildren<ParticleSystem>();
            //seeder.Stop();
        }
        animationDuration += Time.deltaTime;
        if (animationDuration >= 0.3f / animSpeed)
        {
            plantAnimation.speed = 0.0f;
        }
        if (plantAnimation.speed == 0.0f)
        {
            seedTimer += Time.deltaTime;
            
        }
        if (seedTimer >= timetStartSeeding && !seeder.isPlaying)
        {


            seeder.Play();
        }
    }
}
