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
    private TransparentWall currentTransparentWall;

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
        //Calculate the Vector direction 
        Vector3 direction = playerObject.transform.position - transform.position;
        //Calculate the length
        float length = Vector3.Distance(playerObject.transform.position, transform.position);
        //Draw the ray in the debug
        Debug.DrawRay(transform.position, direction * length, Color.red);
        //The first object hit reference
        RaycastHit currentHit;

        //Cast the ray and report the firt object hit filtering by "Wall" layer mask
        if (Physics.Raycast(transform.position, direction, out currentHit, length, LayerMask.GetMask("ObstructionMask")))
        {
            //Getting the script to change transparency of the hit object
            TransparentWall transparentWall = currentHit.transform.GetComponent<TransparentWall>();
            //If the object is not null
            if (transparentWall)
            {
                //If there is a previous wall hit and it's different from this one
                if (currentTransparentWall && currentTransparentWall.gameObject != transparentWall.gameObject)
                {
                    //Restore its transparency setting it not transparent
                    currentTransparentWall.ChangeTransparency(false);
                }
                //Change the object transparency in transparent.
                transparentWall.ChangeTransparency(true);
                currentTransparentWall = transparentWall;
            }            
        }
        else
        {
            //If nothing is hit and there is a previous object hit
            if (currentTransparentWall)
            {
                //Restore its transparency setting it not transparent
                currentTransparentWall.ChangeTransparency(false);
            }
        }
    }
}
