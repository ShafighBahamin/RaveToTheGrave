using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour {

    public bool touch_ground;

    public GameObject player;

    public GameObject bullet;

    public Vector3 vec;

    public int Bullet_Count;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Bullet_Count = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Destroy(GetComponent<MeshCollider>());
        if (touch_ground)
        {
            vec.x = gameObject.transform.position.x;
            vec.z = gameObject.transform.position.z;
            vec.y = 1.28f;
            gameObject.transform.position = vec;
        }
        //move the enemy towards player
        if (gameObject.transform.position.z > player.transform.position.z + 3)
        {
            vec.x = gameObject.transform.position.x;
            vec.z = (float)(gameObject.transform.position.z - .05);
            vec.y = gameObject.transform.position.y;
            gameObject.transform.position = vec;
            //obj.transform.position = Vector3.Lerp(obj.transform.position, gameObject.transform.position, Time.deltaTime * 2);
        }

        /*
        //move enemy away from player
        if (gameObject.transform.position.z < player.transform.position.z + 3)
        {
            vec.x = gameObject.transform.position.x;
            vec.z = (float)(gameObject.transform.position.z + .05);
            vec.y = gameObject.transform.position.y;
            gameObject.transform.position = vec;
            //obj.transform.position = Vector3.Lerp(obj.transform.position, gameObject.transform.position, Time.deltaTime * 2);
        }
        */



        //shoot if within range
        if (gameObject.transform.position.z < player.transform.position.z + 4)
        {
            if (Bullet_Count > 0)
            {
                shoot();
                Bullet_Count--;
            }
        }
    }

    private void shoot()
    {
        bullet = GameObject.CreatePrimitive(PrimitiveType.Plane);
        // put the animation for the bullet
        /*
        sprite = Instantiate(GameObject.FindGameObjectWithTag("Sprite"));
        sprite.transform.parent = obj.transform;
        */
        vec.x = .1f;
        vec.y = .1f;
        vec.z = .1f;
        bullet.transform.localScale = vec;
        bullet.AddComponent<BoxCollider>();
        bullet.AddComponent <Bullet_AI>();
        bullet.tag = "Bullet";
        vec.x = gameObject.transform.position.x;
        vec.y = 1.28f;
        vec.z = gameObject.transform.position.z;
        bullet.transform.position = vec;
        vec.x = 90f;
        vec.y = 90f;
        vec.z = 0;

        bullet.transform.rotation = Quaternion.Euler(vec);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            touch_ground = true;
        }
    }
}
