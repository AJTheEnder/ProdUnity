using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent nav;        //instance of a navmesh 

    Transform player;     //instance of a player
    Transform enemyTransform; // enemy transformation
    public GameObject[] waypoints; // so that I can insert references to waypoints on the map


    float patrolTime = 0f; //how lone they wait at a waypoint
    float patrolTimer; //used to time how long the patrol has been waiting for
    float moveSpeed = 300f;//movement speed
    float max_velocity;// maximum velocity
    float wanderTime; // timer for wander

    int waypointCount; //counter for the waypoints 

    Vector3 velocity;
    Vector3 desired_velocity;

    public EnemyVision vision; // instance of the enemy vison script

    public enum State //enum of the different states
    {
        Patrol,
        Seek,
        Attack,
    }

    private State _state;

    public State state
    {
        get
        {
            return _state; //getter for state
        }
        set
        {
            _state = value;//setter for state
        }
    }

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>(); // setting up the navmesh

        player = GameObject.FindGameObjectWithTag("Player").transform; // setting up the player 

        waypoints = GameObject.FindGameObjectsWithTag("AnchorPoint"); // find all the anchor points

        enemyTransform = transform; // setting up the enemy position
        max_velocity = moveSpeed * Time.deltaTime; // working out the max velocity 
        vision.playerInSight = false; // setting the enemy vision to false in the instance of the enemy vision

        state = State.Patrol;// inital state is set = patrol
    }

    void Update()
    {
        switch (state)
        {
            case State.Patrol:
                patrol();
                break;
            case State.Seek:
                seek();
                break;
        }
    }

    void patrol()
    {
        nav.speed = 150f;// sets the patrol speed

        if (vision.playerInSight == true)// if the enemy is in sight.
        {
            state = State.Seek;// change to seeking state
        }

        if (nav.remainingDistance <= nav.stoppingDistance) // when the enemy arrvies at the waypoint
        {
            patrolTimer += Time.deltaTime;//starts timing how long they have been there

            if (patrolTimer >= patrolTime)// when the timer is greater than the wait time
            {
                if (waypointCount == waypoints.Length - 1)//when they arrive at the last waypoint
                {
                    waypointCount = 0; //resets waypoint counter
                }
                else// if they don't arrive at the last waypoint
                {
                    waypointCount++;//increase waypoint counter
                }
                patrolTimer = 0;//reset timer
            }
        }
        nav.destination = waypoints[waypointCount].transform.position;//setting nav next destinantion
    }

    void seek()
    {
        velocity = player.position - enemyTransform.position;// setting velocity as the left of distance of player and enemy

        enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, Quaternion.LookRotation(velocity), max_velocity);// rotates enemy with the velocity and max velocity

        desired_velocity = velocity.normalized * max_velocity;
        nav.destination += desired_velocity;//moves towards player

        if (velocity.magnitude < 5)
        {
            state = State.Attack;// set to attack state
        }

        if (vision.playerInSight == false)// if not in sight
        {
            state = State.Patrol;//set to patrol state
        }
    }
}
