using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Ghost : MonoBehaviour
{
    // Ghost AI
    public NavMeshAgent ghost;

    // Patrol variables
    public bool patroling;
    public List<Transform> patrolPoints;
    public float walkSpeed;
    public float minIdleTime;
    public float maxIdleTime;
    float idleTime;
    public float detectionRange;

    // Chase variables
    public bool chasing;
    public float chaseSpeed;
    public float catchDistance;
    public float minChaseTime;
    public float maxChaseTime;
    float chaseTime;

    // Ghost target position
    Transform currentDestination;

    // Death variables
    public float jumpscareTime;
    public string deathScene;

    // Player transform
    public Transform player;

    // Front Door
    public DoorScript frontDoor;

    // Helper variables
    int randNum;
    public Vector3 rayCastOffset;

    void Start()
    {
        patroling = true;
        randNum = Random.Range(0, patrolPoints.Count);
        currentDestination = patrolPoints[randNum];
    }
    void Update()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + rayCastOffset, direction, out hit, detectionRange))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                patroling = false;
                StopCoroutine("stayIdle");
                StopCoroutine("chaseRoutine");
                StartCoroutine("chaseRoutine");
                frontDoor.isLocked = true;
                chasing = true;
            }
        }
        if (chasing == true)
        {
            ghost.destination = player.position;
            ghost.speed = chaseSpeed;
            float distance = Vector3.Distance(player.position, ghost.transform.position);
            if (distance <= catchDistance)
            {
                StartCoroutine(deathRoutine());
                chasing = false;
            }
        }
        if (patroling == true)
        {
            ghost.destination = currentDestination.position;
            ghost.speed = walkSpeed;
            if (ghost.remainingDistance <= ghost.stoppingDistance)
            {
                ghost.speed = 0;
                StopCoroutine("stayIdle");
                StartCoroutine("stayIdle");
                patroling = false;
            }
        }
    }
    IEnumerator stayIdle()
    {
        idleTime = Random.Range(minIdleTime, maxIdleTime);
        yield return new WaitForSeconds(idleTime);
        patroling = true;
        randNum = Random.Range(0, patrolPoints.Count);
        currentDestination = patrolPoints[randNum];
    }
    IEnumerator chaseRoutine()
    {
        chaseTime = Random.Range(minChaseTime, maxChaseTime);
        yield return new WaitForSeconds(chaseTime);
        patroling = true;
        chasing = false;
        randNum = Random.Range(0, patrolPoints.Count);
        currentDestination = patrolPoints[randNum];
    }
    IEnumerator deathRoutine()
    {
        yield return new WaitForSeconds(jumpscareTime);
        SceneManager.LoadScene(deathScene);
    }
}