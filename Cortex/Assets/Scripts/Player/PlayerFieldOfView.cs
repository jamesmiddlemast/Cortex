using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;
    public static GameObject targetBody;

    public LayerMask targetMask;
    public LayerMask obstructionMask;


    public static bool canSeeTarget;

    private void Start()
    {
        //playerRef = GameObject.FindGameObjectWithTag("Enemy");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)){
                    canSeeTarget = true;
                    targetBody = target.gameObject;
                    //Set Target's material to glowin.
                    targetBody.GetComponent<EnemyController>().isGlowing = true;
                    playerRef = targetBody;
                }
                else{
                    canSeeTarget = false;
                    targetBody.GetComponent<EnemyController>().isGlowing = false;
                }
            }
            else{
                canSeeTarget = false;
                targetBody.GetComponent<EnemyController>().isGlowing = false;
            }
        }
        else if (canSeeTarget){
            canSeeTarget = false;
            targetBody.GetComponent<EnemyController>().isGlowing = false;
        }
    }
}