using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidbody2D;
    public float leftAngle;
    public float rightAngle;
    bool movingClockwise;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.angularVelocity = speed*10;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.z > rightAngle)
        {
            rigidbody2D.angularVelocity = -1*speed;
        }
        if (transform.rotation.z < leftAngle)
        {
            rigidbody2D.angularVelocity = speed;
        }
    }

}

    
