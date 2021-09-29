using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void RetryButton(){
        SceneManager.LoadScene("Demo 1");
    }

    public void ExitButton(){
        Application.Quit();
    }
}
