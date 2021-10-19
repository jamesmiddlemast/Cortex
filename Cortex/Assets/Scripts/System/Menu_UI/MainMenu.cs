using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject canvasObject;
    private void Start(){
        canvasObject = GameObject.FindGameObjectsWithTag("SettingsCanvas")[0];
    }
    public void StartButton(){
        SceneManager.LoadScene("Tutorial");
    }

    public void ExitButton(){
        Application.Quit();
    }

    public void LoadSettings(){
        canvasObject.GetComponent<Canvas>().enabled = true;
    }

    public void HideSettings(){

    }

}
