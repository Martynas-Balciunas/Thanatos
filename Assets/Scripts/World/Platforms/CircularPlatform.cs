using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPlatform : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    [SerializeField] private bool flip;
    private Rigidbody2D rb;
    private float initialY;
    private float initialX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialY = transform.position.y;
        initialX = transform.position.x;
    }

    void FixedUpdate()
    {
        if (!flip)
        {
            float x = initialX + Mathf.Cos(Time.time * frequency) * amplitude;
            float y = initialY + Mathf.Sin(Time.time * frequency) * amplitude;
            rb.MovePosition(new Vector3(x, y, transform.position.z));
        }
        else
        {
            float x = initialX + Mathf.Cos(Time.time * -frequency) * amplitude;
            float y = initialY + Mathf.Sin(Time.time * -frequency) * amplitude;
            rb.MovePosition(new Vector3(x, y, transform.position.z));
        }
    }
}
