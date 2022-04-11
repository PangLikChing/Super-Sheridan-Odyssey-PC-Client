using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    private AudioSource defaultPlayer;
    private Dictionary<string, AudioClip> audioClips;
    private Dictionary<string, AudioSource> audioPlayers;

    private void Start()
    {
        defaultPlayer = GetComponent<AudioSource>();
    }

    public void AddAudioClip(string clipName, AudioClip clip)
    {
        audioClips.Add(clipName, clip);
    }

    public void AddAudioPlayer(string clipName, AudioSource Source)
    {
        audioPlayers.Add(clipName, Source);
    }

    public void RemoveAudioClip(string clipName)
    {
        audioClips.Remove(clipName);
    }

    public void RemoveAudioPlayer(string playerName)
    {
        audioPlayers.Remove(playerName);
    }

    public void PlayClip(string clipName, string playerName = null)
    {
        AudioSource player;
        if (playerName != null)
        {
            player = audioPlayers[playerName];
        }
        else
        {
            player = defaultPlayer;
        }
        AudioClip clip = audioClips[clipName];
        player.PlayOneShot(clip);
    }

    public void PlayClip(AudioClip clip, string playerName = null)
    {
        AudioSource player;
        if (playerName != null)
        {
            player = audioPlayers[playerName];
        }
        else
        {
            player = defaultPlayer;
        }
        player.PlayOneShot(clip);
    }
}
