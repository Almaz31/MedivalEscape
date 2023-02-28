using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Animator animGround;
    private SpriteRenderer sr;
    [Header("For change heros")]
    [SerializeField]private int _currentHeroId ;
    private GameObject _currentHero;
    public List<GameObject> spritesOfcharacters;
    [Header("For Movement")]
    public float Speed;
    public float _kofSpeed;
    private bool FaceIsRight=true;
    private float move;
    [Header("For Jump")]
    private bool IsGrounded;
    [SerializeField] private float groundCheckRadius;
    public float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask Ground;
    private bool DestroyMode = false;


    // Start is called before the first frame update
    void Start()
    {
        _kofSpeed = 1;
        _currentHeroId = 1;

        rb = GetComponent<Rigidbody2D>();
        animGround = GetComponentInChildren<Animator>();
        sr = spritesOfcharacters[_currentHeroId].GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckButtonsDown();
    }

    private void FixedUpdate()
    {
        Movement();
        NeedFlip();
        GroundCheck();
    }
    void CheckButtonsDown()
    {
        if (Input.GetKeyDown("e"))
            ChangeCharacter();
        if (Input.GetKeyDown("space"))
            CastHeroSkill();
        if (Input.GetKeyUp("space"))
        {
            DestroyMode = false;
            _kofSpeed = 1;
        }
    }
    void GroundCheck()
    {
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, Ground);
        animGround.SetBool("IsGrounded", IsGrounded);
    }
    private void Movement()
    {
        move = Input.GetAxis("Horizontal");
        foreach(GameObject hero in spritesOfcharacters)
        {
            anim = GetComponentInChildren<Animator>();
            anim.SetFloat("move", Mathf.Abs(move));
        }
        
        rb.velocity = new Vector2(move * Speed*_kofSpeed, rb.velocity.y);
    }
    private void NeedFlip()
    {
        if (move > 0 && !FaceIsRight)
            Flip();
        else if (move<0 && FaceIsRight)
            Flip();
    } 
    private void Flip()
    {
        FaceIsRight = !FaceIsRight;
        sr.flipX = !sr.flipX;
    }
    void CastHeroSkill()
    {
        switch (_currentHeroId)
        {
            case 0:
                BeardmanSkill();
                break;
            case 1:
                Jump();
                break;
            case 2:
                Debug.Log("Woman casrt skill");
                break;
            case 3:
                Debug.Log("Oldman casrt skill");
                break;
        }
    }

    void ChangeCharacter()
    {
        _currentHero = spritesOfcharacters[_currentHeroId];
        _currentHero.SetActive(false);
        if (!FaceIsRight)
            Flip();

        if (_currentHeroId != spritesOfcharacters.Count - 1)
            _currentHeroId += 1;
        else
            _currentHeroId = 0;

        _currentHero = spritesOfcharacters[_currentHeroId];
        _currentHero.SetActive(true);
        FaceIsRight = true;
        sr = _currentHero.GetComponent<SpriteRenderer>();
    }
   public void Jump()
    {
        if (IsGrounded)
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

    }
    private void BeardmanSkill()
    {
        _kofSpeed = 0.5f;
        DestroyMode = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CanBeDestroyed") && DestroyMode)
        {
            DestroyTraps(collision);
        }
    }
    void DestroyTraps(Collision2D collision)
    {
        DestroyObjects ds = collision.gameObject.GetComponent<DestroyObjects>();
        ds.DestroyObject();
    }
}

