using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    //cameraOffset is the camera's position relative to the player.
    Vector3 cameraOffset = new Vector3(-6,5,-6);

    //Identify the player, and their position.
    GameObject playerObject;
    Vector3 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        //Identify the player
        playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        //Find the player's position, and move the camera relavent to them.
        playerPos = playerObject.transform.position;
        transform.position = playerPos + cameraOffset;
    }

    //Need to move the Camera to new player's object when body jumping
    /*
    void FindNewPlayer(){
        //Identify the player
        playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
        //Snap to new Player
        playerPos = playerObject.transform.position;
        transform.position = playerPos + cameraOffset;
    }
    */
}
