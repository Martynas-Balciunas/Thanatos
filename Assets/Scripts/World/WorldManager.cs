using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
public class WorldManager : MonoBehaviour
{
    [SerializeField] private GameObject ghostTilemap;
    [SerializeField] private GameObject[] ghostPlatforms; // current version needs each hidden platform to be added into the map
    private List<GameObject> crumblingPlatforms;
    [SerializeField] private int crumbleReapearTimeSeconds;
    private int timer;

    public static WorldManager Instance;
    void Start()
    {
        crumblingPlatforms = new List<GameObject>();
        Instance = this;
        hideGhostMap();
        InvokeRepeating("checkForCrumblingPlatforms", 0f, crumbleReapearTimeSeconds);
    }


    public void showGhostMap() // call upon becoming a ghost
    {
        ghostTilemap.SetActive(true);
        if (ghostPlatforms.Length > 0)
        {
            foreach (GameObject platform in ghostPlatforms) // could add special fx if have time
            {
                platform.SetActive(true);
            }
        }

    }
    public void hideGhostMap() // call upon becoming alive
    {

        ghostTilemap.SetActive(false); // could add special fx if have time

        if (ghostPlatforms.Count() > 0)
        {
            foreach (GameObject platform in ghostPlatforms) // could add special fx if have time
            {
                platform.SetActive(false);
            }
        }
    }

    public void addToCrumbledPlatformList(GameObject platform)
    {
        crumblingPlatforms.Add(platform);
    }

    private IEnumerator StartTimer(int totalTime, GameObject platform)
    {
        timer = 0;
        while (timer < totalTime)
        {
            yield return new WaitForSecondsRealtime(1);
            timer++;
        }
        unCrumblePlatform(platform);
    }
    private void unCrumblePlatform(GameObject platform)
    {        
        platform.SetActive(true);
        crumblingPlatforms.Remove(platform);

    }

    private void checkForCrumblingPlatforms()
    {
        if(crumblingPlatforms.Count() > 0)
        {
            foreach(GameObject platform in crumblingPlatforms)
            {
                StartCoroutine(StartTimer(crumbleReapearTimeSeconds, platform));
            }
        }
    }
}
