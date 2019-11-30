using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    private GameObject level;
    public GameObject rocket;
    public GameObject stars;

    public void StartOver()
    {
        level = transform.root.gameObject;
        rocket.SetActive(true);
        for (int i = 0; i < stars.transform.childCount; i++)
        {
            GameObject star = stars.transform.GetChild(i).gameObject;
            star.SetActive(true);
        }
        level.BroadcastMessage("ResetLevel");
    }
}
