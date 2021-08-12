using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// EnemyController is a class that controls basic information and movements from enemies, including bosses and minibosses.
    /// Consider Enemies HP, Damage, movement and "Kuro detection". 
    /// </summary>
    public float enemyHP;//this is to be assigned via the Unity Editor. Not all enemies have the same HP.
    public float enemyDMG;// this is to be assigned via de Unity Editor. Not all enemies deal the same damage. 
    public float enemyWalkSpd;//This is to be assigned via de Unity Editor. Not all enemies walk equally fast. 
    public float moveUnits = 1.5f;
    float enemyStepCounter = 300;

    public Vector2 initialPos;

    public GameObject EnemyControllerGO;
    public SpriteRenderer enemySprite;
    public RaycastHit2D enemySight;

    public bool isBoss; //this is to be assigned via the Unity Editor. Boss class will be created later on and will interact with this one. 
    public bool hasTurned;

    // Start is called before the first frame update
    void Start()
    {
        hasTurned = false;
        EnemyControllerGO = this.gameObject;
        enemySprite = EnemyControllerGO.GetComponentInChildren<SpriteRenderer>();
        initialPos = EnemyControllerGO.transform.position;
    }


    //Private class methods

    private void TurnAround()
    {
        if (hasTurned == false)
        {
            EnemyControllerGO.GetComponentInChildren<SpriteRenderer>().flipX = true;
            hasTurned = true;
        }
        if (hasTurned == true)
        {
            EnemyControllerGO.GetComponentInChildren<SpriteRenderer>().flipX = false;
            hasTurned = false;
        }
    }

    private void EnemyPatrol()
    {
        EnemyControllerGO.transform.Translate(Vector2.left * enemyWalkSpd * Time.deltaTime);
        Vector2 currentPos = EnemyControllerGO.transform.position;
        if (Vector2.Distance(initialPos,currentPos) >= moveUnits)
        {
            Debug.Log("Code entered the conditional on EnemyPatrol");
            initialPos = currentPos;
            TurnAround();
            EnemyControllerGO.transform.Translate(Vector2.right * enemyWalkSpd * Time.deltaTime);
        }
    }
   







    //Update is called once per frame
    void Update()
    {
        EnemyPatrol();
        Debug.Log(Vector2.Distance(initialPos,EnemyControllerGO.transform.position));
    }
}
