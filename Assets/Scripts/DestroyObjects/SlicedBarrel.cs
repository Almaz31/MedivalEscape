using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlicedBarrel : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]private Vector2 forceiDirection;
    [SerializeField] private int spin;
    float TimetoDestroy;
    public SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(255f, 255f, 255f, 1);
        TimetoDestroy = 0f;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(forceiDirection);
        rb.AddTorque(spin);
        
        
    }

    void Update()
    {
        TimetoDestroy += Time.deltaTime;
        if (TimetoDestroy > 10)
        {
            Destroy(this.gameObject);
            TimetoDestroy = 0f;
        }
             
        sr.color = new Color(255f, 255f, 255f, 1 / (TimetoDestroy / 2));
        
    }
}
