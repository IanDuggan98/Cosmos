using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ZoomyEnemy : MonoBehaviour, IEnemy
{
    private string name;
    private string enemyType;
    public GameObject player;
    private float health;
    private float speed;
    private Vector2 toward, halfangle;
    private float spawningPositionX1, spawningPositionX2;
    private float spawningPositionY1, spawningPositionY2;
    private bool lessX, lessY;
    public ZoomyEnemy()
    {
        name = "Zoomy";
        enemyType = "Medium";
        health = 3f;
        speed = 5f;
    }
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spawningPositionX1 = player.transform.position.x - 10f;
        spawningPositionX2 = player.transform.position.x + 10f;
        spawningPositionY1 = player.transform.position.y - 10f;
        spawningPositionY2 = player.transform.position.y + 10f;

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
        transform.localRotation = calculateRotation();
        
        checkState();
    }

    private Quaternion calculateRotation()
    {
        Quaternion q;

        toward = (transform.position - player.transform.position);
        toward.Normalize();
        halfangle = new Vector2(2f + (2f * toward.x) + toward.y, 1f - toward.x + (2f * toward.y));
        halfangle.Normalize();
        q = new Quaternion(0f, 0f, halfangle.y, halfangle.x);

        return q;
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
            Globals.score += 50;
        }

        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.takeDamage(5f);
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
