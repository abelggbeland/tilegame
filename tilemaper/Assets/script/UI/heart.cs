using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using player;

namespace heath
{ 
    public class heart : MonoBehaviour
    {
        public List<Image> hearts = new List<Image>();
        public Sprite fullheart;
        public Sprite notheart;

        public player_heath playerheath;
        
        

        public void Update()
        {    
            heartcon();
        }

        public void Start()
        {
            
        }

        public void heartcon()
        {

            Image heart1 = hearts[0];
            Image heart2 = hearts[1];
            Image heart3 = hearts[2];

            if (playerheath.loseheathtrue == true)
            {
                if(heart2.sprite == notheart && heart3.sprite == fullheart)
                {
                    heart3.sprite = notheart;
                }

                if (heart1.sprite == notheart && heart2.sprite == fullheart)
                {
                    heart2.sprite = notheart;
                }

                if (heart1.sprite == fullheart)
                {
                    heart1.sprite = notheart;
                }

                playerheath.loseheathtrue = false;
              
            }

            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(heart1.sprite == notheart && heart2.sprite == fullheart)
                {
                    heart1.sprite = fullheart;
                }

                if(heart2.sprite == notheart && heart3.sprite == fullheart)
                {
                    heart2.sprite = fullheart;
                }

                if(heart3.sprite == notheart && heart2.sprite == notheart)
                {
                    heart3.sprite = fullheart;
                }
            }

        }

        public void dead()
        {

        }

    }
}

