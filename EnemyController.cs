using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject EnemyControllerGameObject;
    public Transform enemySpriteTransform;
    
    public GameObject KuroControllerGameObject;
    public BoxCollider2D kuroCollider;

    //Methods

    //this one is to test how raycast works
    public void RaycastTest()
    {
        RaycastHit2D enemySight = Physics2D.Raycast(EnemyControllerGameObject.transform.position, -EnemyControllerGameObject.transform.right,6);

        Debug.DrawRay(this.transform.position, -this.transform.right, Color.green);
        Debug.Log(enemySight.collider == kuroCollider);
    }


    public void EnemyAttack()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        KuroControllerGameObject = GameObject.FindGameObjectWithTag("Player");
        kuroCollider = KuroControllerGameObject.GetComponent<BoxCollider2D>();
        //Debug.Log(KuroControllerGameObject.name);

        EnemyControllerGameObject = GameObject.FindGameObjectWithTag("Enemy");
        enemySpriteTransform = EnemyControllerGameObject.GetComponentInChildren<Transform>();
    }


    //Update is called once per frame
    void Update()
    {
        RaycastTest();
    }
}
