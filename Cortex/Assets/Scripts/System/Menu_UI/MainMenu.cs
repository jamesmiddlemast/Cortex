using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    GameObject soundGameObject;
    AudioSource audioSource;
    public AudioClip typeWriter;
    float typeWriterDelay;
    float currentTypeWriterDelay;
    bool startClicked;

    public GameObject canvasObject;
    private void Start(){
        canvasObject = GameObject.FindGameObjectsWithTag("SettingsCanvas")[0];
        soundGameObject = new GameObject("Sound");
        audioSource = soundGameObject.AddComponent<AudioSource>();
        typeWriterDelay = 1f;
        currentTypeWriterDelay = typeWriterDelay;
        startClicked = false;
    }
    public void StartButton(){
        audioSource.PlayOneShot(typeWriter);
        startClicked = true;
    }

    public void ExitButton(){
        Application.Quit();
    }

    public void LoadSettings(){
        canvasObject.GetComponent<Canvas>().enabled = true;
    }

    public void HideSettings(){

    }

    void Update(){
        if (startClicked){
            currentTypeWriterDelay -= Time.deltaTime;
            if (currentTypeWriterDelay <= 0){
                SceneManager.LoadScene("Tutorial");
            }
        }
    }

}
