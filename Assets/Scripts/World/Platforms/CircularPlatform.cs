using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPlatform : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    [SerializeField] private bool flip;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!flip)
        {
            float x = Mathf.Cos(Time.time * frequency) * amplitude;
            float y = Mathf.Sin(Time.time * frequency) * amplitude;
            rb.MovePosition(new Vector3(x, y, transform.position.z));
        }
        else
        {
            float x = Mathf.Cos(Time.time * -frequency) * amplitude;
            float y = Mathf.Sin(Time.time * -frequency) * amplitude;
            rb.MovePosition(new Vector3(x, y, transform.position.z));
        }
    }
}
