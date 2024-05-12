using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour

{
    public static AudioManager Instance;
    [SerializeField] private AudioSource[] MusicPlaylist;
    void Start()
    {
        Instance = this;
        MusicPlaylist = GetComponents<AudioSource>();
        playMusic(MusicPlaylist[0]);
    }


    public void playMusic(AudioSource song)
    {
        song.loop = true;
        song.Play();

    }
    public void stopMusic()
    {
        foreach(AudioSource song in MusicPlaylist)
        {
            song.loop=false;
            song.Stop();
        }
    }

    public void changeSong(AudioSource song)
    {
        stopMusic();
        playMusic(song);
    }
}
