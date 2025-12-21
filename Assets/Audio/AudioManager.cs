using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public List<GameObject> SoundSources = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string name)
    {
        foreach(GameObject x in SoundSources)
        {
            if (x.name == name)
            {
                AudioSource source = x.GetComponent<AudioSource>();
                source.Play(0);
            }
        }
    }

    public void FadeOut(string name)
    {
        foreach (GameObject x in SoundSources)
        {
            if (x.name == name)
            {
                AudioSource source = x.GetComponent<AudioSource>();
                StartCoroutine(AudioFadeOut.FadeOut(source, 0.8f));
            }
        }
    }
}

public static class AudioFadeOut
{

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}