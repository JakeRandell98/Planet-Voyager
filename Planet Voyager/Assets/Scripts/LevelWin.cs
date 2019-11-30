using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelWin : MonoBehaviour
{
    private float scale;
    private float initialScale;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        initialScale = transform.localScale.x;
    }

    private void Update()
    {
        scale = initialScale + ((-Mathf.Sin(2 * Time.time)) / 10f);
        transform.localScale = new Vector3(scale, scale, 1.0f);
    }

    void ResetLevel()
    {
        gameObject.SetActive(false);
    }
}
