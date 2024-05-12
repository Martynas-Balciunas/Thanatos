using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    private float ImgLength, StartPosX;

    public GameObject Cam;
    public float ParallaxSpeed;

    void Start()
    {
        StartPosX = transform.position.x;
        ImgLength = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float distance = (Cam.transform.position.x * ParallaxSpeed);
        float distanceRelCam = (Cam.transform.position.x * (1 - ParallaxSpeed));
        transform.position = new Vector3(StartPosX + distance, transform.position.y, transform.position.z);

        if (distanceRelCam > StartPosX + ImgLength) 
        {
            StartPosX += ImgLength;
        }
        else if (distanceRelCam < StartPosX - ImgLength)
        {
            StartPosX -= ImgLength;
        }
    }
}
