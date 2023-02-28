using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform pointA; 
    public Transform pointB; 
    public float speed = 2f; 

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool IsHeroNearby;
    private bool IsFaceRight = true;

    private Transform HeroTransform;
    public float startFollowingDistance = 10f;

    public float attackRadius = 1f;
    public int damage = 10;
    public float attackDelay = 2f;

    private float attackTimer;
    private GameObject player;
    private Animator anim;
    float distance;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        HeroTransform = player.GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        distance = Vector2.Distance(rb.position, HeroTransform.position);
        CheckFlip();
        if (HeroTransform != null)
        {
            CheckHeroNearby();
            CheckAttackDistance();
        }
    }
    private void FixedUpdate()
    {
        if (IsHeroNearby)
            Following();
        else
            Patrol();
    }
    private void Patrol()
    {
        float distanceToPoint = Vector2.Distance(transform.position, pointB.position);

        if (distanceToPoint > 0.1f)
        {
            Vector2 direction = (pointB.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
        else
        {
            Vector2 direction = (pointA.position - transform.position).normalized;
            rb.velocity = direction * speed;
            Transform temp = pointA;
            pointA = pointB;
            pointB = temp;
        }
    }
    private void Following()
    {
        if (distance > 1f)
        {
            Vector2 direction = new Vector2(HeroTransform.position.x - rb.position.x, 0f).normalized;
            rb.velocity = direction * speed;
        }
        else
            rb.velocity = new Vector2(0,0);


    }
    void Flip()
    {
        IsFaceRight = !IsFaceRight;
        sr.flipX = !sr.flipX;
    }
    private void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        if(playerHealth != null)
            playerHealth.TakeDamage(damage);

        anim.SetTrigger("Attack");

    }
    private void CheckFlip()
    {
        if (rb.velocity.x > 0 && !IsFaceRight || rb.velocity.x < 0 && IsFaceRight)
            Flip();
    }
    void CheckHeroNearby()
    {
        if (distance < startFollowingDistance)
            IsHeroNearby = true;
        else
            IsHeroNearby = false;
    }
    void CheckAttackDistance()
    {
        if (distance <= attackRadius)
        {
            if (attackTimer <= 0f)
            {
                AttackPlayer();
                attackTimer = attackDelay;
            }
            else
                attackTimer -= Time.deltaTime;
        }
    }
}
