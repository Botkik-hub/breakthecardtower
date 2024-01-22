using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript_Nexus : MonoBehaviour
{
    public GameObject thingToSpawn1;
    public GameObject thingToSpawn2;
    public GameObject thingToSpawn3;

    public Transform thingToSpawn1SpawnPoint;
    public Transform thingToSpawn2SpawnPoint;
    public Transform thingToSpawn3SpawnPoint;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LandingAnimationEvent()
    {
        if (thingToSpawn1)
        {
            // 
            Instantiate(thingToSpawn1, thingToSpawn1SpawnPoint.position, thingToSpawn1SpawnPoint.rotation);
        }
    }

    public void ActivatedAnimationEvent()
    {
        if (thingToSpawn2)
        {
            // 
            Instantiate(thingToSpawn2, thingToSpawn2SpawnPoint.position, thingToSpawn2SpawnPoint.rotation);
        }
    }

    public void ExplosionAnimationEvent()
    {       
        if (thingToSpawn3)
        {
            // 
            Instantiate(thingToSpawn3, thingToSpawn3SpawnPoint.position, thingToSpawn3SpawnPoint.rotation);
        }
    }

    public void DestroyAnimationEvent()
    {
        Destroy(gameObject);
    }
}
