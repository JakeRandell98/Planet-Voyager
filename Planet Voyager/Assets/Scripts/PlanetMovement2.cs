using UnityEngine;

public class PlanetMovement2 : MonoBehaviour
{
    public OrbitPath orbitPath;
    public CircleCollider2D mouseCollider;
    public CircleCollider2D objectCollider;
    public float maxAngle;
    public float minAngle;
    public float startingYpos;
    public bool rocketFlying;

    private bool isBeingHeld;

    private void Start()
    {
        gameObject.transform.position = PositionOnOrbit(new Vector3(0, startingYpos, -2));
        mouseCollider.enabled = true;
        objectCollider.enabled = false;
    }

    private void Update()
    {
        if ((isBeingHeld == true) && (rocketFlying == false))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 planetPos = PositionOnOrbit2(mousePos);
            if (ValidPlanetPosition(planetPos))
            {
                planetPos.z = -2f;
                gameObject.transform.position = planetPos;
            }
        }
    }

    private Vector3 PositionOnOrbit(Vector3 mousePos)
    {
        Vector3 planetPos = new Vector3(0, mousePos.y, -2);
        planetPos.x = Mathf.Sqrt(Mathf.Pow(orbitPath.radius, 2) -
                Mathf.Pow(planetPos.y - orbitPath.origin.y, 2)) + orbitPath.origin.x;
        return planetPos;
    }

    private Vector3 PositionOnOrbit2(Vector3 mousePos)
    {
        Vector3 mouseVector = mousePos - orbitPath.origin;
        mouseVector.z = 0;
        mouseVector.Normalize();
        Vector3 planetPos = orbitPath.origin + (mouseVector * orbitPath.radius);

        return planetPos;
    }

    private bool ValidPlanetPosition(Vector3 planetPos)
    {
        Vector3 forwardVector = new Vector3(1, 0, 0);
       
        Vector3 planetVector = new Vector3(planetPos.x - orbitPath.origin.x, planetPos.y - orbitPath.origin.y, 0);
        float angle = Vector3.SignedAngle(forwardVector, planetVector, new Vector3(0,0,1));

        return (angle < maxAngle) && (angle > minAngle);
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isBeingHeld = true;
        }
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
    }

    private void inFlight()
    {
        mouseCollider.enabled = false;
        objectCollider.enabled = true;
        rocketFlying = true;
    }

    void ResetLevel()
    {
        mouseCollider.enabled = true;
        objectCollider.enabled = false;
        rocketFlying = false;
    }

}
