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
    [Header("For change heros")]
    [SerializeField]private int _currentHeroId ;
    private GameObject _currentHero;
    public List<GameObject> spritesOfcharacters;
    [Header("For Movement")]
    public float Speed;
    public float _kofSpeed;
    [SerializeField] private bool FaceIsRight=true;
    [SerializeField] private float move;
    public int Counter;
    [Header("For Jump")]
    [SerializeField] private bool IsGrounded;
    [SerializeField] private float GroundCheckRadius;
    public float JumpForce;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask Ground;
    public bool DestroyMode = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _currentHeroId = 1;
        animGround = GetComponentInChildren<Animator>();
        _kofSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
            ChangeCharacter();
        if (Input.GetKeyDown("space"))
            CurrentHeroSkill();
        if (Input.GetKeyUp("space"))
        {
            DestroyMode = false;
            _kofSpeed = 1;
        }
    }

    private void FixedUpdate()
    {
        Movement();
        NeedFlip();
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, Ground);
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
            Vector2 scale = spritesOfcharacters[_currentHeroId].transform.localScale;
            scale.x *= -1;
            spritesOfcharacters[_currentHeroId].transform.localScale = scale;
    }
    void CurrentHeroSkill()
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
    }
   public void Jump()
    {
        if (IsGrounded)
            rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);

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
            DestroyObjects ds = collision.gameObject.GetComponent<DestroyObjects>();
            ds.DestroyObject();  
        }
    }
}

