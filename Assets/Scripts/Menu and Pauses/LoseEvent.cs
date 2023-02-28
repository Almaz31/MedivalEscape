using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseEvent : MonoBehaviour
{
    public GameObject loseMessage;
    public void Lose()
    {
        Time.timeScale = 0f;
        loseMessage.SetActive(true);
    }

}
