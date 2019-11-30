using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalDynamics : MonoBehaviour    
{
    public bool rocketFlying;
    public GameObject rocket;
    public SpriteRenderer planetSprite;
    public float planetDensity = 5515;
    private float rocketRadialDistance;
    public Vector3 rocketAcceleration;
    public float rocketAccelerationMagnitude;
    public Vector3 rocketAccelerationDirection;
    private float gravitationalConstant;
    private float planetVolume;
    private float planetMass;

    // Start is called before the first frame update
    void Start()
    {
        gravitationalConstant = 6.67408e-20f;
        CalculatePlanetMass();
        rocketAcceleration = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (rocketFlying)
        {
            // 1 unit = 1000km
            rocketAcceleration = new Vector3(0, 0, 0);
            rocketRadialDistance = (rocket.transform.position - transform.position).magnitude; // In units
            if (rocketRadialDistance <= GetComponentInChildren<GravityGlow>().gravityRadius)
            {
                rocketAccelerationMagnitude = (gravitationalConstant * planetMass) / Mathf.Pow(1000 * rocketRadialDistance, 2f);
                rocketAccelerationDirection = (transform.position - rocket.transform.position).normalized;
                rocketAcceleration = rocketAccelerationMagnitude * rocketAccelerationDirection;
            }
        }
    }

    public void CalculatePlanetMass()
    {
        planetVolume = (4f / 3f) * Mathf.PI * Mathf.Pow(1000000 * planetSprite.bounds.extents.x, 3f);
        planetMass = planetVolume * planetDensity;
    }

    private void inFlight()
    {
        rocketFlying = true;
    }

    void ResetLevel()
    {
        rocketFlying = false;
        rocketAcceleration = new Vector3(0, 0, 0);
    }
}
