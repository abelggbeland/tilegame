using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hazerds
{
    public class spikes : MonoBehaviour
    {
        Animator animator;

        public void Update()
        {
            animator = GetComponent<Animator>();
            spikehit();
        }

        public void spikehit()
        {
            Vector2 origan = new Vector2(transform.position.x, transform.position.y); 

            Debug.DrawRay(origan + new Vector2(-1.2f, 0.2f), Vector2.left * 0.75f, Color.red);
            Debug.DrawRay(origan + new Vector2(1.2f, 0.2f), Vector2.right * 0.75f, Color.red);

            RaycastHit2D hitleft = Physics2D.Raycast(origan + new Vector2(-1.5f, 0.2f), Vector2.left, 0.75f);
            RaycastHit2D hitright = Physics2D.Raycast(origan + new Vector2(1.5f, 0.2f), Vector2.right, 0.75f);

            if(hitleft.collider.name == "player")
            {
                animator.SetTrigger("spike");
            }

            if(hitright.collider.name == "player")
            {
                animator.SetTrigger("spike");
            }
        }
    }
}

