using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimationScript_Scale : MonoBehaviour, IScalePresenter
{
    public GameObject thingToSpawn;
    public GameObject thingToSpawn2;
    public GameObject thingToSpawn3;

    public Transform ImpactSpawnPoint2;
    public Transform ImpactSpawnPoint1;

    public Animator anim;
    public int DamageOnScale;

    public Text scoreText;
    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

     IEnumerator ActivateScaleAnimation()
    {
        // Activates scale animation
        yield return new WaitForSeconds (0.25f);
        anim.SetTrigger("IsDamaged");
    }

    public void DestroyAnimationEvent()
    {
        if (thingToSpawn3)
        {
            // Create a new GameObject in the Scene where the GameObject(Projectile) was destroyed
            Instantiate(thingToSpawn3, transform.position, transform.rotation);
        }
    }
    
    public void PlayerDamageAdded(int amount)
    {
        DamageOnScale+= amount;
        anim.SetInteger("Damage", DamageOnScale);
        Instantiate(thingToSpawn, ImpactSpawnPoint1.position, transform.rotation);
        StartCoroutine(ActivateScaleAnimation());
        scoreText.text = DamageOnScale.ToString();
        CheckColor();
    }

    public void EnemyDamageAdded(int amount)
    {
        DamageOnScale -= amount;
        anim.SetInteger("Damage", DamageOnScale);
        Instantiate(thingToSpawn2, ImpactSpawnPoint2.position, transform.rotation);
        StartCoroutine(ActivateScaleAnimation());
        scoreText.text = DamageOnScale.ToString();
        CheckColor();
    }

    public void SetDifference(int difference)
    {
        DamageOnScale = difference;
        scoreText.text = DamageOnScale.ToString();
    }

    public void SetState(int playerAmount, int enemyAmount)
    {
        //throw new System.NotImplementedException();
    }

    public void SetWinAmount(int amount)
    {
        //throw new System.NotImplementedException();
    }

    public void SetLoseAmount(int amount)
    {
        //throw new System.NotImplementedException();
    }

    void CheckColor()
    {
        if(DamageOnScale > 0)
        {
            scoreText.color = new Color(25f / 255, 222f / 255, 86f / 255);
        }
        else if(DamageOnScale < 0)
        {
            scoreText.color = new Color(149f / 255, 48f / 255, 48f / 255);
        }
        else
        {
            scoreText.color = Color.black;
        }
    }
}
