using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPlatform : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    [SerializeField] private bool flip;
    private Rigidbody2D rb;
    private Vector3 initialPosition; // Store the initial position

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position; // Initialize the initial position
    }

    void FixedUpdate()
    {
        float x, y;

        if (!flip)
        {
            x = Mathf.Cos(Time.time * frequency) * amplitude;
            y = Mathf.Sin(Time.time * frequency) * amplitude;
        }
        else
        {
            x = Mathf.Cos(Time.time * -frequency) * amplitude;
            y = Mathf.Sin(Time.time * -frequency) * amplitude;
        }

        rb.MovePosition(new Vector3(x, y, transform.position.z) + initialPosition); // Add initial position
    }
}
