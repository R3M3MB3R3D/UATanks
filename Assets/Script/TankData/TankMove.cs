using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Since all the 'data' concerning the gameObject comes from
//TankData and all the 'functions' for that 'data' come from
//other scripts, we make the functions require the data.
[RequireComponent(typeof(TankData))]

public class TankMove : MonoBehaviour
{
    public TankData tankData;
    private CharacterController characterController;

    void Awake()
    {
        tankData = this.gameObject.GetComponent<TankData>();
        characterController = GetComponent<CharacterController>();
    }

    //This should be the function called in order to move any tank
    //inside of the game.
    public void Move(Vector3 DirectionToMove)
    {
        //This tells the CharacterController component to move the tank.
        characterController.SimpleMove(DirectionToMove * tankData.tankForwardSpeed);
        tankData.noiseLevel = 5;
    }

    //This should be the function called in order to rotate any tank
    //inside of the game.
    public void Rotate(float direction)
    { 
        tankData.tf.Rotate(new Vector3(0, direction * tankData.tankRotateSpeed * Time.deltaTime, 0));
        tankData.noiseLevel = 3;
    }

    //This will be used by the AI tanks to properly face player tanks
    //from inside the game.
    public void RotateToward(Vector3 lookVector)
    {
        //Basically, using vectors, find the target.
        Vector3 vectorToTarget = lookVector - transform.position;
        //Basically, using vectors, determine which Quaternion we need.
        Quaternion targetQuat = Quaternion.LookRotation(vectorToTarget);
        //Rotate towards the determined Quaternion.
        tankData.tf.rotation = Quaternion.RotateTowards(tankData.tf.rotation, targetQuat, tankData.tankRotateSpeed * Time.deltaTime);
    }
}
