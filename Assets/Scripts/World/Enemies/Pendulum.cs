using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float speed; // impulse strength
    private Rigidbody2D rb;
    public float leftAngle; // in degrees
    public float rightAngle; // in degrees
    private float leftRad;
    private float rightRad;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        leftRad = leftAngle * Mathf.Deg2Rad;
        rightRad = rightAngle * Mathf.Deg2Rad;
        rb.angularVelocity = speed * 10; // gives initial impulse anti-clockwise for it to reach the rightAngle
    }

    // Update is called once per frame
    void Update()
    {
        // if pendulum exceeds the right angle limit
        if (transform.eulerAngles.z > rightAngle && transform.eulerAngles.z < 360 - leftAngle)
        {
            rb.angularVelocity = -speed * 10; // makes pendulum swing clockwise
        }
        // if pendulum exceeds the left angle limit
        else if (transform.eulerAngles.z < 360 - rightAngle && transform.eulerAngles.z > leftAngle)
        {
            rb.angularVelocity = speed * 10; // makes pendulum swing anti-clockwise
        }
    }
}
