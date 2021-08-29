using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    [SerializeField]
    //Movement
        //Walk and crouch speeds.
        float walkSpeed = 5f;
        float crouchSpeed = 2f;
        //Start at walk speed
        float moveSpeed = 4f;
        //Vectors for forward and right movements.
        Vector3 forward, right;

    //Crouching
        //Flag for whether the player is crouched.3
        bool isCrouched = false;

    //BodyJumping
        //Define the player's current body
        GameObject currentBody;   
        //Define the current jump target
        GameObject targetBody;

    // Start is called before the first frame update
    void Start()
    {
        //Set CharController's transformation to align with the Orthagonal Camera
        forward = Camera.main.transform.forward;

        //Normalise
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        //Set Right direction
        right = Quaternion.Euler(new Vector3(0,90,0)) * forward;

        //Identify and Jump into the initial detective's body
        GameObject initialBody = GameObject.FindGameObjectsWithTag("InitialPlayerBody")[0];
        Jump(initialBody);
    }

    // Update is called once per frame
    void Update()
    {
        //Check for player input
        if (Input.anyKey){
            Move();
        }

        //Check for Ctrl press (Crouch)
        if (Input.GetKey(KeyCode.LeftControl)){
            Crouch();
        }

        //Check for Space press (BodyJump)
        if (Input.GetKey(KeyCode.Space)){
            // ----- TODO -----
            // Adjust this to allow targeting a specific enemy rather than just grabing the first tagged object
            // Identify Target
            targetBody = GameObject.FindGameObjectsWithTag("Enemy")[0];
            //Jump to target
            Jump(targetBody);
        }
    }

    //Main Movement Function
    void Move(){
        //Set Movement per direction, based off moveSpeed, time, and the input weights/axii
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
        Vector3 forwardMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");

        //Make the player's body face forwards
        Vector3 heading = Vector3.Normalize(rightMovement + forwardMovement);
        currentBody.transform.forward = heading;

        //Move the player's body
        currentBody.transform.position += rightMovement;
        currentBody.transform.position += forwardMovement;
    }

    //Toggle Crouch Function
    void Crouch(){
        //Check whether already crouching
        if (isCrouched){
            //If so, stand up and retain walk speed
            isCrouched = false;
            moveSpeed = walkSpeed;
        } else {
            //If not, crouch.
            isCrouched = true;
            moveSpeed = crouchSpeed;
        }
    }

    void Jump(GameObject body){
        //Move to the new body
        transform.position = body.transform.position;
        transform.parent = body.transform;

        //Set the new body as the current body
        currentBody = body;
    }
}
