using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket_enemy_ai : MonoBehaviour {

    public GameObject rocket;
    private GameObject player;
    private Rocket_AI b_AI;
    private BoxCollider BC;
    private Animator anime_rocket;
    public Vector3 vec;

    private bool facing_left;
    private bool rockets;
    private bool x_south;
    private bool touch_ground;
    private bool touch_c_fence;
    private bool touch_fence;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Sprite");
        x_south = true;
        rockets = true;
        facing_left = true;
        touch_ground = true;
        touch_c_fence = false;
        touch_fence = false;
    }

    // Update is called once per frame
    void Update ()
    {
        if (facing_left && player.transform.position.z > gameObject.transform.position.z)
        {
            vec = gameObject.transform.localScale;
            vec.x = gameObject.transform.localScale.x * -1;
            gameObject.transform.localScale = vec;
            facing_left = false;
        }
        else if(!facing_left && player.transform.position.z < gameObject.transform.position.z)
        {
            vec = gameObject.transform.localScale;
            vec.x = gameObject.transform.localScale.x * -1;
            gameObject.transform.localScale = vec;
            facing_left = true;
        }
        if (touch_ground)
        {
            vec.x = gameObject.transform.position.x;
            vec.z = gameObject.transform.position.z;
            vec.y = gameObject.transform.position.y;
            gameObject.transform.position = vec;
        }
        anime_rocket = gameObject.GetComponent<Animator>();
        if (rockets)
        {
            InvokeRepeating("shoot_rocket", 0, 5);
            rockets = false;
        }
        //if (touch_fence)
        //{
        //    vec = gameObject.transform.position;
        //    vec.x -= .05f;
        //    gameObject.transform.position = vec;
        //    anime_rocket.SetBool("Fire", false);
        //}
        //else if (touch_c_fence)
        //{
        //    vec = gameObject.transform.position;
        //    vec.x += .05f;
        //    gameObject.transform.position = vec;
        //    anime_rocket.SetBool("Fire", false);
        //}
        if (!x_south)
        {
            vec = gameObject.transform.position;
            vec.x -= .05f;
            gameObject.transform.position = vec;
            if (vec.x < 3)
            {
                x_south = true;
            }
            anime_rocket.SetBool("Fire", false);
        }
        else if (x_south)
        {
            vec = gameObject.transform.position;
            vec.x += .05f;
            gameObject.transform.position = vec;
            if (vec.x > 14)
            {
                x_south = false;
            }
            anime_rocket.SetBool("Fire", false);
        }

        }

    private IEnumerator multi_anime()
    {
        yield return new WaitForSecondsRealtime(.5f);
    }
    private void shoot_rocket()
    {
        anime_rocket.SetBool("Fire", true);

        multi_anime();
        rocket = Instantiate(GameObject.FindGameObjectWithTag("Rocket"));
      
       
        if (facing_left)
        {
            vec.x = -.5f;
            vec.y = .5f;
            vec.z = .5f;
            rocket.transform.localScale = vec;
        }
        else
        {
            vec.x = .5f;
            vec.y = .5f;
            vec.z = .5f;
            rocket.transform.localScale = vec;
        }
        vec.x = 10f;
        vec.y = 90f;
        vec.z = 0;
        rocket.transform.rotation = Quaternion.Euler(vec);
        BC = rocket.AddComponent<BoxCollider>();
        b_AI = rocket.AddComponent<Rocket_AI>();
        b_AI.shooter_pos = gameObject.transform.position;
        if (gameObject.transform.localScale.x < 0)
        {
            b_AI.facing_left = false;
        }
        else
        {
            b_AI.facing_left = true;
        }
        rocket.tag = "_Rocket";
        GameObject.FindGameObjectWithTag("_Rocket").GetComponent<Animator>().Play("Rocket");
        vec.x = gameObject.transform.position.x;
        vec.y = gameObject.transform.position.y;
        vec.z = gameObject.transform.position.z;
        rocket.transform.position = vec;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            touch_ground = true;
        }
        if (other.gameObject.tag == "fence")
        {
            touch_fence = true;
        }
        if (other.gameObject.tag == "c_fence")
        {
            touch_c_fence = true;
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
