using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Preparing for multiplayer, by starting the
//alternative control schemes now rather than
//later. trying to set all this up.
[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMove))]
[RequireComponent(typeof(TankLife))]
[RequireComponent(typeof(TankAttack))]

public class InputControl : MonoBehaviour
{
    public enum ControlScheme { WASD, NUMP }
    public ControlScheme controlScheme;

    public TankData tankData;

    private void Awake()
    {
        {
            tankData = this.gameObject.GetComponent<TankData>();
        }
    }

    private void Update()
    {
        Vector3 directionToMove = Vector3.zero;

        if (controlScheme == ControlScheme.WASD)
        {
            //forward
            if (Input.GetKey(KeyCode.W))
            {
                //Debug.Log("1P Input Forward");
                directionToMove += tankData.tf.forward;
            }
            //backward
            if (Input.GetKey(KeyCode.S))
            {
                //Debug.Log("1P Input Reverse");
                directionToMove += -tankData.tf.forward / 3;
            }
            //left
            if (Input.GetKey(KeyCode.A))
            {
                //Debug.Log("1P Input Left");
                tankData.move.Rotate(-tankData.tankRotateSpeed * Time.deltaTime);
            }
            //right
            if (Input.GetKey(KeyCode.D))
            {
                //Debug.Log("1P Input Right");
                tankData.move.Rotate(tankData.tankRotateSpeed * Time.deltaTime);
            }
            //Gun
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("Gun FIRE!");
                tankData.attack.FireGun();
            }
            //Cannon
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //Debug.Log("Cannon FIRE!");
                tankData.attack.FireCannon();
            }
        }
        else if (controlScheme == ControlScheme.NUMP)
        {
            //forward
            if (Input.GetKey(KeyCode.Keypad8))
            {
                Debug.Log("NUMP Forward");
                directionToMove += Vector3.forward;
            }
            //backward
            if (Input.GetKey(KeyCode.Keypad5))
            {
                Debug.Log("NUMP Backward");
                directionToMove += -Vector3.forward;
            }
            //left
            if (Input.GetKey(KeyCode.Keypad4))
            {
                Debug.Log("NUMP Left");
                tankData.move.Rotate(-tankData.tankRotateSpeed * Time.deltaTime);
            }
            //right
            if (Input.GetKey(KeyCode.Keypad6))
            {
                Debug.Log("NUMP Right");
                tankData.move.Rotate(tankData.tankRotateSpeed * Time.deltaTime);
            }
            //Cannon
            if (Input.GetKey(KeyCode.Keypad7))
            {
                //Debug.Log("Gun FIRE!");
                tankData.attack.FireCannon();
            }
            //Gun
            if (Input.GetKey(KeyCode.Keypad9))
            {
                //Debug.Log("Gun FIRE!");
                tankData.attack.FireGun();
            }
        }
        tankData.move.Move(directionToMove);
    }
}