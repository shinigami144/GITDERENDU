using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerMouvementScipt : MonoBehaviour
{
    const int HAUTEUR = 350;
    const float SPEED = 0.04f;
    const int MAXJUMP = 2;
    
    // traverse plateforme +  croll animation , hit animation death animation  
    // HIT BOX BUG ?? -> HOW TO CORRECT THIS ? 

    // GRAPPIN -> COMMENT FAIRE ?! complexe et aucune idée.
    // SAVE FILE 
    // MENU 

    [SerializeField]
    private UnityEngine.Tilemaps.Tilemap PhisicMap;
    public int doubleInputID;
    private float timeBetweenInput;
    private Rigidbody2D myRb;
    private Animator myAnim;
    private int playerLife;
    public int contJump;
    private bool releasedJumpButton;
    private bool dealDamage;
    public ETAT EtatPerso;
    private int Hp;
    private GameObject GameManager;
    //private Animation AnimarionToPlay;
    
    public enum ETAT
    {
        Neutre,
        Acroupi,
        EnHauteur,
        Intengible
    }

    enum Animation
    {
        Idle,
        Run,
        Jump,
        Fall,
        CrollIdel,
        AttaqueBase,
        AttaqueCroll,
        AttaqueJump,
    }
    private int direction;

    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        playerLife = 10;
        contJump = 0;
        releasedJumpButton = true;
        myAnim = GetComponent<Animator>();
        //StartCoroutine(CorutineTest());
        myAnim.SetFloat("XSpeed", 0f);
        direction = 1;
        //AnimarionToPlay = Animation.Idle;
        EtatPerso = ETAT.Neutre;
        doubleInputID = 0;
        dealDamage = false;
        Hp = 3;
        GameManager = FindObjectOfType<GameManagerScript>().gameObject;
    }

    private bool AnimationIsPlaying(string AnimationName)
    {
        return myAnim.GetCurrentAnimatorStateInfo(0).IsName(AnimationName);
    }

    private bool CurrentAnimationLooped(string AnimationName)
    {
        if (AnimationIsPlaying(AnimationName))
        {
            return myAnim.GetCurrentAnimatorStateInfo(0).loop;
        }
        return false;
    }
    
    private void EtatChute()
    {
        if (myRb.velocity.y < -1 && !AnimationIsPlaying("AttaqueJump"))
        {
            AnimationPlayer(Animation.Fall);
        }
        else
        {
            if(myRb.velocity.y == 0 && EtatPerso == ETAT.Intengible)
            {
                AnimationPlayer(Animation.CrollIdel);
                EtatPerso = ETAT.Acroupi;
                contJump = 0;
            }
            else if (myRb.velocity.y == 0 && (AnimationIsPlaying("Player_Fall") ))
            {
                AnimationPlayer(Animation.Idle);
                EtatPerso = ETAT.Neutre;
                contJump = 0;
            }
        }
    }


    private void AnimationPlayer(Animation animation)
    {
        switch (animation)
        {
            case Animation.Jump:
                myAnim.Play("Player_Jump");
                break;
            case Animation.Fall:
                //Debug.Log("FALL");
                myAnim.Play("Player_Fall");
                break;
            case Animation.Run:
                //Debug.Log("RUN");
                myAnim.Play("Player_Run");
                break;
            case Animation.Idle:
                //Debug.Log("IDLE");
                myAnim.Play("Player_Idel");
                break;
            case Animation.CrollIdel:
                myAnim.Play("CrollIdel");
               
                break;
            case Animation.AttaqueBase:
                myAnim.Play("AttaqueBase");
                //AnimarionToPlay = Animation.Idle;
                
                break;
            case Animation.AttaqueCroll:
                myAnim.Play("AttaqueCroll");
                break;

            case Animation.AttaqueJump:
                myAnim.Play("AttaqueJump");
                break;
        }
    }


    private void UpdateBoxCollider()
    {
        
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        Sprite sprt = GetComponent<SpriteRenderer>().sprite; // passer par animation mais ne règle pas le problem 
        col.offset = new Vector2(0,0);
        col.size = sprt.bounds.size;
        
    }

    private void FixedUpdate()
    {
       
        EtatChute();
        
    }

    // Update is called once per frame
    void Update()
    {
        InputControleur(); // devra me donner les input appuier
        if(transform.position.y < -10 || Hp <= 0)
        {
            GameManager.GetComponent<GameManagerScript>().setEndGame(false);
        }
    }

    private void InputControleur()
    {
        if (Input.GetKeyUp(KeyCode.D))
        {
            StopMove();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            StopMove();
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            releasedJumpButton = true;
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            Croll();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            Redresse();
        }
        if(Input.GetKeyDown(KeyCode.M))
        {
            Attaque();
        }
        timeBetweenInput -= Time.deltaTime;
        if(timeBetweenInput <= 0 && doubleInputID !=0)
        {
            doubleInputID = 0;
        }
        if (AnimationIsPlaying("AttaqueCroll") || AnimationIsPlaying("AttaqueBase") || AnimationIsPlaying("AttaqueJump"))
        {
            if (AnimationIsPlaying("AttaqueCroll"))
            {
                Move();
            }
            dealDamage = true;
        }
        else
        {
            dealDamage = false;
        }
        //Debug.Log(AnimarionToPlay);
    }
    
    private void Attaque() {
        dealDamage = true;
        if(EtatPerso == ETAT.Neutre && !AnimationIsPlaying("Player_Run"))
        {
            myAnim.Play("AttaqueBase");
        }
        else if (EtatPerso == ETAT.Intengible || EtatPerso == ETAT.EnHauteur)
        {
            myAnim.Play("AttaqueJump");
        }
        else if(EtatPerso == ETAT.Acroupi)
        {
            myAnim.Play("AttaqueCroll");
        }

    }

    private void Redresse()
    {
        if (EtatPerso == ETAT.Intengible)
        {
            EtatPerso = ETAT.EnHauteur;
        }
        else if(EtatPerso == ETAT.Acroupi)
        {
            AnimationPlayer(Animation.Idle);
            EtatPerso = ETAT.Neutre;
        }
    }

    private void Croll()
    {
        if (doubleInputID == 1)
        {
            doubleInputID = 2;
            EtatPerso = ETAT.Intengible;
        }
        else
        {
            if (EtatPerso == ETAT.EnHauteur)
            {
                EtatPerso = ETAT.Intengible;
                
            }
            else if (EtatPerso == ETAT.Neutre)
            {
                EtatPerso = ETAT.Acroupi;
                AnimationPlayer(Animation.CrollIdel);
                doubleInputID = 1;
                timeBetweenInput = 1;
            }
        }
    }

    private void StopMove()
    {
        if(EtatPerso == ETAT.Neutre)
        {
            AnimationPlayer(Animation.Idle);
        }
    }

    private void Jump()
    {
        if (contJump < MAXJUMP && releasedJumpButton)
        {
            myRb.velocity = new Vector2(myRb.velocity.x, 0);
            myRb.AddForce(new Vector2(0, HAUTEUR));
            releasedJumpButton = false;
            EtatPerso = ETAT.EnHauteur;
            contJump++;
            AnimationPlayer(Animation.Jump);
        }
    }

    private void Move()
    {
        if (CanMove(direction))
        {
            this.transform.position += new Vector3(SPEED * direction, 0, 0);
        }
    }

    private void MoveLeft()
    {
        if(direction != -1)
        {
            direction = -1;
            GetComponent<SpriteRenderer>().flipX = true;
            
        }
        else
        {
            if (CanMove(direction))
            {
                if (EtatPerso == ETAT.EnHauteur || EtatPerso == ETAT.Neutre)
                {
                    this.transform.position += new Vector3(SPEED * direction, 0, 0);
                    if (EtatPerso == ETAT.Neutre)
                    {
                        AnimationPlayer(Animation.Run);
                    }
                }
            }
        }
    }

    private void MoveRight()
    {
        if (direction != 1)
        {
            direction = 1;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            if (CanMove(direction))
            {
                if (EtatPerso == ETAT.EnHauteur || EtatPerso == ETAT.Neutre)
                {
                    this.transform.position += new Vector3(SPEED * direction, 0, 0);
                    if (EtatPerso == ETAT.Neutre)
                    {
                        AnimationPlayer(Animation.Run);
                    }
                }
            }
        }
    }

    private bool CanMove(int direction)
    {
        Vector3 Vn;
        if( direction == -1)
        {
            Vn = transform.position + new Vector3(SPEED * direction - GetComponent<BoxCollider2D>().size.x / 2, 0, 0); // REUSSI 
            if (PhisicMap.GetSprite(PhisicMap.WorldToCell(Vn)) != null)
            {
                return false;
            }
            Vn += new Vector3(0, GetComponent<BoxCollider2D>().size.y / 2, 0); // HAUT
            if (PhisicMap.GetSprite(PhisicMap.WorldToCell(Vn)) != null)
            {
                return false;
            }
            Vn += new Vector3(0, -GetComponent<BoxCollider2D>().size.y, 0); // BAS
            if (PhisicMap.GetSprite(PhisicMap.WorldToCell(Vn)) != null)
            {
                return false;
            }
            return true;
        }
        else
        {
            Vn = transform.position + new Vector3(SPEED * direction + GetComponent<BoxCollider2D>().size.x/2, 0, 0); // REUSSI 
            if(PhisicMap.GetSprite(PhisicMap.WorldToCell(Vn)) != null){
                return false;
            }
            Vn += new Vector3(0, GetComponent<BoxCollider2D>().size.y / 2, 0); // HAUT
            if (PhisicMap.GetSprite(PhisicMap.WorldToCell(Vn)) != null)
            {
                return false;
            }
            Vn += new Vector3(0, -GetComponent<BoxCollider2D>().size.y, 0); // BAS
            if (PhisicMap.GetSprite(PhisicMap.WorldToCell(Vn)) != null)
            {
                return false;
            }
            return true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.GetComponent<BouleScript>() != null)
        {
            myRb.AddForce(new Vector2(1750, 0));
            Hp -= 1;
        }
    }

    public int getDoubleInput()
    {
        return doubleInputID;
    }

    public bool DealDamage()
    {
        return dealDamage;
    }

}