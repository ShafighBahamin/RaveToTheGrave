using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//coroutine!!! (for smoother loops with animations)
[RequireComponent(typeof(AudioSource))]
public class char_CTRL : MonoBehaviour {

    private Spawn_Enemy Sp_en;
    public int speed;
    public int health;
    public double dist;
    private BoxCollider BC;
    public Vector3 vec;
    public Animator anime;
    public Animator anime_grunt;
    public Animator anime_rocket;
    private Animation rock_death;
    private GameObject brain;
    private AudioSource audio;
    private int brains;
    private Vector3 pos_vec;
    private bool dead;
    public bool boss_punched;
    public bool touch_ground;
    public bool back_g_hit;
    public bool got_hit;
    public bool rocket_hit;
    private bool attack;
    public bool touch_grunt;
    public bool touch_boss;
    public bool touch_rocket;
    private bool enemy_dead_first;
    private bool enemy_dead_second;
    private int drop_brains;
    private int heart_num;
    private string heart;
    private bool touch_fence;
    private bool touch_c_fence;

    // Use this for initialization
    void Start () {
        touch_fence = false;
        touch_c_fence = false;
        touch_boss = false;
        boss_punched = false;
        Sp_en = gameObject.GetComponent<Spawn_Enemy>();
        brains = 0;
        dead = false;
        audio = gameObject.GetComponent<AudioSource>();
        heart = "Heart";
        heart_num = 4;
        rocket_hit = false;
        enemy_dead_first = false;
        enemy_dead_second = false;
        touch_grunt = false;
        touch_rocket = false;
        dist = .1;
        health = 4000;
        back_g_hit = false;
        anime = gameObject.GetComponent<Animator>();
        attack = false;
        //anime.SetBool("Walk", false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        pos_vec = gameObject.transform.position;
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (brains > 0)
        {
            health++;

        }
        if (touch_ground == true)
        {

            vec.x = gameObject.transform.position.x;
            vec.z = gameObject.transform.position.z;
            vec.y = gameObject.transform.position.y;
            gameObject.transform.position = vec;
        }
        if (Input.GetKeyDown("space"))
        {
            attack = true;
            anime.SetBool("Walk", false);
            anime.SetBool("Attack", true);
            audio.Play();
        }
        else if (Input.GetKey("up") || Input.GetKey("right") || Input.GetKey("left") || Input.GetKey("down"))
        {
            attack = false;
            anime.Play("Zombie Walk", -1, .0f);
            anime.SetBool("Walk", true);
            anime.SetBool("Attack", false);
        }
        else
        {
            anime.SetBool("Walk", false);
            anime.SetBool("Attack", false);
            attack = false;
        }

        if (Input.GetKey("up") && touch_c_fence == false)
        {
            //anime.SetBool("Walk", true);
            vec.x = (float)(gameObject.transform.position.x - .1);
            vec.y = gameObject.transform.position.y;
            vec.z = gameObject.transform.position.z;
            gameObject.transform.position = vec;
        }

        if (Input.GetKey("right"))
        {
            if (gameObject.transform.localScale.x != 1)
            {
                vec.x = 1.0f;
                vec.y = 1.0f;
                vec.z = 1.0f;
                gameObject.transform.localScale = vec;
            }
            vec.x = gameObject.transform.position.x;
            vec.y = gameObject.transform.position.y;
            vec.z = (float)(gameObject.transform.position.z + .1);
            gameObject.transform.position = vec;
        }

        if (Input.GetKey("down") && touch_fence == false)
        {
            vec.x = (float)(gameObject.transform.position.x + .1);
            vec.y = gameObject.transform.position.y;
            vec.z = gameObject.transform.position.z;
            gameObject.transform.position = vec;
        }

        if (Input.GetKey("left"))
        {
            if (gameObject.transform.localScale.x != -1)
            {
                vec.x = -1.0f;
                vec.y = 1.0f;
                vec.z = 1.0f;
                gameObject.transform.localScale = vec;
            }
            vec.x = gameObject.transform.position.x;
            vec.y = gameObject.transform.position.y;
            vec.z = (float)(gameObject.transform.position.z - .1);
            gameObject.transform.position = vec;
        }

        if(attack && touch_boss)
        {
            boss_punched = true;
        }
        else
        {
            boss_punched = false;
        }
        if (attack && touch_grunt)
        {
            anime_grunt = GameObject.FindGameObjectWithTag("Child_Enemy").GetComponent<Animator>();
            Destroy(GameObject.FindGameObjectWithTag("Child_Enemy").GetComponent<Enemy_AI>());
            gameObject.GetComponent<Spawn_Enemy>().grunt_death_count++;
            touch_grunt = false;
            drop_brains = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 4f));
            if(drop_brains == 1)
            {
                drop_health();
            }
            anime_grunt.SetBool("Death", true);
        }
        if (attack && touch_rocket)
        {
            anime_rocket = GameObject.FindGameObjectWithTag("Rocket_Enemy").GetComponent<Animator>();
            Destroy(GameObject.FindGameObjectWithTag("Rocket_Enemy").GetComponent<rocket_enemy_ai>());
            anime_rocket.SetBool("Death", true);
            gameObject.GetComponent<Spawn_Enemy>().rocket_death_count++;
            touch_rocket = false;
        }

        if (rocket_hit)
        {
            if (health > 1)
            {
                string name = heart + " " + heart_num.ToString();
                heart_num--;
                Destroy(GameObject.FindGameObjectWithTag(name));
                name = heart + " " + heart_num.ToString();
                heart_num--;
                Destroy(GameObject.FindGameObjectWithTag(name));
            }
            else
            {
                string name = heart + " " + heart_num.ToString();
                heart_num--;
                Destroy(GameObject.FindGameObjectWithTag(name));
            }
            //StartCoroutine(multi_anime(gameObject, "Zombie Player Idle"));
            rocket_hit = false;
        }
        if (got_hit)
        {
            string name = heart + " " + heart_num.ToString();
            heart_num--;
            Destroy(GameObject.FindGameObjectWithTag(name));
            //StartCoroutine(multi_anime(gameObject, "Zombie Player Idle"));
            got_hit = false;
        }
        if (health <= 0)
        {
            anime.SetBool("Walk", false);
            anime.SetBool("Attack", false);
            anime.SetBool("Death", true);
            Destroy(gameObject.GetComponent<char_CTRL>());
        }
    }

    private void drop_health()
    {
        brain = Instantiate(GameObject.FindGameObjectWithTag("Brain"));

        vec.x = 1f;
        vec.y = 1f;
        vec.z = 1f;
        brain.transform.localScale = vec;
        vec.x = 10f;
        vec.y = 90f;
        vec.z = 0;
        brain.transform.parent = null;
        brain.transform.rotation = Quaternion.Euler(vec);
        BC = brain.AddComponent<BoxCollider>();
        brain.AddComponent<Brain_AI>();
        vec = BC.size;
        vec.y = .76f;
        vec.x = .76f;
        vec.z = .76f;
        BC.size = vec;
        brain.tag = "_Brain";

        vec.x = gameObject.transform.position.x;
        vec.y = 1.28f;
        vec.z = gameObject.transform.position.z+2;
        brain.transform.position = vec;
    }

    private void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.tag == "Child_Enemy")
        //{
        //    print("touch");
        //    touch_grunt = true;
        //}
        //if (other.gameObject.tag == "Rocket_Enemy")
        //{
        //    touch_rocket = true;
        //}
        if (other.gameObject.tag == "_Rocket")
        {
            health-=2;
            Destroy(other.gameObject);
            //rocket_hit = true;
        }

        if (other.gameObject.tag == "_Bullet")
        {
            health--;
            Destroy(other.gameObject);
            //got_hit = true;
        }
        if(other.gameObject.tag == "Ground")
        {
            touch_ground = true;
        }
        if(other.gameObject.tag == "backGround")
        {
            back_g_hit = true;
        }
        if(other.gameObject.tag == "_Brain")
        {
            brains = 1; ;
            Destroy(other.gameObject);
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
        if (other.gameObject.tag == "c_fence")
        {
            touch_c_fence = false;
        }
        if (other.gameObject.tag == "fence")
        {
            touch_fence = false;
        }
        //if (other.gameObject.tag == "Child_Enemy")
        //{
        //    touch_grunt = false;
        //}
        //if (other.gameObject.tag == "Rocket_Enemy")
        //{
        //    touch_rocket = false;
        //}
        if (other.gameObject.tag == "backGround")
        {
            back_g_hit = false;
        }
    }
}
