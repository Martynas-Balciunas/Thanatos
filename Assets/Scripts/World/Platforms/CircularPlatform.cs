using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPlatform : MonoBehaviour
{
    [SerializeField] private float frequency = 1.0f;
    [SerializeField] private float amplitude = 1.0f;
    [SerializeField] private bool flip = false;
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

        // Move the platform by adding the initial position to the calculated position
        rb.MovePosition(new Vector3(x, y, transform.position.z) + initialPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null, true);
        }
    }
}
