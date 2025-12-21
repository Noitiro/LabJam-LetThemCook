using UnityEngine;
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
        Debug.Log("Looking for " + name);
        foreach(GameObject x in SoundSources)
        {
            Debug.Log("Is " + name + " same as " + x.name);
            if (x.name == name)
            {
                Debug.Log("Play coins");
                AudioSource source = x.GetComponent<AudioSource>();
                source.Play(0);
            }
        }
    }
}
