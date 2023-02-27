using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool IsHeroNearby=false;
    public float patrolDistance;
    public float startPos;
    public float currentPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        while (transform.position.x < startPos + patrolDistance)
        {
            Debug.Log("Run Right");
            rb.velocity = new Vector2(8, transform.position.y);
        }
        startPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position.x;
        if (IsHeroNearby)
            Following();
        else
            Patrol();
    }

    private void Patrol()
    {

            while (transform.position.x > startPos - patrolDistance)
            {
                Debug.Log("Run left");
                rb.velocity = new Vector2(-8, transform.position.y);
            }

            while (transform.position.x < startPos + patrolDistance)
            {
                Debug.Log("Run Right");
                rb.velocity = new Vector2(8, transform.position.y);
            }
        
    }

    private void Following()
    {
        throw new NotImplementedException();
    }
}
