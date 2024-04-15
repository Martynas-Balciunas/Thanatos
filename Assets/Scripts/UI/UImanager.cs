using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour

{
    [SerializeField] private GameObject pauseMenu;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnApplicationPause(bool pauseStatus) {
        if(pauseStatus == true){
            pauseMenu.gameObject.setEnabled(false);
            
        }        
    }
    
}
