using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private int health = 3;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private int keyCount = 0;
    private UImanager uiManager;
    private WorldManager worldManager;

    void Start()
    {
        uiManager = UImanager.Instance;
        worldManager = WorldManager.Instance;
        Instance = this;
    }

    void Update()
    {
        
    }

    public void takeDamage()
    {
        // reduce health
        // play damage effect & sound (can be seperate function)
        uiManager.removeHeartUI();
        enterGhostForm();
    }

    private void gainHealth() // can be removed, or healing item added
    {
        if(health < maxHealth)
        {
            // increase health
            // play heal effect & sound
            uiManager.addHeartUI();
        }

    }

    public void death()
    {
        // Set health to 0
        // Play death effects & sound
        uiManager.removeAllHearts(); // ensures no hearts in case instant death
        // show loss menu
    }

    private void enterGhostForm()
    {
        // adjust values like double jump floaty jump ability to take dmg etc
        // can add effects & sound or can use ones in death
        worldManager.showGhostMap();
    }

    private void enterAliveForm()
    {
        // adjust values like double jump floaty jump ability to take dmg etc
        // can add effects & sound
        worldManager.hideGhostMap();
    }

    public void keyCollected()
    {
        keyCount++;
        uiManager.updateKeyUI(keyCount);
    }
    public void useKey()
    {
        if (!(keyCount > 0))
        {
            return;
        }
        keyCount--;
        uiManager.updateKeyUI(keyCount);
    }

}
