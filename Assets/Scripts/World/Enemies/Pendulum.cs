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
    private bool isSwingingRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on " + gameObject.name);
            return;
        }

        leftRad = leftAngle * Mathf.Deg2Rad;
        rightRad = rightAngle * Mathf.Deg2Rad;
        rb.angularVelocity = speed * 10; // gives initial impulse anti-clockwise for it to reach the rightAngle
        isSwingingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb == null) return;

        float currentAngle = transform.eulerAngles.z;
        if (currentAngle > 180) currentAngle -= 360; // normalize angle to range [-180, 180]

        // if pendulum exceeds the right angle limit
        if (currentAngle > rightAngle)
        {
            rb.angularVelocity = -speed * 10; // makes pendulum swing clockwise
            isSwingingRight = false;
        }
        // if pendulum exceeds the left angle limit
        else if (currentAngle < leftAngle)
        {
            rb.angularVelocity = speed * 10; // makes pendulum swing anti-clockwise
            isSwingingRight = true;
        }
    }
}
