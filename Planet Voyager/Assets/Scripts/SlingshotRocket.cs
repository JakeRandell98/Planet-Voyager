using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotRocket : MonoBehaviour
{
    public GameObject Dot;
    public int noOfDots;
    public float lineSpeed;
    public int distance;
    public bool isBeingHeld = false;
    public float launchAngle;
    private GameObject rocket;
    public Vector3 rocketPos;
    public Vector3 slingshotVector;
    public Vector3 newMousePos;
    public Vector3 positiveX;
    public GameObject[] dotArray;
    private float correctedLineSpeed;
    public bool rocketFlying;

    private void Awake()
    {
        rocket = GameObject.Find("Rocket");
        rocketPos = rocket.transform.position;
        rocketFlying = false;
        dotArray = new GameObject[noOfDots];
        for (int i = 0; i < noOfDots; i++)
        {
            dotArray[i] = Instantiate(Dot, new Vector3(0, 0, 0), Quaternion.identity);
            dotArray[i].SetActive(false);
        }
    }

    void Update()
    {
        if ((isBeingHeld == true) && (rocketFlying == false))
        {
            positiveX = Vector3.right;
            newMousePos = Input.mousePosition;
            newMousePos = Camera.main.ScreenToWorldPoint(newMousePos);
            newMousePos.z = -2f;
            slingshotVector = rocketPos - newMousePos;
            if (newMousePos.y < rocketPos.y)
                launchAngle = Mathf.Acos(Vector3.Dot(slingshotVector, positiveX)/Vector3.Magnitude(slingshotVector));
            else
                launchAngle = -Mathf.Acos(Vector3.Dot(slingshotVector, positiveX) / Vector3.Magnitude(slingshotVector));
            rocket.GetComponentInChildren<SpriteRenderer>().transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * launchAngle + 90);
            for (int i = 0; i < noOfDots; i++)
            {
                dotArray[i].SetActive(true);
            }
        } 
        else
        {
            for (int i = 0; i < noOfDots; i++)
            {
                dotArray[i].SetActive(false);
            }
        }
        correctedLineSpeed = 1 / lineSpeed;
        CalculateArc();
    }

    void CalculateArc()
    {
        for (int i = 0; i < noOfDots; i++)
        {
            float timeVariant = (1f/ correctedLineSpeed) *(Time.time % correctedLineSpeed);
            float t = ((float)i + timeVariant) / (float)noOfDots;
            dotArray[i].transform.position = rocketPos + t * distance * new Vector3(Mathf.Cos(launchAngle), Mathf.Sin(launchAngle), 0f);
            dotArray[i].transform.position += new Vector3(0, 0,-2f);
        }
    }

    void ResetLevel()
    {
        rocketFlying = false;
    }
}
