using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtSound : MonoBehaviour
{

    GameObject soundGameObject;
    AudioSource audioSource;
    public AudioClip caughtclip;
    bool hasplayed;
    // Start is called before the first frame update
    void Start()
    {
        hasplayed = false;
        soundGameObject = new GameObject("Sound");
        audioSource = soundGameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasplayed){
            hasplayed = true;
            audioSource.PlayOneShot(caughtclip);
        }
        
    }
}
