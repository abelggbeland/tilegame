using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour {

    public Text text;
    public float maintimer = 10f;
    private float timercountdown;
    private bool cancount = true;
    public bool endgame = false;

    public void Start()
    {
        timercountdown = maintimer;
    }

    public void Update()
    {
        timercon();
    }

    public void timercon()
    {
        if (cancount == true && timercountdown >= 0.0f)
        {
            Debug.Log("here");
            timercountdown -= Time.deltaTime;
            text.text = timercountdown.ToString();
        }

        if (timercountdown <= 0.0f && endgame == false)
        {
            cancount = false;
            text.text = "o.oo";
            endgame = true;
        }
    }

}
