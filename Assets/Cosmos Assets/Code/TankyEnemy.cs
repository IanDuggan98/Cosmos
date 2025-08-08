using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class TankyEnemy : MonoBehaviour, IEnemy
{
    public GameObject player;
    private string name;
    private string enemyType;
    private float health;
    private float speed;
    private float rotateSpeed;
    private float spawningPositionX1, spawningPositionX2;
    private float spawningPositionY1, spawningPositionY2;
    private bool lessX, lessY;
    public TankyEnemy()
	{
        name = "Tanky";
        enemyType = "Large";
        health = 10f;
        speed = 0.5f;
        rotateSpeed = 0.5f;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spawningPositionX1 = player.transform.position.x - 10f;
        spawningPositionX2 = player.transform.position.x + 10f;
        spawningPositionY1 = player.transform.position.y - 10f;
        spawningPositionY2 = player.transform.position.y + 10f;

        float[] spawningPoints = {spawningPositionX1, spawningPositionX2, spawningPositionY1, spawningPositionY2};
        var randX = (int) UnityEngine.Random.Range(0f, 2f);
        var randY = (int) UnityEngine.Random.Range(2f, 4f);

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
                transform.position = new Vector3((int)UnityEngine.Random.Range(player.transform.position.x - 20f, spawningPoints[randX]),
                    (int)UnityEngine.Random.Range(player.transform.position.y - 20f, spawningPoints[randY]), 0f);
            }
            else
            {
                transform.position = new Vector3((int)UnityEngine.Random.Range(player.transform.position.x - 20f, spawningPoints[randX]),
                    (int)UnityEngine.Random.Range(spawningPoints[randY], player.transform.position.y + 20f), 0f);
            }
        }
        else
        {
            if (lessY)
            {
                transform.position = new Vector3((int)UnityEngine.Random.Range(spawningPoints[randX], player.transform.position.x + 20f),
                    (int)UnityEngine.Random.Range(player.transform.position.y - 20f, spawningPoints[randY]), 0f);
            }
            else
            {
                transform.position = new Vector3((int)UnityEngine.Random.Range(spawningPoints[randX], player.transform.position.x + 20f),
                    (int)UnityEngine.Random.Range(spawningPoints[randY], player.transform.position.y + 20f), 0f);
            }
        }
    }

    void Update()
    {
        float step = this.speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
        transform.Rotate(new Vector3(0, 0, rotateSpeed));

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y)) < 10f)
        {
            rotateSpeed = 1f;
            speed = 1f;
        }
        else
        {
            rotateSpeed = 0.5f;
            speed = 0.5f;
        }
        checkState();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Laser"))
        {
            this.takeDamage(1f);
            Globals.score += 10;
        }

        if (collider.gameObject.CompareTag("Bomb"))
        {
            this.takeDamage(9f);
            Globals.score += 100;
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
