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

    // Start is called before the first frame update
    void Start()
    {
        //Indentify Components
        health_jumps_text = health_jumps.GetComponent<Text>();
        integrity_text = integrity.GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        health_jumps_text.text = "Health/Jumps: " + CharController.health_jumps;
        integrity_text.text = "Integrity: " + CharController.integrity;
    }
}
