using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using player;

public class demon : MonoBehaviour {

    public GameObject playertofollow;
    public GameObject head;
    public Transform parentforhead;
    Animator animator;
    public headfire headfire;
    public player_heath playerheath;

    private bool firecooldown = true;

    public float attackradius = 5f;
    public float radiustofirehead = 10f;
    public float distoexplode = 3f;
    


    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        
        firehead();
        Controller();
    }

    public void Controller()
    {


        float distobreathfire = Vector2.Distance(transform.position, playertofollow.transform.position);
        if (distobreathfire < attackradius)
        {
            Debug.Log("here");
            animator.SetTrigger("fire");      
            
        }
    }

    public void dodamage()
    {
        float distobreathfire = Vector2.Distance(transform.position, playertofollow.transform.position);
        if (distobreathfire < attackradius)
        {
            playerheath.loseheath();
        }
        
    }


    public void firehead()
    { 
        float distofirehead = Vector2.Distance(transform.position, playertofollow.transform.position);

        if(distofirehead < radiustofirehead && headfire.isActiveAndEnabled)
        {
            Debug.Log("here");
            headfire.startfire = true;
        }
        else
        {
            headfire.startfire = false;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackradius);
        Gizmos.DrawWireSphere(transform.position, radiustofirehead);
        Gizmos.DrawWireSphere(playertofollow.transform.position, distoexplode);
    }

}
