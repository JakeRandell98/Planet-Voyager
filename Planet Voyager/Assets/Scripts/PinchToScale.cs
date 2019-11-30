using UnityEngine;

public class PinchToScale : MonoBehaviour
{
    public bool scalable;
    public float scaleSpeed = 0.005f;
    public float maxScale = 5.0f;
    public float minScale = 0.4f;
    private bool isBeingHeld;
  
    private void Update()
    {
        if (Input.touchCount == 2 && isBeingHeld && scalable)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;


            Vector3 scaleVector = new Vector3(-deltaMagnitudeDiff * scaleSpeed,
                -deltaMagnitudeDiff * scaleSpeed, -deltaMagnitudeDiff * scaleSpeed);
            if ((scaleVector + gameObject.transform.localScale).magnitude <=
                maxScale && (scaleVector + gameObject.transform.localScale).magnitude >= minScale)
            {
                gameObject.transform.localScale += scaleVector;
            }
        }
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
        gameObject.BroadcastMessage("CalculatePlanetMass");
    }
}
