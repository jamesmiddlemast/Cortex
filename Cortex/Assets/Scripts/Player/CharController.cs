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
        float max_integrity = 0.15f;

    //Reference PlayerFieldOfView fovScript
        public PlayerFieldOfView FOVScript;

    //For Footsteps
        public float footstepDelay;
        float currentFootstepDelay;
        GameObject soundGameObject;
        AudioSource audioSource;
        public float playerVolume;
        public AudioClip footstep;
    //Jump Sound
        public AudioClip jumpsound;
    //Error Jump
        public AudioClip errorjumpsound;

    //AmbientMusic
        AudioSource musicAudioSource;
        public float musicVolume;
        public AudioClip ambientMusic;

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
        if (SceneManager.GetActiveScene().name == "Level 2"){
            health_jumps = 5;
        }

        //FOV Script
        FOVScript = this.gameObject.GetComponent<PlayerFieldOfView>();

        //Footsteps
        currentFootstepDelay = footstepDelay;
        soundGameObject = new GameObject("Sound");
        audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.volume = playerVolume;


        //Play ambient music
        musicAudioSource = soundGameObject.AddComponent<AudioSource>();
        musicAudioSource.volume = musicVolume;
        musicAudioSource.PlayOneShot(ambientMusic);

        GameObject canvasObject = GameObject.FindGameObjectsWithTag("SettingsCanvas")[0];
        canvasObject.GetComponent<SettingsScript>().SetVolumeLevels();


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
                        } else {
                            //Play Error noise.
                            audioSource.PlayOneShot(errorjumpsound);
                        }
                    } else {
                        //Play Jump Error Noise.
                        audioSource.PlayOneShot(errorjumpsound);
                    }
                }
                else if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D))){
                    Move();
                    PlayFootstepSound();
                } 
            } else if (currentFootstepDelay != footstepDelay){ //If no input, check whether footsteps just finished
                ResetFootstepCountdown();
            }

            //Check Whether Integrity has been breached
            if (integrity > max_integrity){
                ExitLevel("Caught");
            }

            //Check for R key
            if (Input.GetKeyDown(KeyCode.R)){
                ExitLevel("Reset");
            }
        }
    }

    void ExitLevel(string exitReason){
        //Save current scene
        Scene scene = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("CurrentLevel",scene.name);
        PlayerPrefs.SetString("ExitReason",exitReason);
        
        SceneManager.LoadScene("EndMenu");
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

        //Play jump sound
        audioSource.PlayOneShot(jumpsound);

        //Create AfterImage at current location
        AfterImage(body);

        //Move to the new body
        currentBody.transform.position = body.transform.position;


        //Reset Crouch
        if (isCrouched){
            Crouch();
        }

        //Need to set the player's rotation aligned with the new body's rotation.
        currentBody.transform.rotation = body.transform.rotation;

        //Need to destroy the enemy body
        FOVScript.PlayerJumping();

        //Update Cigaret
        GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>().UpdateCig(health_jumps);//  UIController.UpdateCig(health_jumps);

    }

    void AfterImage(GameObject body){
        //Identify the AfterImage
        GameObject afterimage_source = GameObject.FindGameObjectWithTag("AfterImage_Effect_Source");
        GameObject afterimage_destination = GameObject.FindGameObjectWithTag("AfterImage_Effect_Destination");

        //Clone the afterimage at the source and destination
        Vector3 source_pos = currentBody.transform.position;
        Quaternion source_rotation = Quaternion.Euler(-90, 0, 0);
        Vector3 destination_pos = body.transform.position;
        Quaternion destination_rotation = Quaternion.Euler(-90, 0, 0);

        //Create the particle effect
        GameObject afterimage_new_source = Instantiate(afterimage_source, source_pos, source_rotation);
        GameObject afterimage_new_destination = Instantiate(afterimage_destination, destination_pos, destination_rotation);


        //afterimage_new_source.SetParent(null);
        //afterimage_new_destination.SetParent(null);
    }

    void Pause(){
        UIController.game_paused = !UIController.game_paused;
        UIController.SettingsMenu(UIController.game_paused);
    }

    //Plays footsteps on defined interval
    void PlayFootstepSound(){
        //Decrement cooldown
        currentFootstepDelay = currentFootstepDelay - Time.deltaTime;
        //If cooldown <= 0
        if (currentFootstepDelay <= 0){
            //Reset cooldown and play sound
            currentFootstepDelay = footstepDelay;
            audioSource.PlayOneShot(footstep);
        }
    }

    //Resets footstep cooldown
    void ResetFootstepCountdown(){
        //Reset footstepdelay
        currentFootstepDelay = footstepDelay;
        //Take a final step
        audioSource.PlayOneShot(footstep);
    }

    public void setMusicVolume(float newvolume){
        if (musicAudioSource != null){
            musicAudioSource.volume = newvolume;
        }
    }

    public void setEffectsVolume(float newvolume){
        if (audioSource != null){
            audioSource.volume = newvolume;
        }
    }
}
