using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardThrustButton : MonoBehaviour
{
    public GameObject rocket;

    public void onPress()
    {
        rocket.GetComponent<RocketController>().forwardThrust = true;
    }

    public void onRelease()
    {
        rocket.GetComponent<RocketController>().forwardThrust = false;
    }

}
