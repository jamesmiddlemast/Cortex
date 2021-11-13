using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    float typeWriterDelay;
    float currentTypeWriterDelay;
    bool startClicked;

    public GameObject SettingsObject;
    SettingsScript settingsscriptcomponent;

    bool musicPlaying;
    public AudioClip titleMusic;

    public GameObject canvasObject;
    private void Start(){
        canvasObject = GameObject.FindGameObjectsWithTag("SettingsCanvas")[0];
        typeWriterDelay = 2f;
        currentTypeWriterDelay = typeWriterDelay;
        startClicked = false;
        settingsscriptcomponent = SettingsObject.GetComponent<SettingsScript>();
    }
    public void StartButton(){
        startClicked = true;
        settingsscriptcomponent.PlayTypeWriter();
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
