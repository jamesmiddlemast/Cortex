using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFieldOfView : MonoBehaviour
{
    public float radius = 8f;
    public float angle = 70f;

    public GameObject playerRef;
    public static GameObject targetBody;

    public LayerMask targetMask;
    public LayerMask obstructionMask;


    public bool canSeeTarget;

    private void Start()
    {
        //playerRef = GameObject.FindGameObjectWithTag("Enemy");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.02f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        //Reset canSeeTarget
        if (canSeeTarget){
            canSeeTarget = false;
            targetBody.GetComponent<EnemyController>().isGlowing = false;
        }

        bool foundTarget = false;
        GameObject foundTargetBody = null;
        //If rangeChecks means multiple enemies within sphere
        if (rangeChecks.Length != 0)
        {
            //Check each enemy to see whether they are an acceptable target
            for (int i = 0; i<rangeChecks.Length; i++){
                Transform target = rangeChecks[i].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if ((Vector3.Angle(transform.forward, directionToTarget) < angle / 2) || (-1 * Vector3.Angle(transform.forward, directionToTarget) > -1 * angle / 2))
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.position);
                    //If able to see
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)){
                        foundTarget = true;
                        foundTargetBody = target.gameObject;
                        //canSeeTarget = true;
                        //targetBody = target.gameObject;
                        //Set Target's material to glowin.
                        //targetBody.GetComponent<EnemyController>().isGlowing = true;
                        //playerRef = targetBody;
                    }
                }
            }   
            if (foundTarget){
                canSeeTarget = true;
                targetBody = foundTargetBody;
                targetBody.GetComponent<EnemyController>().isGlowing = true;
                //playerRef = targetBody;
            }        
        }
    }

    public void PlayerJumping(){
        //playerRef = null;
        targetBody = null;
        canSeeTarget = false;
    }
}