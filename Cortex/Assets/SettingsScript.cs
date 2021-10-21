using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float masterVolume;
    public float effectsVolume;
    public float musicVolume;

    //List of Music/Sound components
    public GameObject playerObject;
    public CharController playerObjectScript;

    //Text Fields for Volume
    public GameObject MasterVolumeText;
    public GameObject EffectsVolumeText;
    public GameObject MusicVolumeText;

    void Start()
    {
        //Get Volume from PlayerPrefs (or set both master and effects/music to 5 if no playerPrefs)
        masterVolume = PlayerPrefs.GetFloat("MasterVolume",5f);
        effectsVolume = PlayerPrefs.GetFloat("EffectsVolume",5f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume",5f);

        //Identify Player
        //GameObject[] empty = new GameObject[];
        if (GameObject.FindGameObjectsWithTag("Player").Length > 0){
            playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
            playerObjectScript = playerObject.GetComponent<CharController>();
        } else {
            playerObject = null;
        }

        //Set initial Volumes for all components
        SetVolumeLevels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleFullscreen(){
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void CloseSettings(){
        //Unpause Game if paused
        if (UIController.game_paused){
            UIController.game_paused = false;
        }
        GameObject.FindGameObjectsWithTag("SettingsCanvas")[0].GetComponent<Canvas>().enabled = false;
    }

    public void QuitGame(){
        Application.Quit();
    }

    //Need movement and volume toggles
    public void ToggleControls(){
        //Bool for cardinalmovement
        //if cardinalmovement
        //ChangeMovement to orthagonal
        //else
        //Changemovement to cardinal
    }

    public void SetVolumeLevels(){
        float totalMusicVolume = 0;
        if ((masterVolume > 0)&& (musicVolume > 0)){
            totalMusicVolume = masterVolume + musicVolume;
            totalMusicVolume = totalMusicVolume/10;
        }
        
        float totalEffectsVolume = 0;
        if ((masterVolume > 0) && (effectsVolume > 0)){
            totalEffectsVolume = masterVolume + effectsVolume;
            totalEffectsVolume = totalEffectsVolume/10;
        }

        PlayerPrefs.SetFloat("MasterVolume",masterVolume);
        PlayerPrefs.SetFloat("EffectsVolume",effectsVolume);
        PlayerPrefs.SetFloat("MusicVolume",musicVolume);

        //Update Text
        MasterVolumeText.GetComponent<Text>().text = "Master: "+ masterVolume;
        MusicVolumeText.GetComponent<Text>().text = "Music: "+ musicVolume;
        EffectsVolumeText.GetComponent<Text>().text = "Effects: "+ effectsVolume;

        if (playerObject != null){
            playerObjectScript.setMusicVolume(totalMusicVolume);
            playerObjectScript.setEffectsVolume(totalEffectsVolume);
        }
    }

    public void ChangeMasterVolume(int amount){
        masterVolume += amount;
        if (masterVolume < 0){
            masterVolume = 0;
        } else if (masterVolume > 5){
            masterVolume = 5;
        }
        SetVolumeLevels();
    }
    
    public void ChangeEffectsVolume(int amount){
        effectsVolume += amount;
        if (effectsVolume < 0){
            effectsVolume = 0;
        } else if (effectsVolume > 5){
            effectsVolume = 5;
        }
        SetVolumeLevels();
    }

    public void ChangeMusicVolume(int amount){
        musicVolume += amount;
        if (musicVolume < 0){
            musicVolume = 0;
        } else if (musicVolume > 5){
            musicVolume = 5;
        }
        SetVolumeLevels();
    }
}
