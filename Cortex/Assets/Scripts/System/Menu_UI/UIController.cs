using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    //Identify UI Components
    public GameObject health_jumps;
    public GameObject integrity;
    Text health_jumps_text;
    Text integrity_text;

    public GameObject clues_found;
    Text clues_found_text;

    int current_clues_found;
    public int total_clues;

    // For Pausing/Resuming the game.
    public static bool game_paused;
    //For Debug
    public bool visable_pause;

    //For Objectives
    public static bool all_clues_found = false;

    //Cig image
    public GameObject CigImage;
    public Texture Cig_Full;
    public Texture Cig_Four;
    public Texture Cig_Mostly;
    public Texture Cig_Two;
    public Texture Cig_Slightly;
    public Texture Cig_Empty;

    public GameObject resetText;
    public GameObject resetText2;

    public GameObject tutorialPopup;

    // Start is called before the first frame update
    void Start()
    {
        //Indentify Components
        health_jumps_text = health_jumps.GetComponent<Text>();
        integrity_text = integrity.GetComponent<Text>();
        clues_found_text = clues_found.GetComponent<Text>();
        game_paused = false;
        current_clues_found = 0;
        all_clues_found = false;
        health_jumps_text.text = "";
        integrity_text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (all_clues_found){
            clues_found_text.text = "Find the Exit";
        } else{
            clues_found_text.text = "Find the Clues: " + current_clues_found + " of " + total_clues;
        }

        visable_pause = game_paused;
    }

    public void ClueFound(){
        current_clues_found ++;
        //Change Objective if all Clues found
        if (current_clues_found == total_clues){
            all_clues_found = true;
        }
    }
    public static void SettingsMenu(bool displaySettings){
        GameObject canvasObject = GameObject.FindGameObjectsWithTag("SettingsCanvas")[0];
        canvasObject.GetComponent<Canvas>().enabled = displaySettings;
    }

    public void UpdateCig(int jumps_left){
        RawImage CigImageImage = CigImage.GetComponent<RawImage>();
        if (SceneManager.GetActiveScene().name == "Level 2"){
            if (jumps_left == 5){
                CigImageImage.texture = Cig_Full;
            } else if (jumps_left == 4){
                CigImageImage.texture = Cig_Four;
            } else if (jumps_left == 3){
                CigImageImage.texture = Cig_Mostly;
            } else if (jumps_left == 2){
                CigImageImage.texture = Cig_Two;
            } else if (jumps_left == 1){
                CigImageImage.texture = Cig_Slightly;
            } else if (jumps_left == 0){
                CigImageImage.texture = Cig_Empty;
                //Displau [R]eset? Text
                resetText.GetComponent<Text>().enabled = true;
                resetText2.GetComponent<Text>().enabled = true;
            }
        } else {
            if (jumps_left == 3){
                CigImageImage.texture = Cig_Full;
            } else if (jumps_left == 2){
                CigImageImage.texture = Cig_Mostly;
            } else if (jumps_left == 1){
                CigImageImage.texture = Cig_Slightly;
            } else if (jumps_left == 0){
                CigImageImage.texture = Cig_Empty;
                //Displau [R]eset? Text
                resetText.GetComponent<Text>().enabled = true;
                resetText2.GetComponent<Text>().enabled = true;
            }
        }
    }

    public void ShowTutorialImage(){
        game_paused = true;
        tutorialPopup.SetActive(true);
    }

    public void HideTutorialImage(){
        game_paused = false;
        tutorialPopup.SetActive(false);
    }
}
