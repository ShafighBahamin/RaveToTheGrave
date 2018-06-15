using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AI : MonoBehaviour {
    public GameObject bullet;
    private GameObject player;
    private BOss_bullet_AI b_AI;
    private BoxCollider BC;
    private Animator anime;
    private char_CTRL charcontroller;
    private Spawn_Enemy se;
    public Vector3 vec;

    private bool has_bullet;
    private float distance;
    private float speed;
    private bool touch_ground;
    private bool touch_fence;
    private bool touch_c_fence;
    private bool facing_left;
    private bool shooting;
    private bool touch_sprite;

    public int health;
    // Use this for initialization
    void Start()
    {
        se = GameObject.FindGameObjectWithTag("Sprite").GetComponent<Spawn_Enemy>();
        charcontroller = GameObject.FindGameObjectWithTag("Sprite").GetComponent<char_CTRL>();
        health = 5;
        touch_fence = false;
        touch_c_fence = false;
        speed = .2f;
        distance = 4;
        anime = this.GetComponent<Animator>();
        has_bullet = true;
        touch_ground = false;
        facing_left = true;
        player = GameObject.FindGameObjectWithTag("Sprite");
        InvokeRepeating("shoot", 2, 5);
        shooting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 1)
        {
            print("im gonna go to hell when i die");
            se.boss_death_count++;
            Destroy(this.gameObject);
        }
        if(charcontroller.boss_punched == true)
        {
            print("this is the health " + health.ToString());
            health--;
        }
        if(health < 3 && shooting)
        {
            CancelInvoke("shoot");
            shooting = false;
            distance = 3;
        }

        if (touch_ground)
        {
            vec.x = gameObject.transform.position.x;
            vec.z = gameObject.transform.position.z;
            vec.y = gameObject.transform.position.y;
            gameObject.transform.position = vec;
        }
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
            else if (gameObject.transform.position.z > player.transform.position.z + 3 && gameObject.transform.position.z < player.transform.position.z + 5 && gameObject.transform.position.x < player.transform.position.x)
            {
                vec = gameObject.transform.position;
                vec.x = gameObject.transform.position.x + speed;
                gameObject.transform.position = vec;
            }
            vec.x = gameObject.transform.position.x;
            vec.z = (float)(gameObject.transform.position.z - speed);
            vec.y = gameObject.transform.position.y;

            anime.SetBool("run", true);
            anime.SetBool("fire", false);
            anime.SetBool("melee", false);


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
            else if (gameObject.transform.position.z < player.transform.position.z - 3 && gameObject.transform.position.z > player.transform.position.z - 5 && gameObject.transform.position.x > player.transform.position.x)
            {
                vec = gameObject.transform.position;
                vec.x = gameObject.transform.position.x - speed;
                gameObject.transform.position = vec;
            }
            vec.x = gameObject.transform.position.x;
            vec.z = (float)(gameObject.transform.position.z + speed);
            vec.y = gameObject.transform.position.y;

            anime.SetBool("run", true);
            anime.SetBool("fire", false);
            anime.SetBool("melee", false);
            gameObject.transform.position = vec;
            //obj.transform.position = Vector3.Lerp(obj.transform.position, gameObject.transform.position, Time.deltaTime * 2);
        }
       else if (Vector3.Distance(gameObject.transform.position, player.transform.position) <= 2.85 && !shooting)
        {
            print("gonna punch her in the face");
            anime.SetBool("run", false);
            anime.SetBool("fire", false);
            anime.SetBool("melee", true);
        }
        else
        {
            anime.SetBool("run", false);
            anime.SetBool("fire", false);
            anime.SetBool("melee", false);
        }

    }
    private void shoot()
    {
        // put the animation for the bullet
        /*
        sprite = Instantiate(GameObject.FindGameObjectWithTag("Sprite"));
        sprite.transform.parent = obj.transform;
        */
        if (has_bullet)
        {
            anime.SetBool("run", false);
            anime.SetBool("fire", true);
            anime.SetBool("melee", false);
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
            b_AI = bullet.AddComponent<BOss_bullet_AI>();
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
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "fence")
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
        if (other.gameObject.tag == "fence")
        {
            touch_fence = false;
        }
        if (other.gameObject.tag == "c_fence")
        {
            touch_c_fence = false;
        }
    }
}
