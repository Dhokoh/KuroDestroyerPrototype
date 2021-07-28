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

    float kuroMovSPD = 8f;
    float kuroJumpHgt = 8f;

    //Methods

     public bool EnemyDetected()
    {
        RaycastHit2D kuroSight;
        int playerLayer = 8;
        int layerMask = 1 << playerLayer;
        if (kuroSprite.flipX == false)
        {
            kuroSight = Physics2D.Raycast(kuroSprite.transform.position, kuroSprite.transform.right, Mathf.Infinity);
            Debug.DrawRay(kuroSprite.transform.position, kuroSprite.transform.right, Color.blue);
            Debug.Log(kuroSight.collider.name);
            if (kuroSight.rigidbody == EnemyCollider)
            {
                Debug.Log(kuroSight.collider == EnemyCollider);
                return true;
            }
            else
            {
                return false;
            }
        }
        if (kuroSprite.flipX == true)
        {
            kuroSight = Physics2D.Raycast(kuroSprite.transform.position, -kuroSprite.transform.right, Mathf.Infinity);
            Debug.DrawRay(kuroSprite.transform.position, -kuroSprite.transform.right, Color.blue);
            if (kuroSight.rigidbody == EnemyGameObject.GetComponent<BoxCollider2D>())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    public void RaycastTest()
    {
        RaycastHit2D kuroSight = Physics2D.Raycast(this.transform.position, this.transform.right);
        Debug.DrawRay(this.transform.position, this.transform.right, Color.blue);
    }

    public void MoveKuro()
    {
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
    }

    //Update is called once per frame
    void Update()
    {
        float kuroJump = 0f;
        //Attempt to move with Translate
        MoveKuro();
        
        //Code for jumping. *****Pro tip> if player holds Space for 1,5 seconds, Kuro will do longer jumps... levitation :3. It is even
        //possible to perform a jump-attack (basic animation of attacking while jumping). Lucky shot xD, but now I know <3. 
        kuroJump = Input.GetAxis("Jump");
        Debug.Log(kuroJump);
        KuroControllerXform.Translate(0, kuroJump * kuroJumpHgt * Time.deltaTime, 0);
        
        //Code for attacking. Very basic really. Setting triggers on animations. I think it's the "best option" but well, I'm a noob. So maybe there's
        //more efficient one... anyways, this is mine, I'm doing good. 
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            kuroAnimator.SetTrigger("Attack");
        }
        kuroAnimator.SetTrigger("Non-Attack");

        //Attempts to make Kuro's vision range... using a Raycast... effing thing to be so hard to grasp!!! 
        EnemyDetected(); //1.Testing if a hit is made. And if the ray is being casted.   

    }
} 
