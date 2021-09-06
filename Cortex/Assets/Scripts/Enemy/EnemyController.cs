using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    //Movement Speeds
    float walkSpeed;
    float turnSpeed;
    [SerializeField]
    //Guarding time.
    float guardTime;
    float currentGuardTime;

    //Waypoints
    int currentWaypoint = 0;
    public GameObject[] waypoints;

    [SerializeField]
    bool repeatPatrol;

    float waypointThreshold = 1.0f;
    float faceTime = 3;
    float currentFaceTime;

    //Using Finite State Machine
    /* Enemy States
        Patrolling - Walking towards the next waypoint.
        Guarding - Waiting at the current waypoint.
            Rotating - Rotating at current waypoint.
        Facing - Turning towards the next waypoint.
        Finished - Waiting at final waypoint.
    */
    [SerializeField]
    string enemyState;

    // Start is called before the first frame update
    void Start()
    {  
        //Safety, stops updating if Waypoint is 0
        if (waypoints.Length <1){
            enemyState = "Finished";
        } else {
            enemyState = "Patrolling";
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState){
            case "Patrolling" :
                Patrol();
                break;
            case "Guarding":
                Guard();
                break;
            case "Rotating":
                Rotate();
                break;
            case "Facing":
                Face();
                break;
            case "Finished":
                break;
        }
    }

    void Patrol(){
        //Seek out the next waypoint
        //Get the vector towards the next waypoint
        Vector3 direction = waypoints[currentWaypoint].transform.position - transform.position;
        //Normalize
        direction.Normalize();
        direction = direction * Time.deltaTime * walkSpeed;
        transform.position = transform.position + direction;

        //Check whether close enough to the waypoint
        if (Mathf.Abs(waypoints[currentWaypoint].transform.position.x - transform.position.x) < waypointThreshold){
            if (Mathf.Abs(waypoints[currentWaypoint].transform.position.z - transform.position.z) < waypointThreshold){
                //If so, start Guarding at the current Waypoint.
                enemyState = "Guarding";
                currentGuardTime = guardTime;
            }
        }
    }

    void Guard(){
        //Wait at the Waypoint for the period of time
        currentGuardTime -= Time.deltaTime;
        if (currentGuardTime <= 0){
            currentWaypoint +=1;
            //If reached the last Waypoint
            if (currentWaypoint == waypoints.Length){
                //Check whether to cycle to first waypoint
                if (repeatPatrol){
                    //Reset to first waypoint
                    currentWaypoint = 0;
                    //Start turning to face the next waypoint.
                    currentFaceTime = faceTime;
                    enemyState = "Facing";
                } else {
                    //Otherwise set state to finished
                    enemyState = "Finished";
                }
            } else {
                //Start turning to face the next waypoint.
                currentFaceTime = faceTime;
                enemyState = "Facing";
            }
        }
    }

    void Rotate(){

    }

    void Face(){
        //Reduce Cooldown
        currentFaceTime -= Time.deltaTime;
        //Rotate towards next waypoint.
        Quaternion targetRotation = Quaternion.LookRotation (waypoints[currentWaypoint].transform.position - transform.position);
        float turnAmount = Mathf.Min (turnSpeed * Time.deltaTime, 1);
        transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, turnAmount);
    
        //At the end of the face cooldown, snap to face and start moving.
        if (currentFaceTime <= 0){
            transform.rotation = targetRotation;
            enemyState = "Patrolling";
        }
    }
}
