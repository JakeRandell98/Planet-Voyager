using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketController : MonoBehaviour
{
    public GameObject rocketLaunchController;
    public GameObject rocketSprite;
    public GameObject planets;
    public CircleCollider2D mouseCollider;
    public PolygonCollider2D objectCollider;
    public GameObject explosion;
    public Text winText;
    public GameObject fuelBar;
    public float initial_speed;
    private float speed;
    private float launchAngle;
    public Vector3 velocity;
    private Vector3 direction;
    public Vector3 rocketTotalAcceleration;
    private bool rocketFlying;
    private float rotationAngle;
    public int starCount;
    private Vector3 initialRocketPos;
    public float totalFuel;
    public float accelerationPerFuel;
    public float fuelCount;
    public bool forwardThrust;
    public bool reverseThrust;
    private float rotationOffset;
    private float rot;
    private float newRot;
    public float accelerationMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        rocketLaunchController.GetComponent<SlingshotRocket>().isBeingHeld = false;
        speed = 0;
        mouseCollider.enabled = true;
        objectCollider.enabled = false;
        rocketFlying = false;
        rocketTotalAcceleration = new Vector3(0, 0, 0);
        initialRocketPos = transform.position;
        fuelCount = totalFuel;
        fuelBar.GetComponent<Slider>().value = fuelCount / totalFuel;
        rotationOffset = 90;
        accelerationMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (rocketFlying == true)
        {
            rot = Mathf.Atan2(velocity.y, velocity.x);
            rocketTotalAcceleration = new Vector3(0, 0, 0);
            rotationOffset = accelerationMultiplier * 90;
            for (int i = 0; i < planets.transform.childCount; i++)
            {
                Transform planet = planets.transform.GetChild(i);
                rocketTotalAcceleration += planet.GetComponentInChildren<OrbitalDynamics>().rocketAcceleration;
            }
            if (forwardThrust && (fuelCount > 0))
            {
                rocketTotalAcceleration += accelerationPerFuel * new Vector3(Mathf.Cos(rot), Mathf.Sin(rot), 0f) * accelerationMultiplier;
                fuelCount -= Time.deltaTime;
                fuelBar.GetComponent<Slider>().value = fuelCount / totalFuel;
                rotationOffset = accelerationMultiplier * 90;
            }
            if (reverseThrust && (fuelCount > 0))
            {
                rocketTotalAcceleration += accelerationPerFuel * new Vector3(-Mathf.Cos(rot), -Mathf.Sin(rot), 0f) * accelerationMultiplier;
                fuelCount -= Time.deltaTime;
                fuelBar.GetComponent<Slider>().value = fuelCount / totalFuel;
                rotationOffset = accelerationMultiplier * -90;
            }
            velocity += rocketTotalAcceleration * Time.deltaTime;
            rotationAngle = Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x);
            newRot = rotationAngle * Mathf.Deg2Rad;
            if (Mathf.Abs(newRot - rot) > (Mathf.PI / 2) && (forwardThrust || reverseThrust))
            {
                accelerationMultiplier = -accelerationMultiplier;
                if (forwardThrust && (fuelCount > 0))
                {
                    rocketTotalAcceleration += accelerationPerFuel * new Vector3(Mathf.Cos(rot), Mathf.Sin(rot), 0f) * accelerationMultiplier;
                    rotationOffset = accelerationMultiplier * 90;
                }
                if (reverseThrust && (fuelCount > 0))
                {
                    rocketTotalAcceleration += accelerationPerFuel * new Vector3(-Mathf.Cos(rot), -Mathf.Sin(rot), 0f) * accelerationMultiplier;
                    rotationOffset = accelerationMultiplier * -90;
                }
                velocity += rocketTotalAcceleration * Time.deltaTime;
                rotationAngle = Mathf.Rad2Deg * Mathf.Atan2(velocity.y, velocity.x);
                newRot = rotationAngle * Mathf.Deg2Rad;
            }
            rocketSprite.transform.rotation = Quaternion.Euler(0, 0, rotationAngle + rotationOffset);
        }
        transform.Translate(velocity);
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rocketLaunchController.GetComponent<SlingshotRocket>().isBeingHeld = true;
        }
    }

    private void OnMouseUp()
    {
        rocketLaunchController.GetComponent<SlingshotRocket>().isBeingHeld = false;
        speed = initial_speed;
        launchAngle = rocketLaunchController.GetComponent<SlingshotRocket>().launchAngle;
        direction = new Vector3(Mathf.Cos(launchAngle), Mathf.Sin(launchAngle), 0f);
        velocity = speed * direction;
        rocketLaunchController.GetComponent<SlingshotRocket>().rocketFlying = true;
        rocketFlying = true;
        planets.BroadcastMessage("inFlight");
        mouseCollider.enabled = false;
        objectCollider.enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.transform.parent.name == "Planets")
        {
            gameObject.SetActive(false);
            explosion.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
            explosion.SetActive(true);
            explosion.BroadcastMessage("ExplodeSound");
        }
        if (collision.collider.transform.parent.name == "Stars")
        {
            starCount++;
            AudioSource starAudioSource = collision.collider.transform.gameObject.GetComponent<AudioSource>();
            collision.collider.enabled = false;
            collision.collider.transform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(StarCollected(starAudioSource, collision));
            if (starCount == 3)
            {
                winText.gameObject.SetActive(true);
            }
        }


    }

    IEnumerator StarCollected(AudioSource audioSource, Collision2D collision)
    {
        audioSource.PlayOneShot(audioSource.clip, 1.0f);
        yield return new WaitWhile(() => audioSource.isPlaying);
    }

    void ResetLevel()
    {
        rocketLaunchController.GetComponent<SlingshotRocket>().isBeingHeld = false;
        speed = 0;
        velocity = new Vector3(0, 0, 0);
        mouseCollider.enabled = true;
        objectCollider.enabled = false;
        rocketFlying = false;
        rocketTotalAcceleration = new Vector3(0, 0, 0);
        rocketSprite.transform.localPosition = new Vector3(0, 0, 0);
        rocketSprite.transform.localRotation = Quaternion.Euler(0, 0, 90);
        transform.position = initialRocketPos;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        fuelCount = totalFuel;
        fuelBar.GetComponent<Slider>().value = fuelCount / totalFuel;
        rotationOffset = 90;
        accelerationMultiplier = 1;
        starCount = 0;
    }
}
