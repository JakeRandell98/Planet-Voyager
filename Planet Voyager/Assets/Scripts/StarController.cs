using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public bool rocketFlying;
    private float rotationAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationAngle = 30 * Time.time;
        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }

    private void inFlight()
    {
        rocketFlying = true;
    }

    void ResetLevel()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }
}
