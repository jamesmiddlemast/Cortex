using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    GameObject soundGameObject;
    AudioSource audioSource;
    public AudioClip footstep;

    public void PlayFootstepSound(){
        audioSource.PlayOneShot(footstep);
    }
    // Start is called before the first frame update
    void Start()
    {
        soundGameObject = new GameObject("Sound");
        audioSource = soundGameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
