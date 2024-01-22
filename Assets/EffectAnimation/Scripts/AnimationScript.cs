using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public GameObject thingToSpawn;
    public GameObject thingToSpawn2;
    public GameObject thingToSpawn3;
    public GameObject thingToSpawn4;
    public GameObject demolishedHex;
    public Transform demolishedHexSpawnPoint;

    public Animator anim;
    public CameraScript cameraScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LandingAnimationEvent()
    {
        if (thingToSpawn)
        {
            // Create a new GameObject in the Scene where the GameObject(Projectile) was destroyed
            GameObject a = Instantiate(thingToSpawn, transform.position, transform.rotation);
            Destroy(a, 1);
        }

        if (thingToSpawn2)
        {
            // Create a new GameObject in the Scene where the GameObject(Projectile) was destroyed
            GameObject a = Instantiate(thingToSpawn2, transform.position, transform.rotation);
            Destroy(a, 1);
        }

    }

    public void SettlingAnimationEvent()
    {
        if (thingToSpawn3)
        {
            // Create a new GameObject in the Scene where the GameObject(Projectile) was destroyed
            GameObject a = Instantiate(thingToSpawn3, transform.position, transform.rotation);
            Destroy(a, 1);
        }
    }

    public void DemolishAnimationEvent()
    {
        if (demolishedHex)
        {
            // Create a new GameObject in the Scene where the GameObject(Projectile) was destroyed
            //Instantiate(demolishedHex, demolishedHexSpawnPoint.position, transform.rotation);
            Instantiate(thingToSpawn4, demolishedHexSpawnPoint.position, transform.rotation);
        }
    }

    public void DestroyAnimationEvent()
    {
        Destroy(gameObject);
    }
}
