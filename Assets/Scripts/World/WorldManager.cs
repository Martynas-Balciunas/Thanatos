using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private GameObject ghostTilemap;
    [SerializeField] private GameObject[] ghostPlatforms; // current version needs each hidden platform to be added into the map
    public static WorldManager Instance;
    void Start()
    {
        Instance = this;
        hideGhostMap();
    }


    public void showGhostMap() // call upon becoming a ghost (WorldManager.Instance.showGhostMap();)
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
    public void hideGhostMap() // call upon becoming alive (WorldManager.Instance.hideGhostMap();)
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

}
