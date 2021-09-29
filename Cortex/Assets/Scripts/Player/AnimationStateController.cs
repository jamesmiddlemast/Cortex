using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // all variables set to each key, "isWalking" is Boolean used by unity to see if animation transition conditions are true or false, this can be found in the animation contrller settings in unity
        bool isWalking = animator.GetBool("isWalking");
        bool wPressed = Input.GetKey("w");
        bool sPressed = Input.GetKey("s");
        bool aPressed = Input.GetKey("a");
        bool dPressed = Input.GetKey("d");
        // if player presses w key
        if (!isWalking && wPressed | sPressed | aPressed | dPressed)
        {
            // then set is walking = true
            animator.SetBool("isWalking", true);
        }

        // if player is not pressing w
        if (isWalking && !wPressed & !sPressed & !aPressed & !dPressed)
        {
            // set the isWalking boolean to be false
            animator.SetBool("isWalking", false);


        }

        // If game is paused, set disable walking animation
        if (UIController.game_paused)
        {
            // set the isWalking boolean to be false
            animator.SetBool("isWalking", false);
        }
    }
}
