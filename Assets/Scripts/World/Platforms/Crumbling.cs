using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Crumbling : MonoBehaviour
{
    [SerializeField] private float crumbleTimeSeconds;
    private float timer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("StartTimer", crumbleTimeSeconds);
        }
    }

    private IEnumerator StartTimer(float totalTime)
    {
        timer = 0f;
        while (timer < totalTime)
        {
            yield return new WaitForSecondsRealtime(1);
            timer++;
        }
        Invoke("crumblePlatform", 0f);
    }
    private void crumblePlatform()
    {
        WorldManager.Instance.addToCrumbledPlatformList(gameObject);
        gameObject.SetActive(false);
    }
}