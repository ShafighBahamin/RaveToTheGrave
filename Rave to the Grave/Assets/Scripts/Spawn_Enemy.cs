
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    private float spawnTime = 3f;
    private int spawns;
    private int spawns_rocket;
    private int spawns_boss;
    private Vector3 vec;
    private int total_grunts;
    private int total_bosses;
    private int total_rockets;
    private int rand_num;
    private int dist;
    private int how_many_bosses;
    private bool boss_mode;
    private bool called_invoke;

    public int boss_death_count;
    public int rocket_death_count;
    public int grunt_death_count;

    //obj is enemy
    private GameObject obj;
    private GameObject player;
    private GameObject sprite;
    private GameObject sprite_rocket;


    void Start()
    {
        total_bosses = 0;
        boss_death_count = 0;
        boss_mode = false;
        grunt_death_count = 0;
        rocket_death_count = 0;
        total_grunts = 1;
        total_rockets = 1;
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        InvokeRepeating("Spawn_Rocket", spawnTime, spawnTime);
        //InvokeRepeating("Spawn_boss", spawnTime, spawnTime);
        spawns = 1;
        spawns_rocket = 1;
        how_many_bosses = 0;
        spawns_boss = 0;
        player = GameObject.FindGameObjectWithTag("Sprite");
        called_invoke = false;
    }

    private void Update()
    {

        if (boss_mode && !called_invoke)
        {
            called_invoke = true;
            InvokeRepeating("Spawn_boss", spawnTime, spawnTime);
        }
        else if(!boss_mode)
        {
            if (GameObject.FindGameObjectWithTag("Rocket_Enemy") == null && spawns_rocket < 1 && rocket_death_count < total_rockets)
            {
                spawns_rocket = 2;
            }
            if (GameObject.FindGameObjectWithTag("Child_Enemy") == null && spawns < 1 && grunt_death_count < total_grunts)
            {
                spawns = 3;
            }
            if ((grunt_death_count + rocket_death_count) == (total_grunts + total_rockets))
            {
                called_invoke = false;
                boss_mode = true;

                CancelInvoke("Spawn");
                CancelInvoke("Spawn_Rocket");

                how_many_bosses += 1;
                spawns_boss += how_many_bosses;

                grunt_death_count = 0;
                rocket_death_count = 0;

                total_rockets += 2;
                total_grunts += 2;
            }
        }
        
        if(boss_death_count == how_many_bosses)
        {
            boss_death_count = 0;
            boss_mode = false;
            CancelInvoke("Spawn_boss");
            InvokeRepeating("Spawn", spawnTime, spawnTime);
            InvokeRepeating("Spawn_Rocket", spawnTime, spawnTime);
        }
    }




    void Spawn_Rocket()
    {
        dist = 15;
        //adjust spawn number
        if (spawns_rocket > 0)
        {
            //obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
            sprite_rocket = Instantiate(GameObject.FindGameObjectWithTag("Rocket Enemy"));
            sprite_rocket.tag = "Rocket_Enemy";
            vec.x = Random.Range(2.3f, 15f);
            vec.y = gameObject.transform.position.y;
            rand_num = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 2f));
            if (rand_num == 0 && gameObject.transform.position.z > 4.5)
            {
                dist *= -1;
            }
            rand_num = (int)Mathf.Floor(UnityEngine.Random.Range(8f, 15f));
            vec.z = gameObject.transform.position.z + rand_num;
            sprite_rocket.transform.position = vec;
            vec.x = 10;
            vec.y = -90f;
            vec.z = 0;

            sprite_rocket.transform.rotation = Quaternion.Euler(vec);
            Rigidbody rb = sprite_rocket.AddComponent<Rigidbody>();
            BoxCollider BC = sprite_rocket.AddComponent<BoxCollider>();
            vec = BC.size;
            vec.x = 1.21f;
            vec.z = 1.5f;
            BC.size = vec;
            //BC.isTrigger = true;
            //sprite.AddComponent<BoxCollider2D>();
            sprite_rocket.transform.parent = null;
            rocket_enemy_ai E_AI = sprite_rocket.AddComponent<rocket_enemy_ai>();
            rb.mass = 5;
            rb.freezeRotation = true;
            //obj.transform.Rotate(vec, Time.deltaTime*10f);
            spawns_rocket--;
        }
    }


    void Spawn()
    {
        dist = 10;
        //adjust spawn number
        if (spawns > 0)
        {
            //obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
            sprite = Instantiate(GameObject.FindGameObjectWithTag("Enemy"));
            sprite.tag = "Child_Enemy";
            vec.x = Random.Range(2.3f, 15f);
            vec.y = gameObject.transform.position.y;
            rand_num = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 2f));
            if (rand_num == 0 && gameObject.transform.position.z > .5)
            {
                dist *= -1;
            }
            vec.z = gameObject.transform.position.z + dist;
            sprite.transform.position = vec;
            vec.x = 10;
            vec.y = -90f;
            vec.z = 0;

            sprite.transform.rotation = Quaternion.Euler(vec);
            Rigidbody rb = sprite.AddComponent<Rigidbody>();
            BoxCollider BC = sprite.AddComponent<BoxCollider>();
            vec = BC.size;
            vec.x = 1.21f;
            vec.z = 1.5f;
            BC.size = vec;
            //BC.isTrigger = true;
            //sprite.AddComponent<BoxCollider2D>();
            sprite.transform.parent = null;
            Enemy_AI E_AI = sprite.AddComponent<Enemy_AI>();
            rb.mass = 5;
            rb.freezeRotation = true;
            //obj.transform.Rotate(vec, Time.deltaTime*10f);
            spawns--;
        }
    }


    void Spawn_boss()
    {
        dist = 10;
        //adjust spawn number
        if (spawns_boss > 0)
        {
            //obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
            sprite = Instantiate(GameObject.FindGameObjectWithTag("Boss"));
            sprite.tag = "_boss";
            vec.x = Random.Range(2.3f, 15f);
            vec.y = gameObject.transform.position.y;
            rand_num = (int)Mathf.Floor(UnityEngine.Random.Range(0f, 2f));
            if (rand_num == 0 && gameObject.transform.position.z > .5)
            {
                dist *= -1;
            }
            vec.z = gameObject.transform.position.z + dist;
            sprite.transform.position = vec;
            vec.x = 10;
            vec.y = -90f;
            vec.z = 0;

            sprite.transform.rotation = Quaternion.Euler(vec);
            Rigidbody rb = sprite.AddComponent<Rigidbody>();
            BoxCollider BC = sprite.AddComponent<BoxCollider>();
            vec = BC.size;
            vec.x = 1.21f;
            vec.y = 3f;
            vec.z = 1.5f;
            BC.size = vec;
            //BC.isTrigger = true;
            //sprite.AddComponent<BoxCollider2D>();
            sprite.transform.parent = null;
            Boss_AI B_AI = sprite.AddComponent<Boss_AI>();
            rb.mass = 5;
            rb.freezeRotation = true;
            //obj.transform.Rotate(vec, Time.deltaTime*10f);
            spawns_boss--;
        }
    }
}





