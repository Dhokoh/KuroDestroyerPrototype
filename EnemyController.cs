using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemyWalkSpeed = 3f;

    public GameObject EnemyControllerGameObject;
    public BoxCollider2D EnemyCollider;
    public SpriteRenderer enemySprite;
    
    public GameObject KuroControllerGameObject;
    public KuroController kuroCtrl;
    public BoxCollider2D kuroCollider;

    //Methods

    //this one is to test how raycast works on this with A MERE FUNCTION TO CAST A RAY AND WITH DEBUG.LOG SEE IT ON THE EDITOR. 
    public void RaycastTest()
    {
        int playerLayermask = 1 << 8;
        RaycastHit2D enemySight = Physics2D.Raycast(enemySprite.transform.position, enemySprite.transform.right, Mathf.Infinity, playerLayermask);
        Debug.DrawRay(enemySprite.transform.position, -enemySprite.transform.right, Color.green);
        if (enemySight.collider != null)
        {
            Debug.Log(enemySight.collider);
            Debug.Log(enemySight.collider == kuroCollider);
            //return true;
        }
        else
        {
            Debug.Log(enemySight.collider);
            Debug.Log(enemySight.collider == kuroCollider);
            //return false
        }
        
    }

    //boolpublic bool PlayerDetected()
    public void MoveEnemy()   
    {
        while (EnemyControllerGameObject.transform.position.x <= enemyWalkSpeed)
        {
            EnemyControllerGameObject.transform.Translate(enemyWalkSpeed * Time.deltaTime, 0, 0);
        }
        enemySprite.flipX=true; 
    }

    // Start is called before the first frame update
    void Start()
    {
        KuroControllerGameObject = GameObject.FindGameObjectWithTag("Player");
        kuroCollider = KuroControllerGameObject.GetComponent<BoxCollider2D>();
        kuroCtrl = GameObject.FindObjectOfType<KuroController>();

        EnemyControllerGameObject = GameObject.FindGameObjectWithTag("Enemy");
        enemySprite = this.GetComponentInChildren<SpriteRenderer>();
        EnemyCollider = this.GetComponent<BoxCollider2D>();
    }


    //Update is called once per frame
    void Update()
    {
        MoveEnemy();
        RaycastTest();
    }
}
