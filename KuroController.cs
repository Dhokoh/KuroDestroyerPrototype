using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuroController : MonoBehaviour
{
    //Variables (Remember to change those important to private after testing all the movement)

    public GameObject KuroControllerGameObject;
    public Transform KuroControllerTransform;
    public Animator kuroAnimator;
    public SpriteRenderer kuroSprite;

    public GameObject EnemyGameObject;
    public BoxCollider2D EnemyCollider;

    public float kuroMovSPD = 8f;
    public float kuroJumpHgt = 8f;
    public float kuroHP = 100;

    bool getHit = false;
    bool needsStandUp;

    // Start is called before the first frame update
    void Start()
    {
        KuroControllerGameObject = this.gameObject;
        KuroControllerTransform = KuroControllerGameObject.transform;
        kuroAnimator = KuroControllerGameObject.GetComponentInChildren<Animator>();
        kuroSprite = KuroControllerGameObject.GetComponentInChildren<SpriteRenderer>();

        EnemyGameObject = GameObject.FindGameObjectWithTag("Enemy");
        EnemyCollider = EnemyGameObject.GetComponent<BoxCollider2D>();

    }

    //Private Methods to be used within this class only. Change only if strictly needed. 

    private bool HasRotatedZ() //Method commented out due to complication with Quaternions comparations (?).
    {
        Quaternion currentRotation = KuroControllerGameObject.transform.rotation;
        if (currentRotation.z >= 0.7f)
        {
            needsStandUp = true;
            return needsStandUp;
        }
        else if (currentRotation.z <= -0.7f)
        {
            needsStandUp = true;
            return needsStandUp;
        }
        else
        {
            needsStandUp = false;
            return false;
        }
    }


    public void StandUp()
    {
        bool isHorizontal = HasRotatedZ();
        if (isHorizontal == true && KuroControllerGameObject.transform.rotation.z>=0.7f)
        {
            KuroControllerGameObject.transform.Rotate(0, 0, -90);
        }
        if (isHorizontal == true && KuroControllerGameObject.transform.rotation.z <=-0.7)
        {
            KuroControllerGameObject.transform.Rotate(0, 0, 90);
        }
    }









    //Methods for Update

    public void MoveKuro()
    {
        SpriteRenderer kuroSprite = this.GetComponentInChildren<SpriteRenderer>();
        //attempt of Transform.Translate()
        if (Input.GetAxis("Horizontal") == 1 | Input.GetKeyDown(KeyCode.D))
        {
            kuroSprite.flipX = false;
            float kuroMoves = Input.GetAxis("Horizontal");
            KuroControllerTransform.Translate(kuroMoves * kuroMovSPD * Time.deltaTime, 0, 0);
        }
        if (Input.GetAxis("Horizontal") == -1 | Input.GetKeyDown(KeyCode.A))
        {
            kuroSprite.flipX = true;
            float kuroMoves = Input.GetAxis("Horizontal");
            KuroControllerTransform.Translate(kuroMoves * kuroMovSPD * Time.deltaTime, 0, 0);
        }
    }











    //Update is called once per frame
    void Update()
    {
        HasRotatedZ();
        if (needsStandUp == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                StandUp();
            }
        }

        float kuroJump = 0f;
        MoveKuro();

        //Code for jumping. *****Pro tip> if player holds Space for 1,5 seconds, Kuro will do longer jumps... levitation :3. It is even
        //possible to perform a jump-attack (basic animation of attacking while jumping). Lucky shot xD, but now I know <3. 
        kuroJump = Input.GetAxis("Jump");
        //Debug.Log(kuroJump);
        KuroControllerTransform.Translate(0, kuroJump * kuroJumpHgt * Time.deltaTime, 0);

        //Code for attacking. Very basic really. Setting triggers on animations. I think it's the "best option" but well, I'm a noob. So maybe there's
        //more efficient one... anyways, this is mine, I'm doing good. 
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            kuroAnimator.SetTrigger("Attack");
        }
        kuroAnimator.SetTrigger("Non-Attack");
    }
    
} 

