using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float masterVolume;
    public float effectsVolume;
    public float musicVolume;

    //List of Music/Sound components
    public GameObject playerObject;
    public CharController playerObjectScript;

    void Start()
    {
        //Get Volume from PlayerPrefs (or set both master and effects/music to 5 if no playerPrefs)
        masterVolume = PlayerPrefs.GetFloat("MasterVolume",5f);
        effectsVolume = PlayerPrefs.GetFloat("EffectsVolume",5f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume",5f);

        //Identify Player
        playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
        playerObjectScript = playerObject.GetComponent<CharController>();

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
        playerObjectScript.setMusicVolume(totalMusicVolume);

        float totalEffectsVolume = 0;
        if ((masterVolume > 0) && (effectsVolume > 0)){
            totalEffectsVolume = masterVolume + effectsVolume;
            totalEffectsVolume = totalEffectsVolume/10;
        }
        playerObjectScript.setEffectsVolume(totalEffectsVolume);

        PlayerPrefs.SetFloat("MasterVolume",masterVolume);
        PlayerPrefs.SetFloat("EffectsVolume",effectsVolume);
        PlayerPrefs.SetFloat("MusicVolume",musicVolume);
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
