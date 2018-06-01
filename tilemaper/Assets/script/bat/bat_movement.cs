using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bat
{
    public class bat_movement : MonoBehaviour
    {

        Collider2D collider2D;
        Rigidbody2D rigidbody2D;

        public Collider2D ground;
        public GameObject playertofolow;
        public Animator animator;
        public float seeradius = 10f;
        public float idleradius = 3f;
        public float diveradius = 2f;
        public int raycastamunt = 5;
        private int raydownnumber;
        public float rayspacing = 1.5f;
        public float flyspeed = 5f;
        public float cavespeed = 15f;
        public float idlespeed = 16;
        public float divespeed = 6;
        public float disdiverad = 7f;
        private bool canflyup = true;
        private bool incave = false;
        private bool isinidle = false;
        private bool canfly;
        private bool cantmove;
        private bool canupupnav;
        private bool candive;
        private bool isdiving;

        public void FixedUpdate()
        {
            
        }

        public void Update()
        {

            movement();   
        }

        public void Start()
        {
            collider2D = GetComponent<Collider2D>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

        }

        public void movement()
        {
            Vector2 playerloc = new Vector2(playertofolow.transform.position.x, playertofolow.transform.position.y);
            float distoplayer = Vector2.Distance(transform.position, playertofolow.transform.position);
            float distoidle = Vector2.Distance(transform.position, playertofolow.transform.position);
            float distodive = Vector2.Distance(transform.position, playerloc + new Vector2(0, disdiverad));
            Vector2 origan = new Vector2(transform.position.x, transform.position.y - rayspacing);
            Vector2 playerorigan = new Vector2(playertofolow.transform.position.x, playertofolow.transform.position.y);
            Vector2 flyvetor = new Vector2(0, flyspeed);
            Vector2 divevetor = new Vector2(0, divespeed);
            Vector2 cavevetor = new Vector2(0, cavespeed);

            if(distoplayer < seeradius)
            {
                Vector2 rayroofogigan = new Vector2(transform.position.x - 0.5f, transform.position.y + 1);

                for (int rayroof = 0; rayroof < 3; rayroof++)
                {
                    Debug.Log(canupupnav);
                    RaycastHit2D rayup = Physics2D.Raycast(rayroofogigan, Vector2.left, 0.5f);
                    Debug.DrawRay(rayroofogigan, Vector2.left * 0.5f, Color.red);
                    Debug.Log(rayup.collider.name);
                    if (rayup.collider.name == "foreground")
                    {
                        canupupnav = false;
                    }
                    else
                    {
                        canupupnav = true;
                    }

                    rayroofogigan += new Vector2(1, 0);
                }
                

        
            }


            //down
            for (int raydown = 0; raydown < raycastamunt; raydown++)
            {
                RaycastHit2D raydownname = Physics2D.Raycast(origan, Vector2.down, 1);
                Debug.DrawRay(origan, Vector2.down * 1);
                origan += new Vector2(0, -rayspacing);

                if (canfly == true)
                {
                    if (raydownname.collider.name == "foreground")
                    {

                        float distoland = Vector2.Distance(transform.position, raydownname.point);
                        if (collider2D.IsTouchingLayers(LayerMask.GetMask("ground")) && distoland <= 7.5f)
                        {
                            incave = true;
                        }

                        if (canflyup == false)
                        {

                            if (distoland == 1.5f)
                            {
                                if (incave == true)
                                {
                                    rigidbody2D.velocity = cavevetor;
                                }
                            }

                            if (distoland <= 1.5f)
                            {
                                rigidbody2D.velocity = flyvetor;
                            }
                        }
                        if (canflyup == true)
                        {
                            if (distoland <= 4.5f)
                            {

                                rigidbody2D.velocity = flyvetor;
                            }
                        }

                    }
                }
            }


            origan = new Vector2(transform.position.x, transform.position.y + rayspacing);

            //up
            for (int rayup = 0; rayup < raycastamunt; rayup++)
            {

                if(canfly == true)
                {
                    RaycastHit2D rayupname = Physics2D.Raycast(origan, Vector2.up, 1);
                    Debug.DrawRay(origan, Vector2.up * 1);
                    origan += new Vector2(0, rayspacing);



                    if (distoidle > idleradius)
                    {
                        isinidle = true;
                        if (isinidle == true)
                        {
                            Invoke("idlein", 3);
                            canfly = true;

                        }
                    }
                    else
                    {
                        isinidle = false;
                        canfly = true;
                        animator.SetBool("idle", false);
                        rigidbody2D.constraints = RigidbodyConstraints2D.None;
                        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                    }

                    if (rayupname.collider.name == "foreground")
                    {
                        float distoland = Vector2.Distance(transform.position, rayupname.point);

                        if (distoland <= 3f)
                        {

                            canflyup = false;
                        }

                    }

                    if (rayupname.collider.name == "background")
                    {

                        float distoland = Vector2.Distance(transform.position, rayupname.point);

                        if (distoland <= 4f)
                        {

                            canflyup = true;

                        }

                    }
                }
                
               
            }

            origan = new Vector2(transform.position.x - rayspacing, transform.position.y);
            

            if (distoplayer < seeradius)
            {
                canfly = false;

                

                if (collider2D.IsTouchingLayers(LayerMask.GetMask("ground")))
                {
                    if(candive == true)
                    {
                        candive = true;
                    }

                }
                

                Vector2 neworigan = new Vector2(transform.position.x, transform.position.y);
                if (Physics2D.Linecast(neworigan, playertofolow.transform.position, LayerMask.GetMask("ground")))
                {          
                    Debug.DrawLine(neworigan, playertofolow.transform.position);
                    cantmove = true;

                }
                else
                {
                    cantmove = false;
                }

                
                if (distodive > diveradius)
                {
                    

  
                    if (canupupnav == true)
                    {
                        rigidbody2D.velocity = flyvetor;

                        
                    }
                    if (canupupnav == false)
                    {
                        
                        Debug.Log("here");
                        Vector2 dirplayer = playertofolow.transform.position - transform.position;
                        rigidbody2D.velocity = dirplayer * 3;

                    }

                    if(canupupnav == false && cantmove == true)
                    {
                        Debug.Log("here");
                        isinidle = true;
                        idlein();

                    }

                    if(canupupnav == true)
                    {
                        isinidle = false;
                        rigidbody2D.constraints = RigidbodyConstraints2D.None;
                        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                    }

                    if (collider2D.IsTouchingLayers(LayerMask.GetMask("ground")) && cantmove == false)
                    {
                        Vector2 dirplayer = playertofolow.transform.position - transform.position;
                        rigidbody2D.velocity = dirplayer * 3;
                    }



                }

                if (distodive < diveradius)
                {
                    candive = true;
                }



                if (candive == true)
                {
                    Vector2 dirplayer = playertofolow.transform.position - transform.position;
                    rigidbody2D.velocity = dirplayer * 3;
                    Invoke("dive", 6);

                }

            }
            else
            {
                canfly = true;
            }

            


            origan = new Vector2(transform.position.x + rayspacing, transform.position.y);

            

        }

        public void idlein()
        {
            if(isinidle == true)
            {
                if (!collider2D.IsTouchingLayers(LayerMask.GetMask("ground")))
                {

                    rigidbody2D.velocity = new Vector2(0, idlespeed);
                }
                else
                {
                    rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
                    animator.SetBool("idle", true);
                    canfly = false;
                }
            }
         

            
        }

        public void idleout()
        {
            animator.SetTrigger("idleout");
        }

        public void dive()
        {
            candive = false;
        }

        public void OnDrawGizmos()
        {
            Vector2 playerloc = new Vector2(playertofolow.transform.position.x, playertofolow.transform.position.y);
            Gizmos.color = new Color(255, 0, 0);
            Gizmos.DrawWireSphere(transform.position, seeradius);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, idleradius);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, playertofolow.transform.position);
            Gizmos.DrawWireSphere(playerloc + new Vector2(0, disdiverad), diveradius);
            
        }

    }
}

