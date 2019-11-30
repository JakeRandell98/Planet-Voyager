using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseThrustButton : MonoBehaviour
{
    public GameObject rocket;

    public void onPress()
    {
        rocket.GetComponent<RocketController>().reverseThrust = true;
    }

    public void onRelease()
    {
        rocket.GetComponent<RocketController>().reverseThrust = false;
    }

}
