
using UnityEngine;

public class Spawn_Enemy : MonoBehaviour
{
    public float spawnTime = 3f;
    public int spawns;
    public Vector3 vec;

    //obj is enemy
    public GameObject obj;

    public GameObject sprite;
    
    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        spawns = 1;
    }

    private void Update()
    {
    }
    void Spawn()
    {
        //adjust spawn number
        if (spawns > 0)
        {
            obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
            sprite = Instantiate(GameObject.FindGameObjectWithTag("Sprite"));
            sprite.transform.parent = obj.transform;
            vec.x = .25f;
            vec.y = .25f;
            vec.z = .25f;
            obj.transform.localScale = vec;
            obj.AddComponent<BoxCollider>();
            obj.AddComponent<Enemy_AI>();
            vec.x = Random.Range(2.22f, 5.5f);
            vec.y = 1.28f;
            vec.z = gameObject.transform.position.z + 10;
            obj.transform.position = vec;
            vec.x = 90f;
            vec.y = 90f;
            vec.z = 0;

            obj.transform.rotation = Quaternion.Euler(vec);
            //obj.transform.Rotate(vec, Time.deltaTime*10f);
            spawns--;
        }
    }
}