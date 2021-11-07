using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraButton : MonoBehaviour
{
    public GameObject[] ControlledCameras;
    bool hasSwitched;
    // Start is called before the first frame update
    void Start()
    {
        hasSwitched = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasSwitched){
            if(other.tag == "Player"){
                hasSwitched = true;
                for(int i = 0; i < ControlledCameras.Length; i++){
                    ControlledCameras[i].GetComponent<SecurityCamera>().Deactivate();
                }
            }
        }
    }
}