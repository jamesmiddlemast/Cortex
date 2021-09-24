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
        bool isWalking = animator.GetBool("isWalking");
        bool wPressed = Input.GetKey("w");
        bool sPressed = Input.GetKey("s");
        bool aPressed = Input.GetKey("a");
        bool dPressed = Input.GetKey("d");
        // if player presses w key
      if (!isWalking && wPressed) 
        {
            // then set is walking = true
            animator.SetBool("isWalking", true);
        } 
      // if player is not pressing w
      if (isWalking && !wPressed)
        {
            // set the isWalking boolean to be false
            animator.SetBool("isWalking", false);
        }
       
    }
}
