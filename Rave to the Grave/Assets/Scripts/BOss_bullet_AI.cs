using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOss_bullet_AI : MonoBehaviour {

    public Vector3 vec;
    private GameObject shooter;
    public bool facing_left;
    public Vector3 shooter_pos;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!facing_left)
        {
            shoot_left();
        }
        else
        {
            shoot_right();
        }
    }

    private void shoot_right()
    {
        if (gameObject.transform.position.z < shooter_pos.z + 10)
        {
            vec.x = gameObject.transform.position.x;
            vec.z = (float)(gameObject.transform.position.z + .2);
            vec.y = gameObject.transform.position.y;
            gameObject.transform.position = vec;
        }
        if (gameObject.transform.position.z > shooter_pos.z + 10)
        {
            Destroy(gameObject);
        }
    }

    private void shoot_left()
    {
        if (gameObject.transform.position.z > shooter_pos.z - 10)
        {
            vec.x = gameObject.transform.position.x;
            vec.z = (float)(gameObject.transform.position.z - .2);
            vec.y = gameObject.transform.position.y;
            gameObject.transform.position = vec;
        }
        if (gameObject.transform.position.z < shooter_pos.z - 10)
        {
            Destroy(gameObject);
        }
    }
}
