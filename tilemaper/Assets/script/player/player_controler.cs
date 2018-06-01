using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace player
{
    public class player_controler : MonoBehaviour
    {
        [SerializeField] float speed = 15f;
        [SerializeField] float airspeed = 5f;
        [SerializeField] float jumpspeed = 5;
        [SerializeField] float dubblejumpspeed = 10;
        [SerializeField] float climbspeed;
        [SerializeField] float climbdownspeed;

        bool canClimb = false;
        bool dubblejumpcooldown = false;
        

        SpriteRenderer spriterender;
        Rigidbody2D rigidbody2D;
        Animator animator;
        Collider2D collider2D;

        public void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            spriterender = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            collider2D = GetComponent<Collider2D>();
        }

        public void Update()
        {
            movement();
            flipsprite();
            aninatorcontrol();
            jump();
            climb();
        }

        public void movement()
        {
            Vector2 moveDir = Vector2.zero;

            if(collider2D.IsTouchingLayers(LayerMask.GetMask("ground")) || collider2D.IsTouchingLayers(LayerMask.GetMask("normal ladders")) || collider2D.IsTouchingLayers(LayerMask.GetMask("loseheath")))
            {
                moveDir.x = Input.GetAxis("Horizontal");

                rigidbody2D.velocity += moveDir * speed * Time.deltaTime;
            }
            else
            {
                return;
            }
        }
        
        public void jump()
        {
            Vector2 moveair = Vector2.zero;

            if (!collider2D.IsTouchingLayers(LayerMask.GetMask("ground")))
            {
                moveair.x = Input.GetAxis("Horizontal");
                rigidbody2D.velocity += moveair * airspeed * Time.deltaTime;
            }

            if (collider2D.IsTouchingLayers(LayerMask.GetMask("ground")) || collider2D.IsTouchingLayers(LayerMask.GetMask("loseheath")) && !collider2D.IsTouchingLayers(LayerMask.GetMask("jump tiles")))
            {

                if (Input.GetButtonDown("Jump"))
                {
                    Vector2 jumpforce = new Vector2(0, jumpspeed);
                    rigidbody2D.velocity += jumpforce;
                }
            }

            if (collider2D.IsTouchingLayers(LayerMask.GetMask("jump tiles")) && dubblejumpcooldown == false)
            {
                Debug.Log("here" + dubblejumpcooldown);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Vector2 dubblejumpforce = new Vector2(0, dubblejumpspeed);
                    rigidbody2D.velocity += dubblejumpforce;

                    Invoke("dubblejump", 0.5f);

                    dubblejumpcooldown = true;
                }
            }

        }

        public void dubblejump()
        {
            dubblejumpcooldown = false;
        }

        public void climb()
        {
            if(!collider2D.IsTouchingLayers(LayerMask.GetMask("normal ladders")) && !collider2D.IsTouchingLayers(LayerMask.GetMask("ground")))
            {
                animator.SetBool("climb_idle", false);
                animator.SetBool("climing", false);
                rigidbody2D.constraints = RigidbodyConstraints2D.None;
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                canClimb = false;

            }

            if(collider2D.IsTouchingLayers(LayerMask.GetMask("start ladders")) && Input.GetKey(KeyCode.W))
            {
                canClimb = true;
            }

            if(collider2D.IsTouchingLayers(LayerMask.GetMask("start ladders")) && Input.GetKey(KeyCode.S))
            {
                animator.SetBool("climing", false);
                canClimb = false;
            }

            if (canClimb)
            {
                if (collider2D.IsTouchingLayers(LayerMask.GetMask("normal ladders")) || collider2D.IsTouchingLayers(LayerMask.GetMask("start ladders")))
                {

                    if (!collider2D.IsTouchingLayers(LayerMask.GetMask("ground")) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && collider2D.IsTouchingLayers(LayerMask.GetMask("normal ladders")))
                    {
                        animator.SetBool("climb_idle", true);
                        rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionY;
                    }
                    else
                    {
                        rigidbody2D.constraints = RigidbodyConstraints2D.None;
                        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                    }

                    if (collider2D.IsTouchingLayers(LayerMask.GetMask("ground")))
                    {
                        animator.SetBool("climing", false);
                    }

                    Vector2 climbforce = new Vector2(0, climbspeed);
                    Vector2 climbdownforce = new Vector2(0, climbdownspeed);

                    if (Input.GetKey(KeyCode.W) == true)
                    {
                        rigidbody2D.velocity = climbforce;
                        animator.SetBool("climing", true);
                        animator.SetBool("climb_idle", false);
                    }

                    if (Input.GetKey(KeyCode.S) == true)
                    {
                        rigidbody2D.velocity = -climbdownforce;
                        animator.SetBool("climing", true);
                        animator.SetBool("climb_idle", false);
                    }
                }
                else
                {
                    animator.SetBool("climing", false);
                }
            }       
        }

        public void flipsprite()
        {

            if (Input.GetKeyDown(KeyCode.D) == true)
            {
                spriterender.flipX = false;
            }

            if (Input.GetKeyDown(KeyCode.A) == true)
            {
                spriterender.flipX = true;
            }
        }

        public void aninatorcontrol()
        {
            if ((Input.GetKey(KeyCode.D) == true) || (Input.GetKey(KeyCode.A) == true))
            {
                animator.SetBool("running", true);
            }
            else
            {
                StartCoroutine("stoprunning");            
            }
        }

        

        IEnumerator stoprunning()
        {
            yield return new WaitForSeconds(0.5f);
            animator.SetBool("running", false);
        }

        IEnumerator sideJump()
        {
            yield return new WaitForSeconds(0.5f);      
        }

    }
}


