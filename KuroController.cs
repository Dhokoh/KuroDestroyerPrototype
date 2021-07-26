using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuroController : MonoBehaviour
{
    //Variables (Remember to change those important to private after testing all the movement)

    public GameObject KuroControllerGameObject;
    public Transform KuroControllerXform;
    public Animator kuroAnimator;

    public GameObject EnemyGameObject;

    float kuroMovSPD = 8f;
    float kuroJumpHgt = 8f;

    //Methods

    public bool EnemyDetected()
    {
        bool kuroSaw = false;
        RaycastHit2D kuroSight;
        kuroSight = Physics2D.Raycast(KuroControllerGameObject.transform.position, KuroControllerGameObject.transform.right);
        Debug.DrawRay(this.transform.position, this.transform.right, Color.red);
        if (kuroSight.collider != null && kuroSight.rigidbody == EnemyGameObject.GetComponent<Rigidbody2D>())
        {
            kuroSaw = true;
            Debug.Log(kuroSaw);
            return kuroSaw;
        }
        else
        {
            kuroSaw = false;
            Debug.Log(kuroSaw);
            return kuroSaw;
        }
    }

    public void RaycastTest()
    {
        RaycastHit2D kuroSight = Physics2D.Raycast(this.transform.position, this.transform.right);
        Debug.DrawRay(this.transform.position, this.transform.right, Color.blue);
    }

    public void MoveKuro()
    {
        //attempt of Transform.Translate()
        if (Input.GetAxis("Horizontal") == 1 | Input.GetAxis("Horizontal") == -1)
        {
            float kuroMoves = Input.GetAxis("Horizontal");
            Debug.Log(kuroMoves);
            KuroControllerXform.Translate(kuroMoves * kuroMovSPD * Time.deltaTime, 0, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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
