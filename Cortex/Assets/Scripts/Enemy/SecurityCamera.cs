using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public GameObject SecurityCameraBody;

    public string cameraState;
    //States = stationary, turning, deactivating, off;

    public float StationaryTime;
    float currentStationaryTime;
    public float TurnSpeed;
    public float TurnTime;
    float currentTurnTime;

    //Positions to Turn to
    int currentWaypoint = 0;
    public GameObject[] CameraTargets;


    public float DeactivateTime;
    float currentDeactivateTime;
    public GameObject DeactivateTarget;
    public GameObject Spotlight;

    public Light PowerLight;
    public Color PowerLightGreen;
    public Color PowerLightRed;

    // Start is called before the first frame update
    void Start()
    {
        cameraState = "Stationary";
        currentStationaryTime = StationaryTime;

        currentDeactivateTime = DeactivateTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Only update if game not paused
        if (!UIController.game_paused){
            switch (cameraState){
                case "Stationary" :
                Stationary();
                break;
                case "Turning" :
                Turn();
                break;
                case "Deactivating" : 
                PowerDown();
                break;
                case "Off" :
                Off();
                break;
            }
        }
    }

    //Stay still for a period of time, watching for the player
    void Stationary(){
        Watch();
        if (currentStationaryTime > 0){
            currentStationaryTime -= Time.deltaTime;
        } else {
            if (CameraTargets.Length == 0){
                currentStationaryTime = StationaryTime;
            } else {
                //Turn to next Waypoint
                int nextWaypoint = currentWaypoint + 1;
                if (nextWaypoint < CameraTargets.Length){
                    //If more CameraTargets, move to the next
                    currentWaypoint ++;
                }
                else { //If at the end, move back
                    currentWaypoint = 0;
                }
                cameraState = "Turning";
                currentTurnTime = TurnTime;
            }
        }
    }

    //Turn to next position, watching for the player
    void Turn(){
        Watch();
        //Turn to face new position
        //Reduce Cooldown
        currentTurnTime -= Time.deltaTime; 
        //Rotate towards next waypoint.
        Quaternion targetRotation = Quaternion.LookRotation(CameraTargets[currentWaypoint].transform.position - SecurityCameraBody.transform.position);
        float turnAmount = Mathf.Min (TurnSpeed * Time.deltaTime, 1);
        SecurityCameraBody.transform.rotation = Quaternion.Lerp (SecurityCameraBody.transform.rotation, targetRotation, turnAmount);
    
        //At the end of the face cooldown, stay stationary.
        if (currentTurnTime <= 0){
            //SecurityCameraBody.transform.rotation = targetRotation;
            cameraState = "Stationary";
            currentStationaryTime = StationaryTime;
        }
    }

    //Button calls Deactivate
    public void Deactivate(){
        cameraState = "Deactivating";
    }

    //Turn to face deactivate position
    void PowerDown(){
        //Reduce Cooldown
        currentDeactivateTime -= Time.deltaTime; 
        //Rotate towards next waypoint.
        Quaternion targetRotation = Quaternion.LookRotation(DeactivateTarget.transform.position - SecurityCameraBody.transform.position);
        float turnAmount = Mathf.Min (TurnSpeed * Time.deltaTime, 1);
        SecurityCameraBody.transform.rotation = Quaternion.Lerp (SecurityCameraBody.transform.rotation, targetRotation, turnAmount);
    
        //At the end of the face cooldown, stay stationary.
        if (currentDeactivateTime <= 0){
            cameraState = "Off";
            //Turn off light and remove field of view script.
            Spotlight.SetActive(false);
            SecurityCameraBody.GetComponent<FieldOfView>().radius = 0;
            PowerLight.color = PowerLightRed;
        }
    }

    //Do nothing
    void Off(){

    }

    //Do Field of View stuff for the player.
    void Watch(){

    }
}
