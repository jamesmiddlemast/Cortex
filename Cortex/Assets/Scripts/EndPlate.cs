using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPlate : MonoBehaviour
{
    public string NextLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Trigger Victory on collision with Player
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            //Check if all clues are found
            if (UIController.all_clues_found == true){
                SceneManager.LoadScene(NextLevel);
            } else {
                //If not, request player finds clues
                Scene scene = SceneManager.GetActiveScene();
                PlayerPrefs.SetString("CurrentLevel",scene.name);
                SceneManager.LoadScene("CluesMissingScene");

            }
        }
    }
}
