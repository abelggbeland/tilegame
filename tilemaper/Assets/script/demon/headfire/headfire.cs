using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headfire : MonoBehaviour {

    public GameObject playertofollow;
    public GameObject demontofollow;
    Animator animator;
    Collider2D collider2D;
    Rigidbody2D rigidbody2D;
    public float radiusexplode = 2;
    public float spawnshakex;
    public float spawnshakey;

    public bool stoploop;
    public bool startfire;

    public void Start()
    {

        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        fire();
    }

    public void fire()
    {
        if(startfire == true && isActiveAndEnabled)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.None;
            Debug.Log("fire");
            Vector2 distoplayer = playertofollow.transform.position - transform.position;
            
            rigidbody2D.velocity = distoplayer * 4;

            float distoexplode = Vector2.Distance(transform.position, playertofollow.transform.position);

            if(distoexplode < radiusexplode)
            {
                startfire = false;
                animator.SetTrigger("explode");
                Invoke("enable", 4);
                
            }
        }
        else
        {
            rigidbody2D.velocity = new Vector2(0, 4);
            Invoke("wait", 2);
            
        }
        
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void enable()
    {
        Vector2 playertranform = new Vector2(transform.position.x, transform.position.y + 0.3f);
        Vector2 demtranform = new Vector2(demontofollow.transform.position.x - spawnshakex, demontofollow.transform.position.y + spawnshakey);
        transform.position = demtranform;
            

        gameObject.SetActive(true);

    }

    public void wait()
    {
        if(startfire == false)
        {
            Vector2 playertranform = new Vector2(transform.position.x, transform.position.y + 0.3f);
            Vector2 demtranform = new Vector2(demontofollow.transform.position.x - 0.9f, demontofollow.transform.position.y + 2);
            Vector2 distodemon = demtranform - playertranform;
            rigidbody2D.velocity = distodemon;
        }
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(playertofollow.transform.position, radiusexplode);
    }
}
