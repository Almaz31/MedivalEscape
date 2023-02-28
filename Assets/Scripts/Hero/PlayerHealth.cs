using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private int playerHealth;
    public TMP_Text healthText;
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: "+playerHealth.ToString();
        CheckLife();
    }

    private void CheckLife()
    {
        if (playerHealth <= 0)
           StartCoroutine( LoseWithDelay());
                  
    }

    public void TakeDamage(int numberOfDamge)
    {
        if(playerHealth>=0)
            playerHealth -= numberOfDamge;
    }
    IEnumerator LoseWithDelay()
    {
        LoseEvent loseEvent = canvas.GetComponent<LoseEvent>();
        yield return new WaitForSeconds(1);
        if (loseEvent != null)
            loseEvent.Lose();
        StopCoroutine(LoseWithDelay());
    }
}
