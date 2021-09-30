using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //Health/Jumps
        public static int health_jumps = 3;
    //Integrity of the Memory
        public static float integrity = 0.0f;
        [SerializeField]
        float max_integrity;

    //Reference PlayerFieldOfView fovScript
        public PlayerFieldOfView FOVScript;


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
        currentBody = GameObject.FindGameObjectsWithTag("PlayerBody")[0];
        //Jump(initialBody);
        //Set the new body's Mask as the Player's Mask (for enemy targeting)
        currentBody.layer = 8;

        //Set the new body's Tag as the Player
        currentBody.tag = "PlayerBody";

        //Setup Enemy's Field of view to original value
        //FieldOfView.radius = 7f;

        //Reset Integrity
        integrity = 0.0f;

        //Reset Jumps;
        health_jumps = 3;

        //FOV Script
        FOVScript = this.gameObject.GetComponent<PlayerFieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check Pause/Unpause
        if (Input.GetKeyDown(KeyCode.P)){
            Pause();
        }

        //Only get player input and update if game is not paused

        if (!UIController.game_paused){
            //Check for player input
            if (Input.anyKey){
                //Check for Ctrl press (Crouch)
                if (Input.GetKeyDown(KeyCode.LeftControl)){
                    Crouch();
                }
                //Check for Space press (BodyJump)
                else if (Input.GetKeyDown(KeyCode.Space)){
                    //Check health/Jumps left
                    if (health_jumps > 0){
                        // Identify Target, Check if a target is visible and jump to them
                        if (FOVScript.canSeeTarget == true){
                            targetBody = PlayerFieldOfView.targetBody;
                            //Jump to target
                            health_jumps -= 1;
                            Jump(targetBody);
                            targetBody.gameObject.GetComponent<EnemyController>().Disappear();
                        }
                    }
                }
                else if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D))){
                    Move();
                }
            }

            //Check Whether Integrity has been breached
            if (integrity > max_integrity){
                SceneManager.LoadScene("EndMenu");
            }
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
            //Also set enemie's view radius back to normal.
            //FieldOfView.radius = 7f;
        } else {
            //If not, crouch.
            isCrouched = true;
            moveSpeed = crouchSpeed;
            //Shrink Enemy's view radius.
            //FieldOfView.radius = 4f;
        }
    }

    void Jump(GameObject body){
        //Move to the new body
        currentBody.transform.position = body.transform.position;

        //Move to the floor
        //Vector3 dis = currentBody.transform.position;
        //dis.y = 0;
        //currentBody.transform.position = dis;

        //No longer Destroy the current body
        //Destroy(this.currentBody);

        //No longer replace the bodySet the new body as the current body
        //currentBody = body;
        //Set the new body's Mask as the Player's Mask (for enemy targeting)
        //body.layer = 8;

        //Set the new body's Tag as the Player
        //body.tag = "PlayerBody";

        //Reset Crouch
        if (isCrouched){
            Crouch();
        }

        //Need to set the player's rotation aligned with the new body's rotation.
        currentBody.transform.rotation = body.transform.rotation;

        //Need to disable the new body's fieldofview script.
        //FieldOfView fovScript = body.GetComponent<FieldOfView>();
        //if (fovScript != null){
        //    Destroy(fovScript);
        //}

        //Need to destroy the enemy body
        FOVScript.PlayerJumping();
        //Destroy(body);
        //body.transform.position.y = -100;
    }

    void Pause(){
        UIController.game_paused = !UIController.game_paused;
    }
}
