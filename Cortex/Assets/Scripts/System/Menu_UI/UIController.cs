using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        //Indentify Components
        health_jumps_text = health_jumps.GetComponent<Text>();
        integrity_text = integrity.GetComponent<Text>();
        clues_found_text = clues_found.GetComponent<Text>();
        game_paused = false;
        current_clues_found = 0;
    }

    // Update is called once per frame
    void Update()
    {
        health_jumps_text.text = "Health/Jumps: " + CharController.health_jumps;
        integrity_text.text = "Integrity: " + CharController.integrity;
        clues_found_text.text = "Clues Found: " + current_clues_found + " of " + total_clues;

        visable_pause = game_paused;
    }

    public void ClueFound(){
        current_clues_found ++;
    }
}