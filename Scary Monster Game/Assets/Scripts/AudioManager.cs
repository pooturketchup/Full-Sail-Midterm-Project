using UnityEngine.Audio;
using System;
using UnityEngine;
//Source - Brackeys - Introduction to AUDIO in Unity
//https://www.youtube.com/watch?v=6OT43pvUyfY&ab_channel=Brackeys
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixer GameAudio;
    public static AudioManager instance;
    private void Awake()
    {
        //so we can continue audio from scene to scene
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = GameAudio.FindMatchingGroups("Master")[0];
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    public void Play (string name)
    {
       Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound:" + name + " not found!");
            return;
        }
        s.source.Play();
    }
}
    