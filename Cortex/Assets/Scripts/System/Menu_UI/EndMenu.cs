using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void RetryButton(){
        SceneManager.LoadScene("Demo 2");
    }

    public void ExitButton(){
        Application.Quit();
    }
}
