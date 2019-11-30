using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGlow : MonoBehaviour  
{

    public SpriteRenderer glow;
    private float alpha;
    private float scale;
    private float initialScale;
    public float gravityRadius;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale.x;
        gravityRadius = glow.bounds.extents.x;
    }

    // Update is called once per frame
    void Update()
    {
        alpha = 0.2f + (Mathf.Sin(2 * Time.time)) / 40f;
        scale = initialScale + ((-Mathf.Sin(2 * Time.time)) / 200f);
        glow.color = new Color (1f, 1f, 1f, alpha);
        transform.localScale = new Vector3(scale, scale, 1.0f);
    }
}
