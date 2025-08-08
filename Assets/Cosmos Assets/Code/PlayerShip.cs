using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class PlayerShip : MonoBehaviour
{
	public float health;
	public float shields;
    private bool isAlive;

	public PlayerShip()
	{
		health = 100f;
		shields = 100f;
        isAlive = true;
	}

	void Update()
	{
		if (health < 100f && health >= 0.1f)
		{
            health += 0.01f;
        }
        if (health > 100f)
        {
            health = 100f;
        }
        checkState();
	}

    private float getHealth()
    {
        return this.health;
    }

    private void setHealth(float health)
    {
        this.health = health;
    }

    private float getShields()
    {
        return this.shields;
    }

    private void setShields(float shields)
    {
        this.shields = shields;
    }

    public void takeDamage(float damage)
    {
        if (this.shields > 0f)
        {
            this.shields -= damage;
            if (this.shields < 0f)
            {
                setShields(0f);
            }
        } else {
            this.health -= damage;
            if (this.health < 0f)
            {
                setHealth(0f);
            }
        }
    }

    private void checkState()
    {
        if (this.shields == 0f && this.health == 0f)
        {
            Debug.Log("Dead");
            this.isAlive = false;
        }

        if (!isAlive)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Application.Quit();
        }
    }
}
