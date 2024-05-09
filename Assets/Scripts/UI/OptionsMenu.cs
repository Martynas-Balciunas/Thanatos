using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void setMasterVolume(float volume)
    {
        audioMixer.SetFloat("exposedMasterAudio", volume);
    }
    public void setMusicVolume(float volume)
    {
        audioMixer.SetFloat("exposedMusicAudio", volume);
    }

    public void setQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void setFullscreen(bool isFullscreen) 
    {
        Screen.fullScreen=isFullscreen; 
    }
}
