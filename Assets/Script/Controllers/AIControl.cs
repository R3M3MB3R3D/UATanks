using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    //referencing outside scripts to attach them later
    //Since all tanks use the same data and movement
    //scripts.
    public TankData tankData;
    public TankAttack tankAttack;
    public TankLife tankLife;
    public TankMove tankMove;

    //Creating a variable for the targetting of the player
    public GameObject target;

    //Creating the variables we will need to provide the
    //AI with the semblance of sight, these are what we
    //are going to need to accomplish that.
    public float viewDistance;
    [Range(0, 360)]
    public float fieldOfView;

    //This will help us determine whether or not the AI "saw"
    //the player and decide whether or not the AI will move into
    //Pursue or Retreat states based on that fact.
    public bool inLineOfSight()
    {
        Debug.Log("Im looking");
        //Using a raycast, we will determine where the AI is and where
        //the player is, respective to each other.
        Vector3 vectorToTarget = target.transform.position - transform.position;
        RaycastHit hitInfo;
        Debug.DrawRay(transform.position, transform.forward * viewDistance, Color.red);
        Physics.Raycast(transform.position, vectorToTarget, out hitInfo, viewDistance);
        float angle = Vector3.Angle(vectorToTarget, transform.forward / 2);


        //if there is no hitInfo, the AI didn't see anything.
        if (hitInfo.collider == null)
        {
            return false;
        }

        //IF the position is within the FOV, IF the position is within view distance,
        //IF the hitInfo is a Player, THEN the AI has "seen" the player.
        if (angle <= fieldOfView)
        {
            if (Vector3.Distance(transform.position, target.transform.position) < viewDistance)
            {
                if (hitInfo.collider.gameObject.GetComponent<InputControl>() == target.GetComponent<InputControl>())
                {
                    Debug.Log("I SEE YOU!!!");
                    return true;
                }
            }
        }
        return false;
    }

    //This will help us determine whether or not the AI "heard" the player
    //and decide whether or not the AI will move into Pursue or Retreat
    //states based on that fact.
    public bool listeningForNoise()
    {
        //if there's no noise, the AI didn't hear anything.
        if (target.GetComponent<TankData>().noiseLevel <= 0)
        {
            return false;
        }
        else
        {
            //Basically, sound is compared to distance and if its more than 0
            //the AI "heard" the player, and the bool returns true.
            float noiseLevel = target.GetComponent<TankData>().noiseLevel;
            float distance = Vector3.Distance(transform.position, target.gameObject.transform.position);
            noiseLevel -= distance;
            if (noiseLevel >= 1)
            {
                Debug.Log("I HEAR YOU!!!");
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    //AIPerson will be the way our AI acts in game, we will
    //have them spawn with a random looptype so that the 
    //game is never exactly the same.
    public enum AIPerson
    {
        Aggressive, Patroller, Coward, Camper, Random
    }
    public AIPerson currentPerson;

    //AIStates will be how our AI will actually carry out
    //it's personality type.  All of the States are simple
    //commands to be carried out.
    public enum AIStates
    {
        Pursue, Camp, Patrol, Retreat, Power
    }
    public AIStates currentState = AIStates.Camp;

    //AIAvoid will be a sub-routine for when the AI runs into
    //an obstacle that it does not have any instructions to 
    //interact with, so it will avoid that obstacle instead.
    public enum AIAvoid
    {
        None, TurnToAvoid, MoveToAvoid
    }
    public AIAvoid currentAvoid = AIAvoid.None;

    //waypoints will be used for several AI personalities to
    //give them direction when the time comes for them to move
    //when the player is not a factor.
    public List<Transform> waypoints;
    public int currentWaypoint;

    public float cutOff;
    public bool isForward;
    public float feelerDistance;

    private void Awake()
    {
        tankData = GetComponent<TankData>();
        tankAttack = GetComponent<TankAttack>();
        tankLife = GetComponent<TankLife>();
        tankMove = GetComponent<TankMove>();
        currentState = AIStates.Camp;
    }

    private void stateHandler()
    {
        switch (currentState)
        {
            case AIStates.Pursue:
                Pursue();
                break;
            case AIStates.Camp:
                Camp();
                break;
            case AIStates.Patrol:
                Patrol();
                break;
            case AIStates.Retreat:
                Retreat();
                break;
            case AIStates.Power:
                Power();
                break;
        }
    }

    void Update()
    {
        stateHandler();
    }

    //When the AI has sensed the player, it will move
    //into pursuit mode, where it will move towards and
    //attack the player.
    protected void Pursue()
    {

    }

    //Watches and listens for the player, and attacks the
    //player that moves into it's detection range. *TODO
    //set up camp points for the AI to inhabit.
    protected void Camp()
    {
        if (inLineOfSight() || listeningForNoise())
        {
            tankMove.RotateToward(target.transform.position);
            tankAttack.FireCannon();
        }
    }

    //Moves from point to point while watching and listening
    //for the player.  *TODO set up waypoints for the AI to
    //patrol along.
    protected void Patrol()
    {

    }

    //At a certain threshoold of health, the AI will move into
    //retreat mode, where it will move away from the player as
    //best it can.
    protected void Retreat()
    {

    }

    //If the AI is not 100% on health and ammo, it will move into
    //Power mode once it detects a powerup and attempt to claim it
    //in order to replenish missing stats.
    protected void Power()
    {

    }
}