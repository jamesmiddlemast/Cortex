using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public GameObject ExitReasonText;
    void Start(){
        //Update Text based on reason for Exit
        string exitReason = PlayerPrefs.GetString("ExitReason","Default");
        if (exitReason == "Caught"){
            ExitReasonText.GetComponent<Text>().text = "You Were Caught!";
        } else if (exitReason == "Reset"){
            ExitReasonText.GetComponent<Text>().text = "Level Reset - Try Again?";
        }
    }
    public void RetryButton(){
        //Load Scene name
        string RetryLevel = PlayerPrefs.GetString("CurrentLevel");
        SceneManager.LoadScene(RetryLevel);
    }

    public void ExitButton(){
        Application.Quit();
    }
}
