using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateEnemyController : MonoBehaviour
{
    Animator animator;
    public GameObject EnemyController;
    EnemyController EnemyControllerScript;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        EnemyControllerScript = EnemyController.GetComponent<EnemyController>();

    }

    // Update is called once per frame
    void Update()
    {
        // all variables set to each key, "isWalking" is Boolean used by unity to see if animation transition conditions are true or false, this can be found in the animation contrller settings in unity
        bool isWalking = animator.GetBool("isWalking");
        bool enemyStateWalking = EnemyControllerScript.isWalking();
        
        // if player presses w key
        if (!isWalking && enemyStateWalking)
        {
            // then set is walking = true
            animator.SetBool("isWalking", true);
        }

        // if player is not pressing w
        if (isWalking && !enemyStateWalking)
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
