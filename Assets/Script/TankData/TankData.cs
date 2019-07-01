using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankData : MonoBehaviour
{
    //This is the script that should handle all of the 'data'
    //involved with the tank, this is where all the variables
    //will be able to be adjusted for gameplay balance.
    public Transform tf;
    public TankMove move;
    public TankAttack attack;
    public TankLife life;

    //Creating variables for movement and rotation speed, editable
    //in the inspector.  Reverse Speed should be painfully slow,
    //Rotation should be a bit better but not as good as forward.
    public float tankForwardSpeed;
    public float tankRotateSpeed;

    //Creating variables for tank life and armor, editable
    //in the inspector, "armor" will be added to current
    //health every time damage is taken as a mitigator.
    public int tankMaxLife;
    public float tankCurrentLife;
    public float tankArmor;

    //Creating variables for damage and attack cooldowns,
    //editable in the inspector.  Gun damage will be low,
    //cannon damage will depend mostly on fire rate.
    public float tankGunDamage;
    public int tankGunAmmoMax;
    public float tankGunAmmoCurrent;

    public float tankCannonDamage;
    public int tankCannonAmmoMax;
    public float tankCannonAmmoCurrent;

    public int tankCannonFireR;
    public float tankCannonCoolD;
    public float tankGunCoolD;

    //Creating variables for sound and hearing, vision and
    //seeing, so that AI tanks can "see" and "hear" other tanks
    //(and walls) so that they can interact with them a little 
    //bit better.
    public float noiseLevel;
    public float hitBox;

    //Creating variables for score and enemies destroyed,
    //in order to create a leaderboard and other objects.
    public int lives;
    public int tankScore;

    void Awake()
    {
        tf = GetComponent<Transform>();
        move = GetComponent<TankMove>();
        attack = GetComponent<TankAttack>();
        life = GetComponent<TankLife>();

        //Removed Reverse Speed and just divided the speed
        //by 3 when moving backwards, I would have to completely
        //change Move and IController to keep and use the reverse
        //variable.
        tankForwardSpeed = 5;
        tankRotateSpeed = 100;

        tankMaxLife = 100;
        tankCurrentLife = 100;
        tankArmor = 5;

        tankGunDamage = 6;
        tankGunAmmoMax = 100;
        tankGunAmmoCurrent = 100;
        tankCannonDamage = 40;
        tankCannonAmmoMax = 10;
        tankCannonAmmoCurrent = 10;

        tankCannonFireR = 3;

        lives = 3;
        tankScore = 0;
    }

    void Update()
    {
        //moved this out of TankAttack script since
        //it was referenced here anyway, working again.
        tankCannonCoolD = tankCannonCoolD + Time.deltaTime;
        tankGunCoolD = tankGunCoolD + Time.deltaTime;
    }
}
