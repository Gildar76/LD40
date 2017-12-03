using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int health;
    [SerializeField]
    private int numSeeds;
    [SerializeField]
    private int foodCount;
    [SerializeField]
    private float hungerMeter;
    public bool canPlant = false;
    public GameObject currentPlatSlot;
    private GameObject currentPlant;
    Animator anim;
    public float timeSinceLastHealthReduction = 0.0f;

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public int NumSeeds
    {
        get
        {
            return numSeeds;
        }

        set
        {
            numSeeds = value;
        }
    }

    public int FoodCount
    {
        get
        {
            return foodCount;
        }

        set
        {
            foodCount = value;
        }
    }

    public float HungerMeter
    {
        get
        {
            return hungerMeter;
        }

        set
        {
            hungerMeter = value;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (health <= 0) anim.SetBool("Dead", true);
        timeSinceLastHealthReduction += Time.deltaTime;
        if (timeSinceLastHealthReduction >= 10.0f)
        {
            Health -= NumSeeds;
        }
        if (NumSeeds < 1) canPlant = false;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canPlant)
            {
                if (!currentPlatSlot.GetComponent<PlantSlotController>().HasPlant)
                {
                    PlantSeed();
                } else
                {
                    HarvestPlant();
                }

            } else
            {
                HarvestPlant();
            }
        }
    }

    private void FixedUpdate()
    {
         
        Vector2 movementVec = new Vector2(Input.GetAxis("Horizontal"), 0);
        rb.velocity = movementVec * Speed;
        if (rb.velocity.x >= 0.2f || rb.velocity.x <= -0.2f)
        {
            if (rb.velocity.x <= 0f) anim.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            if (rb.velocity.x >= 0f) anim.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            anim.SetBool("Moving", true);
        } else
        {
            anim.SetBool("Moving", false);
        }

    }

    public void PlantSeed()
    {
        if (!canPlant || currentPlatSlot == null) return;
        if (currentPlatSlot.GetComponent<PlantSlotController>().HasPlant) return;
        currentPlatSlot.GetComponent<PlantSlotController>().HasPlant = true;
        NumSeeds--;
        foreach (Transform child in currentPlatSlot.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Plantslots")
        {

            currentPlatSlot = other.gameObject;
            canPlant = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Plantslots")
        {

            currentPlatSlot = null;
            canPlant = false;
        }
    }

    private void HarvestPlant()
    {
        if (currentPlant != null) return;
        if (currentPlatSlot == null) return;
        //Find the plant
        currentPlant = currentPlatSlot.GetComponent<PlantSlotController>().PickPlant();
        currentPlant.transform.parent = transform;
        currentPlant.transform.localPosition = new Vector3(0.0f, 1.0f, 0.0f);
        

    }

    //private void OnParticleCollision(GameObject other)
    //{
    //    if (other.gameObject.tag == "Seeds")
    //    {
    //        numSeeds++;
    //        Destroy(other);
    //        //other.gameObject.SetActive(false);
    //        SendMessage("DestroyParticle", GetComponents<Collider>());
    //    }
    //    Debug.Log("COlliding Normal");
    //}

    //private void OnParticleTrigger()
    //{
    //    Debug.Log("COlliding trigger");
    //    numSeeds++;
    //}

}
