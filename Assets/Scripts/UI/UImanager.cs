using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;
using UnityEngine.Audio;

public class UImanager : MonoBehaviour
{
    public static UImanager Instance; // reference to UIManager --> UImanager.Instance
    [SerializeField] private TMP_Text keyText;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject lossMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private AudioMixer masterVolume;

    private float originalVolume;
    void Start()
    {
        masterVolume.GetFloat("exposedMasterAudio", out originalVolume);
        Instance = this;
    }

    void LateUpdate()
    {
        if ((Input.GetKeyUp(KeyCode.Joystick3Button7) || Input.GetKeyUp(KeyCode.Escape)) && Time.timeScale == 1f)
        {
            Pause();
        }
        else if ((Input.GetKeyUp(KeyCode.Joystick3Button7) || Input.GetKeyUp(KeyCode.Escape)) && Time.timeScale == 0f)
        {
            unPauseOnClick();
        }
    }

    private void OnApplicationPause(bool pauseStatus) {
        if(pauseStatus == true){
            Pause();
            masterVolume.SetFloat("exposedMasterAudio", -80f);
        }        
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        if(pauseMenu != null)
        {
            pauseMenu.SetActive(true);
            masterVolume.SetFloat("exposedMasterAudio", -80f);

        }
    }

    public void unPauseOnClick()
    {
        Time.timeScale = 1f;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
            masterVolume.SetFloat("exposedMasterAudio", originalVolume);
        }

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
        masterVolume.SetFloat("exposedMasterAudio", originalVolume);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void goToMainMenu()
    {
        Time.timeScale = 1f; // Ensure the time scale is reset before loading the main menu
        saveManager.Instance.SaveGame(); //Save game before going to the main menu
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
    if (saveManager.Instance.doesSaveDataExist())
    {
        Debug.Log("Continuing game...");
        saveManager.Instance.LoadGame();
    }
    else
    {
        Debug.Log("No save data found, starting new game.");
        startNewGame();
    }
}

    public void startNewGame() 
    {
        Time.timeScale = 1f; // Ensure the time scale is reset before starting a new game
        SceneManager.LoadScene("TutorialLevel");
        // SceneManager.LoadScene("Tutorial");
    }



}