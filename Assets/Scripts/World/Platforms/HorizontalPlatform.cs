using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatform : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;
    [SerializeField] private bool flip;

    private float initialX;

    void Start()
    {
        // Store the initial x position
        initialX = transform.position.x;
    }

    void FixedUpdate()
    {
        float y = transform.position.y;
        float x;

        if (!flip)
        {
            x = initialX + Mathf.Cos(Time.time * frequency) * amplitude;
        }
        else
        {
            x = initialX + Mathf.Cos(Time.time * -frequency) * amplitude;
        }

        transform.position = new Vector3(x, y, transform.position.z);
    }
}
