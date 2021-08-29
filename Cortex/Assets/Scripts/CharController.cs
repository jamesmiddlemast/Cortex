using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    [SerializeField]
    //Walk and crouch speeds.
    float walkSpeed = 5f;
    float crouchSpeed = 2f;

    //Start at walk speed
    float moveSpeed = 4f;

    //Flag for whether the player is crouched.3
    bool isCrouched = false;

    //Vectors for forward and right movements.
    Vector3 forward, right;

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
    }

    //Main Movement Function
    void Move(){
        //Set Movement per direction, based off moveSpeed, time, and the input weights/axii
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
        Vector3 forwardMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");

        //Make the player face forwards
        Vector3 heading = Vector3.Normalize(rightMovement + forwardMovement);
        transform.forward = heading;

        //Move the player
        transform.position += rightMovement;
        transform.position += forwardMovement;
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
}
