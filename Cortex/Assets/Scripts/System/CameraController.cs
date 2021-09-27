using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    //cameraOffset is the camera's position relative to the player.
    Vector3 cameraOffset = new Vector3(-6,5,-6);

    //Identify the player, and their position.
    public GameObject playerObject;
    Vector3 playerPos;

    //Variables for determining when the player is hidden behind a wall
    //The current wall
    private List<TransparentWall> currentTransparentWalls = new List<TransparentWall>();

    //Hitting multiple objects
    RaycastHit[] hits;

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

    //For determining if a wall is between the camera and the player
    private void FixedUpdate()
    {
        //Remove all transparencies at start of update, clear list of transparent walls, then redo transparencies.
        //If there are transparent walls.
        if (currentTransparentWalls.Count > 1){
            for (int i = 0; i < currentTransparentWalls.Count; i++){
                currentTransparentWalls[0].ChangeTransparency(false);
                currentTransparentWalls.Remove(currentTransparentWalls[0]);
            }
        }

        //Calculate the Vector direction 
        Vector3 direction = playerObject.transform.position - transform.position;
        //Calculate the length
        float length = Vector3.Distance(playerObject.transform.position, transform.position);
        //Draw the ray in the debug
        Debug.DrawRay(transform.position, direction * length, Color.red);
        //The first object hit reference
        //RaycastHit currentHit;

        //Cast the ray and report the firt object hit filtering by "Wall" layer mask
        hits = Physics.RaycastAll(transform.position, direction, length, LayerMask.GetMask("ObstructionMask"));
        if (hits.Length > 0)
        {
            for (int i = 0; i<hits.Length; i++){

                //Getting the script to change transparency of the hit object
                TransparentWall transparentWall = hits[i].transform.GetComponent<TransparentWall>();
                //If the object is not null
                if (transparentWall)
                {
                    //Change the object transparency in transparent.
                    transparentWall.ChangeTransparency(true);
                    currentTransparentWalls.Add(transparentWall);
                }
            }        
        }
    }
}
