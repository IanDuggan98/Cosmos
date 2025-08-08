using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class TwistyEnemy : MonoBehaviour, IEnemy
{
    public GameObject player;
    private string name;
    private string enemyType;
    private float health;
    private float speed;
    private float spawningPositionX1, spawningPositionX2;
    private float spawningPositionY1, spawningPositionY2;
    private bool lessX, lessY;
    public TwistyEnemy()
	{
        name = "Twisty";
        enemyType = "Small";
        health = 2f;
        speed = 1f;
	}

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spawningPositionX1 = player.transform.position.x - 5f;
        spawningPositionX2 = player.transform.position.x + 5f;
        spawningPositionY1 = player.transform.position.y - 5f;
        spawningPositionY2 = player.transform.position.y + 5f;

        float[] spawningPoints = { spawningPositionX1, spawningPositionX2, spawningPositionY1, spawningPositionY2 };
        var randX = (int)UnityEngine.Random.Range(0f, 2f);
        var randY = (int)UnityEngine.Random.Range(2f, 4f);

        if (spawningPoints[randX] < player.transform.position.x)
        {
            lessX = true;
        }

        if (spawningPoints[randY] < player.transform.position.y)
        {
            lessY = true;
        }

        if (lessX)
        {
            if (lessY)
            {
                transform.position = new Vector3((int)UnityEngine.Random.Range(player.transform.position.x - 10f, spawningPoints[randX]),
                    (int)UnityEngine.Random.Range(player.transform.position.y - 10f, spawningPoints[randY]), 0f);
            }
            else
            {
                transform.position = new Vector3((int)UnityEngine.Random.Range(player.transform.position.x - 10f, spawningPoints[randX]),
                    (int)UnityEngine.Random.Range(spawningPoints[randY], player.transform.position.y + 10f), 0f);
            }
        }
        else
        {
            if (lessY)
            {
                transform.position = new Vector3((int)UnityEngine.Random.Range(spawningPoints[randX], player.transform.position.x + 10f),
                    (int)UnityEngine.Random.Range(player.transform.position.y - 10f, spawningPoints[randY]), 0f);
            }
            else
            {
                transform.position = new Vector3((int)UnityEngine.Random.Range(spawningPoints[randX], player.transform.position.x + 10f),
                    (int)UnityEngine.Random.Range(spawningPoints[randY], player.transform.position.y + 10f), 0f);
            }
        }
    }

    void Update()
    {
        float step = this.speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
        transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(-20.0f, 20.0f)));

        checkState();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Laser"))
        {
            this.takeDamage(1f);
            Globals.score += 5;
        }

        if (collider.gameObject.CompareTag("Bomb"))
        {
            this.takeDamage(9f);
            Globals.score += 10;
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }

    private void checkState()
    {
        if (this.health < 0f)
        {
            Destroy(gameObject);
        }
    }

    public string getName()
    {
        return this.name;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public string getEnemyType()
    {
        return this.enemyType;
    }

    public void setEnemyType(string enemyType)
    {
        this.enemyType = enemyType;
    }

    public float getSpeed()
    {
        return this.speed;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public float getHealth()
    {
        return this.health;
    }

    public void setHealth(float health)
    {
        this.health = health;
    }
}
