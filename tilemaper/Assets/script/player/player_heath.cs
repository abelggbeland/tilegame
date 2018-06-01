using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace player
{
    public class player_heath : MonoBehaviour
    {
        Collider2D collider2D;

        public bool loseheathtrue = false;
        private bool hurtcooldown = false;

        public void Start()
        {
            collider2D = GetComponent<Collider2D>();
        }

        public void Update()
        {

            if (collider2D.IsTouchingLayers(LayerMask.GetMask("loseheath")) && hurtcooldown == false)
            {
                Debug.Log("here");
                loseheathtrue = true;
                Invoke("hurtcooldownmet", 3f);
                hurtcooldown = true;
            }
        }

        public void loseheath()
        {
            Debug.Log("here");
            loseheathtrue = true;
        }

        public void hurtcooldownmet()
        {
            hurtcooldown = false;
        }

    }
}


