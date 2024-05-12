using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    public static UImanager Instance; // reference to UIManager --> UImanager.Instance
    [SerializeField] private TMP_Text keyText;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject lossMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject[] hearts;

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
      
    }

    private void OnApplicationPause(bool pauseStatus) {
        if(pauseStatus == true){
            Pause();
        }        
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        if(pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }
    }

    public void unPauseOnClick()
    {
        Time.timeScale = 1f;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

    }

    public void saveOnClick()
    {
        // to be implemented
    }

    public void exitOnClick()
    {
        Application.Quit();
    }

    public void updateKeyUI(int keyCount)
    {
       keyText.text = keyCount.ToString();
    }

    public void removeHeartUI() // call when damage taken
    {
        for(int i = hearts.Count() - 1; i >= 0; i--) // reverse through array
        {
            if (hearts[i].activeInHierarchy) // if heart is active
            {
                hearts[i].SetActive(false); // make it inactive
                // can add heart loss effect function call here
                break; // exit
            }
        }
    }

    public void addHeartUI() // call when healed
    {
        for (int i = 0; i < hearts.Count(); i ++) // check through array
        {
            if (!hearts[i].activeInHierarchy) // if heart is inactive
            {
                hearts[i].SetActive(true); // make it active
                // can add heart gain effect function call here
                break; // exit
            }
        }
    }

    public void removeAllHearts()
    {
        for (int i = hearts.Count() - 1; i >= 0; i--)
        {
            if (hearts[i].activeInHierarchy)
            {
                hearts[i].SetActive(false);
            }

        }
    }

    public void showLossMenu()
    {
        Time.timeScale = 0f;
        lossMenu.SetActive(true);
    }

    public void resetCurrentScene()
    {
        Time.timeScale = 1f; // Ensure the time scale is reset before reloading
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void goToMainMenu()
    {
        Time.timeScale = 1f; // Ensure the time scale is reset before loading the main menu
        SceneManager.LoadScene("MainMenu");
    }

    public void showSettingsMenu()
    {
        Time.timeScale = 0f;
        settingsMenu.SetActive(true);
    }

    public void hideSettingsMenu()
    {
        Time.timeScale = 1f;
        settingsMenu.SetActive(false);
    }

    public void continuePreviousGame()
    {
        if (!saveManager.Instance.doesSaveDataExist())
        {
            startNewGame();
        }
    }

    public void startNewGame() 
    {
        Time.timeScale = 1f; // Ensure the time scale is reset before starting a new game
        SceneManager.LoadScene("Level1");
        // SceneManager.LoadScene("Tutorial");
    }
}
