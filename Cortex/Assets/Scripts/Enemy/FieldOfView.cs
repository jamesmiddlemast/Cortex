using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0,360)]
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;

    float integrityDamage = 1.0f;
    public Light flashlight;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        flashlight = gameObject.GetComponentInChildren<Light>();
        if (radius == 0) {
            radius = 7;
        }
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
                    canSeePlayer = true;
                    //If able to see the player, increase damage of the memory.
                    CharController.integrity += integrityDamage * Time.deltaTime;
                }
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer){
            canSeePlayer = false;
        }
        UpdateSpotlight(canSeePlayer);
    }

    private void UpdateSpotlight(bool seePlayer){
        if (seePlayer){
            //Change Spotlight to Red
            flashlight.color = new Color32(241, 69, 31, 255);
            return;
            //F1461F
        } else {
            //Change Spotlight to Yellow
            flashlight.color = new Color32(243, 235, 93, 255);
            return;
            //F3EB5D
        }
    }
}