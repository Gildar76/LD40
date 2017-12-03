using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSlotController : MonoBehaviour
{
    public GameObject plant;
    public int MaxFoodGain { get; set; }
    public bool HasPlant { get; set; }
    public int FoodGain { get; set; }
    public float FoodGainTimer { get; set; }
    private GameObject plantRef;
    private GameObject seedSpawnerRef;
    public List<Sprite> sprites;

    private void Awake()
    {
        
        FoodGainTimer = 0.0f;
        HasPlant = false;
        FoodGain = 0;
        MaxFoodGain = 5;
    }

    private void Update()
    {
        if (HasPlant && FoodGain < MaxFoodGain)
        {
            FoodGainTimer += Time.deltaTime;
            if (FoodGainTimer >= 5.0f)
            {
                FoodGainTimer = 0.0f;
                FoodGain++;
            }
        }

       

    }

    public GameObject PickPlant()
    {
        if (!HasPlant) return null;
        if (plantRef == null)
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.name == "Plant") plantRef = child.gameObject;
                if (child.gameObject.name == "SeedSpawner") seedSpawnerRef = child.gameObject;
            }
        }
        GameObject go = GameObject.Instantiate(plantRef);
        go.GetComponent<PlantController>().enabled = false;
        //go.GetComponent<SeedController>().enabled = false;
        seedSpawnerRef.GetComponent<ParticleSystem>().Stop();
        go.GetComponent<Animator>().enabled = false;
        go.GetComponent<SpriteRenderer>().sprite = sprites[FoodGain];
        Debug.Log("food gain is" + FoodGain);
        //Animator plantAnimation = go.GetComponent<Animator>();
        go.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        plantRef.SetActive(false);
        seedSpawnerRef.SetActive(false);
        go.GetComponent<PlantController>().enabled = false;
        //AnimatorStateInfo si = plantAnimation.GetCurrentAnimatorStateInfo(0);
        ////AnimationClip[] clip = plantAnimation.GetCurrentAnimatorClipInfo(0);

        //AnimatorClipInfo[] clipInfo =  plantAnimation.GetCurrentAnimatorClipInfo(0);
        //foreach (AnimatorClipInfo c in clipInfo)
        //{
        //    c.clip.
        //}
        //float animationPercent = (si.normalizedTime % 1.0f);
        //// Here we get the current frame using the animationPercent
        //int currentFrame = Mathf.FloorToInt(animationPercent * 5.0f) + 1;
        return go;

    }

}
