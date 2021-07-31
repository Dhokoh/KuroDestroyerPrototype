using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuroController : MonoBehaviour
{
    //Variables (Remember to change those important to private after testing all the movement)

    public GameObject KuroControllerGameObject;
    public Transform KuroControllerXform;
    public Animator kuroAnimator;
    public SpriteRenderer kuroSprite;

    public GameObject EnemyGameObject;
    public BoxCollider2D EnemyCollider;

    float kuroMovSPD = 8f;
    float kuroJumpHgt = 8f;
    float kuroHP;

    //Methods

    public bool EnemyDetected() //Method defined to test if the raycast detects the Collider from the Enemy. 
    {
        RaycastHit2D sight;
        int layerMask = 1 << 9;
        sight = Physics2D.Raycast(kuroSprite.transform.position, kuroSprite.transform.right, Mathf.Infinity, layerMask);
        Debug.DrawRay(kuroSprite.transform.position, kuroSprite.transform.right, Color.cyan);
        if (sight.collider == EnemyCollider)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool KuroSpotsEnemy() //This method is supposed to be used for Kuro to "see" enemies and "react" to their sighting. 
                                 //This method works in mysterious ways, Kuro Spots an enemy ONLY after jumping over the enemy and being "behind" it.
    {
        RaycastHit2D kuroSight;
        int layerMask = 1 << 9;
        if (kuroSprite.flipX == false)
        {
            kuroSight = Physics2D.Raycast(kuroSprite.transform.position, -kuroSprite.transform.right, Mathf.Infinity, layerMask);
            Debug.DrawRay(kuroSprite.transform.position, kuroSprite.transform.right, Color.blue);
            Debug.Log(kuroSight.collider);
            if (kuroSight.collider != null)
            {
                
                Debug.Log(kuroSight.collider == EnemyCollider);
                Debug.Log(kuroSight.collider);
                return true;
            }
        }
        if (kuroSprite.flipX == true)
        {
            kuroSight = Physics2D.Raycast(kuroSprite.transform.position, -kuroSprite.transform.right, Mathf.Infinity, layerMask);
            Debug.DrawRay(kuroSprite.transform.position, -kuroSprite.transform.right, Color.yellow);
            Debug.Log(kuroSprite.flipX);
            if (kuroSight.collider != null)
            {
                Debug.Log(kuroSight.collider == EnemyCollider);
                Debug.Log(kuroSight.collider);
                return true;
            }
        }
        return false;
    }
    

    public void MoveKuro()
    {
        SpriteRenderer kuroSprite = this.GetComponentInChildren<SpriteRenderer>();
        //attempt of Transform.Translate()
        if (Input.GetAxis("Horizontal") == 1 | Input.GetKeyDown(KeyCode.D))
        {
            kuroSprite.flipX = false;
            float kuroMoves = Input.GetAxis("Horizontal");
            KuroControllerXform.Translate(kuroMoves * kuroMovSPD * Time.deltaTime, 0, 0);
        }
        if (Input.GetAxis("Horizontal") == -1 | Input.GetKeyDown(KeyCode.A))
        {
            kuroSprite.flipX = true;
            float kuroMoves = Input.GetAxis("Horizontal");
            KuroControllerXform.Translate(kuroMoves * kuroMovSPD * Time.deltaTime,0,0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        kuroSprite = this.GetComponentInChildren<SpriteRenderer>();
        KuroControllerGameObject = this.gameObject;
        KuroControllerXform = this.GetComponentInChildren<Transform>();
        kuroAnimator = this.gameObject.GetComponentInChildren<Animator>();

        EnemyGameObject = GameObject.FindObjectOfType<EnemyController>().gameObject;
        EnemyCollider = EnemyGameObject.GetComponentInChildren<BoxCollider2D>();
    }

    //Update is called once per frame
    void Update()
    {
        float kuroJump = 0f;
        bool enemySpotted = KuroSpotsEnemy();
        //Attempt to move with Translate
        MoveKuro();

        if (enemySpotted == true)
        {
            kuroAnimator.SetBool("EnemySpotted", enemySpotted);
        }
        kuroAnimator.SetBool("EnemySpotted", enemySpotted);
        
        //Code for jumping. *****Pro tip> if player holds Space for 1,5 seconds, Kuro will do longer jumps... levitation :3. It is even
        //possible to perform a jump-attack (basic animation of attacking while jumping). Lucky shot xD, but now I know <3. 
        kuroJump = Input.GetAxis("Jump");
        //Debug.Log(kuroJump);
        KuroControllerXform.Translate(0, kuroJump * kuroJumpHgt * Time.deltaTime, 0);
        
        //Code for attacking. Very basic really. Setting triggers on animations. I think it's the "best option" but well, I'm a noob. So maybe there's
        //more efficient one... anyways, this is mine, I'm doing good. 
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            kuroAnimator.SetTrigger("Attack");
        }
        kuroAnimator.SetTrigger("Non-Attack");

        //KuroRaycastTest();
        //KuroSpotsEnemy(); //1.Testing if a hit is made. And if the ray is being casted.   
    }
} 
