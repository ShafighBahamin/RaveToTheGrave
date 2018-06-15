using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain_AI : MonoBehaviour {
    private bool touch_ground;
    private Vector3 vec;
	// Use this for initialization
	void Start () {
        touch_ground = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(touch_ground == true)
        {
            vec.x = gameObject.transform.position.x;
            vec.z = gameObject.transform.position.z;
            vec.y = gameObject.transform.position.y;
            gameObject.transform.position = vec;
        }
	}

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            touch_ground = true;
        }
    }
}
