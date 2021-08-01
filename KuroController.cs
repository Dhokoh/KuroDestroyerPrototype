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

    public float kuroMovSPD = 8f;
    public float kuroJumpHgt = 8f;
    public float kuroHP = 100;

    bool getHit = false;

    //Methods

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

    private void TurnUpSideDown()
    {
        Quaternion rotQuaternion = 
        if (kuroSprite.transform.rotation.z == 0)
        {
            kuroSprite.transform.rotation.z = 0;
        }    
        if (kuroSprite.transform.rotation.z <= 0.7f && kuroSprite.transform.rotation.z != 0)
        {
            kuroSprite.transform.Rotate(0, 0, -90);
        }
        if (kuroSprite.transform.rotation.z >= -0.7f && kuroSprite.transform.rotation.z !=  0)
        {
            kuroSprite.transform.Rotate(0, 0, 90);
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
        Debug.Log("The value for the X axis in the rotation is: " + kuroSprite.transform.rotation.x);
        Debug.Log("The value for the Y axis in the rotation is: " + kuroSprite.transform.rotation.y);
        Debug.Log("The value for the Z axis in the rotation is: " + kuroSprite.transform.rotation.z);
        
        float kuroJump = 0f;
        MoveKuro();
        if (kuroSprite.transform.rotation.z >= 0 && kuroSprite.transform.rotation.z < 0.7f)
        {
            TurnUpSideDown();

        }
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
