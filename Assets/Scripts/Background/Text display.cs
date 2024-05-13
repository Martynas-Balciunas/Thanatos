using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Textdisplay : MonoBehaviour
{
    [SerializeField] private GameObject textToOutput;
    [SerializeField] private GameObject textToDestroy;
    [SerializeField] private float x;
    [SerializeField] private float y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
     {
        Destroy(textToDestroy);
        Instantiate(textToOutput, new Vector3(x,y,0), Quaternion.identity);
        
    }
}
