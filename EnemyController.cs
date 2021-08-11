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
    public float moveUnits = 1;
    float enemyStepCounter = 0;

    public Vector2 initialPos;


    public GameObject EnemyControllerGO;
    public SpriteRenderer enemySprite;
    public RaycastHit2D enemySight;

    public bool isFacingRight;
    public bool isBoss; //this is to be assigned via the Unity Editor. Boss class will be created later on and will interact with this one. 
    public bool hasTurned;

    // Start is called before the first frame update
    void Start()
    {
        hasTurned = false;
        EnemyControllerGO = this.gameObject;
        enemySprite = EnemyControllerGO.GetComponentInChildren<SpriteRenderer>();
        isFacingRight = EnemyControllerGO.transform.localScale.x > 0;
        initialPos = EnemyControllerGO.transform.position;
        Debug.Log(EnemyControllerGO.transform.localScale.x);
        Debug.Log(isFacingRight);

    }


    //Private class methods

    private void TurnAround()
    {
        if (hasTurned == false)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            hasTurned = true;
            isFacingRight = EnemyControllerGO.transform.localScale.x > 0;
            Debug.Log(transform.localScale);
            Debug.Log(isFacingRight + " this is the isFacingRight value which is a transform.localScale.x > 0");
            Debug.Log(hasTurned+" this is... well.. hasTurned's value.");
        }
        
    }

    private void EnemyPatrol()
    {
        EnemyControllerGO.transform.Translate(Vector2.left * enemyWalkSpd * Time.deltaTime);
        Vector2 currentPos = EnemyControllerGO.transform.position;
        Debug.Log(Vector2.Distance(initialPos, currentPos));
        enemyStepCounter += 1;
        if(enemyStepCounter == moveUnits)
        {
            TurnAround();
        }
    }
   







    //Update is called once per frame
    void Update()
    {
        EnemyPatrol();
    }
}

