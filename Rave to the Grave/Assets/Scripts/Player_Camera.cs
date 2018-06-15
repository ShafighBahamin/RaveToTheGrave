using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour {

    public GameObject Player;

    public Vector3 vec;
    Camera cam;
	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Sprite");
	}

    // Update is called once per frame
    void Update() {
        vec.z = Player.transform.position.z;
        vec.y = Player.transform.position.y + 1.64f;
        vec.x = Player.transform.position.x + 6f;
        gameObject.transform.position = vec;
    
	}
}
