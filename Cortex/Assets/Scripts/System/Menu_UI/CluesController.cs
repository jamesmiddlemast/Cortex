using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluesController : MonoBehaviour
{
    GameObject soundGameObject;
    AudioSource audioSource;
    public AudioClip CluePickup;

    public GameObject UIController;

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

    private void OnTriggerEnter(Collider other)
    {
        if((other.tag == "Player") || (other.tag == "PlayerBody")){
            UIController.GetComponent<UIController>().ClueFound();

            audioSource.PlayOneShot(CluePickup);
            other.GetComponent<CharController>().playCluePickup();
            this.gameObject.SetActive(false);
        }
    }
}
