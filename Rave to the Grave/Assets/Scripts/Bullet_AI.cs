using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_AI : MonoBehaviour {


    public Vector3 vec;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.z > -12)
        {
            vec.x = gameObject.transform.position.x;
            vec.z = (float)(gameObject.transform.position.z - .2);
            vec.y = gameObject.transform.position.y;
            gameObject.transform.position = vec;
        }
        if (gameObject.transform.position.z < -12)
        {
            Destroy(gameObject);
        }
	}
}
