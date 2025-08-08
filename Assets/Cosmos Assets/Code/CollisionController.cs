using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class CollisionController : MonoBehaviour
{
    public PlayerShip playerShip;

    void Start()
    {
        Debug.Log("Collision controller started");
    }


    void Update()
    {
        //Debug.Log("Collision controller running");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Entered OnCollisionEnter2D");
        if (collision.gameObject.CompareTag("TwistyEnemy"))
        {
            playerShip.takeDamage(1f);
        }
        if (collision.gameObject.CompareTag("ZoomyEnemy"))
        {
            playerShip.takeDamage(5f);
        }
        if (collision.gameObject.CompareTag("TankyEnemy"))
        {
            playerShip.takeDamage(10f);
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            //Debug.Log("Stay enemy");
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            //Debug.Log("Exit enemy");
        }
    }
}
