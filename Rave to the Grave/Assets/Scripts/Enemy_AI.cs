﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour {

    private bool touch_ground;
    private bool touch_fence;
    private bool touch_c_fence;
    private bool isIdle;
    private bool isWalk;
    public GameObject player;
    public GameObject bullet;
    private Bullet_AI b_AI;
    private BoxCollider BC;
    private Animator anime;
    public Vector3 vec;
    public int Bullet_Count;
    private bool facing_left;
    private float distance;
    private float speed;
    private bool fire;
	// Use this for initialization
	void Start ()
    {
        fire = true;
        touch_fence = false;
        touch_c_fence = false;
        isWalk = false;
        isIdle = false;
        speed = .1f;
        distance = 3;
        touch_ground = false;
        facing_left = true;
        anime = gameObject.GetComponent<Animator>();
        touch_ground = true;
        player = GameObject.FindGameObjectWithTag("Sprite");
        Bullet_Count = 6;
    }

    // Update is called once per frame
    void Update()
    {

        Destroy(GetComponent<MeshCollider>());
        if (touch_ground)
        {
            vec.x = gameObject.transform.position.x;
            vec.z = gameObject.transform.position.z;
            vec.y = gameObject.transform.position.y;
            gameObject.transform.position = vec;
        }
        //move the enemy towards player
        if (touch_c_fence)
        {
            vec = gameObject.transform.position;
            vec.x = gameObject.transform.position.x + speed;
            gameObject.transform.position = vec;
        }
        else if (touch_fence)
        {
            vec = gameObject.transform.position;
            vec.x = gameObject.transform.position.x - speed;
            gameObject.transform.position = vec;
        }
        else if (gameObject.transform.position.z > player.transform.position.z + distance)
        {
            if (!facing_left)
            {
                vec = gameObject.transform.localScale;
                vec.x = gameObject.transform.localScale.x * -1;
                gameObject.transform.localScale = vec;
                facing_left = true;
            }
            if (gameObject.transform.position.z > player.transform.position.z + 3 && gameObject.transform.position.z < player.transform.position.z + 5 && gameObject.transform.position.x > player.transform.position.x)
            {
                vec = gameObject.transform.position;
                vec.x = gameObject.transform.position.x - speed;
                gameObject.transform.position = vec;
            }
            vec.x = gameObject.transform.position.x;
            vec.z = (float)(gameObject.transform.position.z - speed);
            vec.y = gameObject.transform.position.y;
            anime.SetBool("Walk", true);
            anime.SetBool("Fire", false);


            gameObject.transform.position = vec;
            //obj.transform.position = Vector3.Lerp(obj.transform.position, gameObject.transform.position, Time.deltaTime * 2);
        }
        else if (gameObject.transform.position.z < player.transform.position.z - distance)
        {
            if (facing_left)
            {
                vec = gameObject.transform.localScale;
                vec.x = gameObject.transform.localScale.x * -1;
                gameObject.transform.localScale = vec;
                facing_left = false;
            }
            if (gameObject.transform.position.z < player.transform.position.z - 3 && gameObject.transform.position.z > player.transform.position.z - 5 && gameObject.transform.position.x < player.transform.position.x)
            {
                vec = gameObject.transform.position;
                vec.x = gameObject.transform.position.x + speed;
                gameObject.transform.position = vec;
            }
            vec.x = gameObject.transform.position.x;
            vec.z = (float)(gameObject.transform.position.z + speed);
            vec.y = gameObject.transform.position.y;
            anime.SetBool("Walk", true);
            anime.SetBool("Fire", false);
            gameObject.transform.position = vec;
            //obj.transform.position = Vector3.Lerp(obj.transform.position, gameObject.transform.position, Time.deltaTime * 2);
        }
        else if ((gameObject.transform.position.z < player.transform.position.z + 4 || gameObject.transform.position.z > player.transform.position.z - 4) && Bullet_Count > 0 && fire)
        {
            InvokeRepeating("shoot", 0, 2);
            fire = false;
        }
        else
        {
            anime.SetBool("Walk", false);
            anime.SetBool("Fire", false);
        }
    }

   

    private void shoot()
    {
        // put the animation for the bullet
        /*
        sprite = Instantiate(GameObject.FindGameObjectWithTag("Sprite"));
        sprite.transform.parent = obj.transform;
        */
        if (Bullet_Count > 0)
        {
            anime.SetBool("Walk", false);
            anime.SetBool("Fire", true);
            bullet = Instantiate(GameObject.FindGameObjectWithTag("Bullet"));
            if (facing_left)
            {
                vec.x = .3f;
                vec.y = .3f;
                vec.z = .3f;
            }
            else
            {
                vec.x = -.3f;
                vec.y = .3f;
                vec.z = .3f;
            }
            bullet.transform.localScale = vec;
            vec.x = 10f;
            vec.y = 90f;
            vec.z = 0;
            bullet.transform.parent = null;
            bullet.transform.rotation = Quaternion.Euler(vec);
            BC = bullet.AddComponent<BoxCollider>();
            b_AI = bullet.AddComponent<Bullet_AI>();
            if (gameObject.transform.localScale.x < 0)
            {
                b_AI.facing_left = true;
            }
            else
            {
                b_AI.facing_left = false;
            }
            b_AI.shooter_pos = gameObject.transform.position;
            bullet.tag = "_Bullet";

            vec.x = gameObject.transform.position.x;
            vec.y = gameObject.transform.position.y;
            vec.z = gameObject.transform.position.z;
            bullet.transform.position = vec;
            Bullet_Count--;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "fence")
        {
            touch_fence = true;
        }
        if (other.gameObject.tag == "c_fence")
        {
            touch_c_fence = true;
        }
        if (other.gameObject.tag == "Ground")
        {
            touch_ground = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "fence")
        {
            touch_fence = false;
        }
        if(other.gameObject.tag == "c_fence")
        {
            touch_c_fence = false;
        }
    }
}
