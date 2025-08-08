using System;
using UnityEngine;

public interface IEnemy
{
    public string getName();
    public void setName(string name);

    public string getEnemyType();
    public void setEnemyType(string enemyType);

    public float getSpeed();
    public void setSpeed(float speed);

    public float getHealth();
    public void setHealth(float health);

    public void takeDamage(float damage);
}
