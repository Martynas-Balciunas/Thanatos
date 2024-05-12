using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partol : MonoBehaviour
{
    [SerializeField] private GameObject PatrolPointA;//limit coordinates to the left
    [SerializeField] private GameObject PatrolPointB;//limit coordinates to the right
    [SerializeField] private float speed;//speed of moving left and right
    private Rigidbody2D rigidBody;
    private Transform currentPoint;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        
        currentPoint = PatrolPointB.transform;//current point is a target of where pendulum should go to
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == PatrolPointB.transform)
        {
            rigidBody.velocity = new Vector2(speed, 0); //moves enemy to the right
        }
        else
        {
            rigidBody.velocity = new Vector2(-speed, 0); //moves enemy to the left
        }
        
        //if (object with the script is in radious of 0.5f to taget point "currentPoint" AND taget point "currentPoint" is same as PatrolPointB)
        if(Vector2.Distance(transform.position, currentPoint.position )< 0.5f && currentPoint == PatrolPointB.transform)
        {
            currentPoint = PatrolPointA.transform;// Sets target point "currentPoint" from PatrolPointB to PatrolPointA
        }
        //if (object with the script is in radious of 0.5f to taget point "currentPoint" AND taget point "currentPoint" is same as PatrolPointA)
        if(Vector2.Distance(transform.position, currentPoint.position )< 0.5f && currentPoint == PatrolPointA.transform)
        {
            currentPoint = PatrolPointB.transform;// Sets target point "currentPoint" from PatrolPointA to PatrolPointB
        }
    }

    //Drawing in editor for ease of use
    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(PatrolPointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PatrolPointB.transform.position, 0.5f);
        Gizmos.DrawLine(PatrolPointA.transform.position, PatrolPointB.transform.position);
    }
}
