using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour {

    private GameObject[] grunts;
    private GameObject[] rockets;

    // Use this for initialization
    void Start () {
        //call clean repeatedly
        InvokeRepeating("clean", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	}
    void clean()
    {
        //get a list of the enemies
        grunts = GameObject.FindGameObjectsWithTag("Child_Enemy");
        rockets = GameObject.FindGameObjectsWithTag("Rocket_Enemy");
        //check if player death
        if(gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Zombie Death") == true)
        {
            Application.Quit();
        }
        //check for deaths for both tpes of enemies
        foreach (GameObject g in grunts)
        {
            if (g.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Soldier Death") == true)
            {
                Destroy(g);
            }
        }
        foreach (GameObject g in rockets)
        {
            if (g.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Death Rocket Enemy") == true)
            {
                Destroy(g);
            }
        }
    }
}
