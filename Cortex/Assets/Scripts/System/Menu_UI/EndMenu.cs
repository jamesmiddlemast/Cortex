using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void RetryButton(){
        //Load Scene name
        string RetryLevel = PlayerPrefs.GetString("CurrentLevel");
        SceneManager.LoadScene(RetryLevel);
    }

    public void ExitButton(){
        Application.Quit();
    }
}
