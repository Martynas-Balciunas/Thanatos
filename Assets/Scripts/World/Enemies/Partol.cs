using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partol : MonoBehaviour
{
    public GameObject PatrolPointA;
    public GameObject PatrolPointB;
    [SerializeField] private float speed;
    private Rigidbody2D rigidBody;
    private Transform currentPoint;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        
        currentPoint = PatrolPointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == PatrolPointB.transform)
        {
            rigidBody.velocity = new Vector2(speed, 0);
        }
        else
        {
            rigidBody.velocity = new Vector2(-speed, 0);
        }
        

        if(Vector2.Distance(transform.position, currentPoint.position )< 0.5f && currentPoint == PatrolPointB.transform)
        {
            currentPoint = PatrolPointA.transform;
        }
        if(Vector2.Distance(transform.position, currentPoint.position )< 0.5f && currentPoint == PatrolPointA.transform)
        {
            currentPoint = PatrolPointB.transform;
        }
    }


    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(PatrolPointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PatrolPointB.transform.position, 0.5f);
        Gizmos.DrawLine(PatrolPointA.transform.position, PatrolPointB.transform.position);
    }
}
