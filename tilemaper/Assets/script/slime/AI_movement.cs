using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace slime
{
    public class AI_movement : MonoBehaviour
    {
        Rigidbody2D rigidbody2D;
        Collider2D collider2D;
        SpriteRenderer spritrenderer;

        public float cooldowntime = 5f;

        bool movingright = true;
        bool jumpcooldown = true;

        public void Start()
        {
            spritrenderer = GetComponent<SpriteRenderer>();
            rigidbody2D = GetComponent<Rigidbody2D>();
            collider2D = GetComponent<Collider2D>();
        }

        public void FixedUpdate()
        {
            movement();
        }




        public void movement()
        {

            float raycastdis = 3f;

            Vector2 slimepos = new Vector2(transform.position.x, transform.position.y);
            Vector2 endpointleft = slimepos + Vector2.left * 2.5f;
            Vector2 endpointright = slimepos + Vector2.right * 2.5f;

            Debug.DrawRay(slimepos + new Vector2(-0.5f, 0), Vector2.left * 1f, Color.green);
            Debug.DrawRay(endpointleft + new Vector2(0, -1.7f), Vector2.down * 0.5f, Color.green);

            Debug.DrawRay(slimepos + new Vector2(0.5f, 0), Vector2.right * 1f, Color.blue);
            Debug.DrawRay(endpointright + new Vector2(0, -1.7f), Vector2.down * 0.5f, Color.blue);

            RaycastHit2D hitleft = Physics2D.Raycast(endpointleft + new Vector2(0, -1.7f), Vector2.up, 0.5f);
            RaycastHit2D hitright = Physics2D.Raycast(endpointright + new Vector2(0, -1.7f), Vector2.up, 0.5f);

            RaycastHit2D hitfront = Physics2D.Raycast(slimepos + new Vector2(0.5f, 0), Vector2.right, 1f);
            RaycastHit2D hitback = Physics2D.Raycast(slimepos + new Vector2(-0.5f, 0), Vector2.left, 1f);

            if (movingright)
            {
                if (collider2D.IsTouchingLayers(LayerMask.GetMask("ground")) && jumpcooldown == true)
                {

                    if(hitfront.collider.name == "foreground")
                    {
                        movingright = false;
                    }

                    spritrenderer.flipX = false;
                    if (hitright.collider.name == "foreground")
                    {               
                        rigidbody2D.velocity = new Vector2(5, 20);
                        Invoke("cooldown", cooldowntime);
                        jumpcooldown = false;
                        movingright = true;

                        if (hitfront.collider.name == "foreground")
                        {
                            rigidbody2D.velocity = new Vector2(-5, 20);
                        }
                    }
                    else
                    {
                        movingright = false;
                    }
                }
            }
            
            if (!movingright)
            {
           
                if (collider2D.IsTouchingLayers(LayerMask.GetMask("ground")) && jumpcooldown == true)
                {
                    

                    spritrenderer.flipX = true;
                    if (hitleft.collider.name == "foreground")
                    {
                        
                        rigidbody2D.velocity = new Vector2(-5, 20);   
                        Invoke("cooldown", cooldowntime);
                        jumpcooldown = false;
                        movingright = false;

                        if (hitback.collider.name == "foreground")
                        {
                            rigidbody2D.velocity = new Vector2(5, 20);
                        }

                    }
                    else
                    {
                        movingright = true;
                    }
                }
                

            }
        }

        public void cooldown()
        {
            jumpcooldown = true;
            rigidbody2D.constraints = RigidbodyConstraints2D.None;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            

        }


    }
}


