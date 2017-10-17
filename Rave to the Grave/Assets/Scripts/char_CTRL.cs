using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//coroutine!!! (for smoother loops with animations)

public class char_CTRL : MonoBehaviour {


    public int speed;
    public int health;
    public double dist;
    public Vector3 vec;
    public Animator anime;


    public bool touch_ground;
    public bool back_g_hit;
    public bool got_hit;


	// Use this for initialization
	void Start () {
        dist = .1;
        health = 5;
        back_g_hit = false;
        
	}
	
	// Update is called once per frame
	void Update () {
        
        if(touch_ground == true)
        { 
            vec.x = gameObject.transform.position.x;
            vec.z = gameObject.transform.position.z;
            vec.y = 1.28f;
            gameObject.transform.position = vec;
        }

        if (Input.GetKey("up") && gameObject.transform.position.x > 2.3)
        {
            anime.SetBool("Walk", true);
            vec.x = (float)(gameObject.transform.position.x - .1);
            vec.z = gameObject.transform.position.z;
            vec.y = 1.28f;
            gameObject.transform.position = vec;
        }

        if (Input.GetKey("right"))
        {
            anime.SetBool("Walk", true);
            vec.x = gameObject.transform.position.x ;
            vec.z = (float)(gameObject.transform.position.z+.05);
            vec.y = 1.28f;
            gameObject.transform.position = vec;
        }

        if (Input.GetKey("down") && gameObject.transform.position.x<5.6)
        {
            anime.SetBool("Walk", true);
            vec.x = (float)(gameObject.transform.position.x + .1);
            vec.z = gameObject.transform.position.z;
            vec.y = 1.28f;
            gameObject.transform.position = vec;
        }

        if (Input.GetKey("left"))
        {
            anime.SetBool("Walk", true);
            vec.x = gameObject.transform.position.x;
            vec.z = (float)(gameObject.transform.position.z-.1);
            vec.y = 1.28f;
            gameObject.transform.position = vec;
        }

        if (got_hit)
        {
            anime.SetBool("Walk", false);
        }

        if (health == 0)
        {
            /*
             death animation goes here
             */
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            health--;
            Destroy(other.gameObject);
            got_hit = true;
        }
        if(other.gameObject.tag == "Ground")
        {
            touch_ground = true;
        }
        if(other.gameObject.tag == "backGround")
        {
            back_g_hit = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "backGround")
        {
            back_g_hit = false;
        }
    }
}
