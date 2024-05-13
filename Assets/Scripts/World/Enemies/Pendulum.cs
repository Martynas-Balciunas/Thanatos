using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float speed;//impulse strength
    private Rigidbody2D rb;
    public float leftAngle;
    public float rightAngle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = speed * 10;//gives initial impulse anti clockwise for it to reach the rightAngle
    }

    // Update is called once per frame
    void Update()
    {
        //if pendulum exceeds the right angle limit
        if (transform.rotation.z > rightAngle)
        {
            rb.angularVelocity = -1 * speed;//makes pendulum swing clockwise
        }
        //if pendulum exceeds the left angle limit
        if (transform.rotation.z < leftAngle)
        {
            rb.angularVelocity = speed;//makes pendulum swing anti clockwise
        }
    }

}