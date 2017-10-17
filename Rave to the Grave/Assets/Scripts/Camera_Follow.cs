﻿using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object


    private Vector3 offset;         //Private variable to store the offset distance between the player and camera


    private Vector3 vec;
    // Use this for initialization
    void Start () 
    {

        player = gameObject.GetComponentInParent<GameObject>();
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }
    
    // LateUpdate is called after Update each frame
    void LateUpdate () 
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        vec.x = player.transform.position.x + offset.x;
        vec.y = player.transform.position.y + offset.y;
        vec.z = -5.12636f;
        transform.position = vec;
    }
}