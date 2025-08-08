using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class Bomb : MonoBehaviour, Projectile
{
    private string name;
    private float damage;
    private float fireRate;
    private float speed;
    private float ammoCapacity;
    private float currentAmmo;
    private float reloadSpeed;
    private bool reloading;

    public Bomb()
	{
        name = "Bomb";
        damage = 9;
        fireRate = 1;
        speed = 6;
        ammoCapacity = 1;
        reloadSpeed = 3;
        currentAmmo = 1;
        reloading = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * this.speed * Time.deltaTime);
    }

    public string getName()
    {
        return this.name;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public float getDamage()
    {
        return this.damage;
    }

    public void setDamage(float damage)
    {
        this.damage = damage;
    }

    public float getFireRate()
    {
        return this.fireRate;
    }

    public void setFireRate(float fireRate)
    {
        this.fireRate = fireRate;
    }

    public float getSpeed()
    {
        return this.speed;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public float getAmmoCapacity()
    {
        return this.ammoCapacity;
    }

    public void setAmmoCapacity(float ammo)
    {
        this.ammoCapacity = ammo;
    }

    public float getCurrentAmmo()
    {
        return this.currentAmmo;
    }

    public void setCurrentAmmo(float ammo)
    {
        this.currentAmmo = ammo;
    }

    public void reduceAmmo(float ammo)
    {
        this.currentAmmo -= ammo;
    }

    public void reload()
    {
        setCurrentAmmo(getAmmoCapacity());
    }

    public float getReloadSpeed()
    {
        return this.reloadSpeed;
    }

    public void setReloadSpeed(float reloadSpeed)
    {
        this.reloadSpeed = reloadSpeed;
    }

    public bool IsReloading()
    {
        return this.reloading;
    }

    public void setReloadStatus(bool reloadStatus)
    {
        this.reloading = reloadStatus;
    }
}
