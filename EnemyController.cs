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
    public bool isBoss; //this is to be assigned via the Unity Editor. Boss class will be created later on and will interact with this one. 

    public GameObject EnemyControllerGO;
    public RaycastHit2D enemySight;


    // Start is called before the first frame update
    void Start()
    {
        EnemyControllerGO = this.gameObject;
    }


    //Private class methods

    private void EnemyMovement()
    {
        EnemyControllerGO.GetComponent<Rigidbody2D>().velocity = new Vector2(1,0);
        EnemyControllerGO.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0));
    }







    //Update is called once per frame
    void Update()
    {
        EnemyMovement();
        Debug.Log(EnemyControllerGO.transform.position);
    }
}
