using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlaySound(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null){
            Debug.LogWarning("Sound: " + name + " doesn't exist");
            return;
        }
        s.source.Play();
    }

    public void StopSound(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
