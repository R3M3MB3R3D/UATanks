using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIControl : MonoBehaviour
{
    public TankData tankData;
    public TankAttack tankAttack;
    public TankLife tankLife;
    public TankMove tankMove;

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
        Idle, Move, Camp, Assault, Retreat
    }
    public AIStates currentState = AIStates.Idle;

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
    }

    void Update()
    {
        if ((tankData.tankGunCoolD >= .25) & (tankData.tankGunAmmoCurrent > 0))
        {
            tankData.attack.FireGun();
            tankData.tankGunCoolD = 0;
            tankData.tankGunAmmoCurrent -= 1;
        }
        if ((tankData.tankCannonCoolD >= tankData.tankCannonFireR) & (tankData.tankCannonAmmoCurrent > 0))
        {
                tankData.attack.FireCannon();
                tankData.tankCannonCoolD = 0;
                tankData.tankCannonAmmoCurrent -= 1;
            
        }
    }
}
