using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour

{
    public static AudioManager Instance;
    [SerializeField] private AudioSource[] MusicPlaylist;

    private int playingSongs = 0;
    private int songIndex = 0;
    void Start()
    {
        Instance = this;
        MusicPlaylist = GetComponents<AudioSource>();
        stopMusic();
        playMusic(MusicPlaylist[0]);
    }

    private void FixedUpdate()
    {
        foreach(AudioSource song in MusicPlaylist)
        {
            if (song.isPlaying)
            {
                playingSongs++;
                break;
            }
        }
        if (playingSongs == 0)
        {
            if(songIndex + 1 < MusicPlaylist.Length)
            {
                playMusic(MusicPlaylist[songIndex + 1]);
            }
            else
            {
                playMusic(MusicPlaylist[0]);
            }
        }
        playingSongs = 0;
    }
    public void playMusic(AudioSource song)
    {
        song.Play();
    }
    public void stopMusic()
    {
        foreach(AudioSource song in MusicPlaylist)
        {

            song.Stop();
        }
    }
    public void pauseMusic()
    {
        foreach (AudioSource song in MusicPlaylist)
        {
            song.Pause();
        }
    }
    public void unPauseMusic()
    {
        foreach (AudioSource song in MusicPlaylist)
        {
            song.UnPause();
        }
    }
    public void changeSong(AudioSource song)
    {
        stopMusic();
        playMusic(song);
    }
}
