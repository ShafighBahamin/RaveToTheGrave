using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch_Box : MonoBehaviour {

    public char_CTRL CharController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Child_Enemy")
        {
            CharController.touch_grunt = true;
        }
        if (other.gameObject.tag == "_boss")
        {
            CharController.touch_boss = true;
        }
        if (other.gameObject.tag == "Rocket_Enemy")
        {
            CharController.touch_rocket = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Child_Enemy")
        {
            CharController.touch_grunt = false;
        }
        if (other.gameObject.tag == "_boss")
        {
            CharController.touch_boss = false;
        }
        if (other.gameObject.tag == "Rocket_Enemy")
        {
            CharController.touch_rocket = false;
        }
    }
}
