using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public interface Projectile
{
    public string getName();
    public void setName(string name);
    public float getDamage();
    public void setDamage(float damage);
    public float getFireRate();
    public void setFireRate(float fireRate);
    public float getSpeed();
    public void setSpeed(float speed);
    public float getAmmoCapacity();
    public void setAmmoCapacity(float ammo);
    public float getCurrentAmmo();
    public void setCurrentAmmo(float ammo);
    public void reduceAmmo(float ammo);
    public void reload();
    public float getReloadSpeed();
    public void setReloadSpeed(float reloadSpeed);
    public bool IsReloading();
    public void setReloadStatus(bool reloadStatus);
}
