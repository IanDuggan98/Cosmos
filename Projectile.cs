using System;

public interface Projectile
{
    private float damage;
    private float fireRate;
    private float speed;
    private float ammoCapacity;
    private float currentAmmo;
    private float reloadSpeed;

    private float getDamage();
    private void setDamage(float damage);

    private float getFireRate();

    private void setFireRate(float fireRate);

    private float getSpeed();

    private void setSpeed(float speed);

    private float getAmmoCapacity();
    private void setAmmoCapacity(float ammo);
    public float getCurrentAmmo();

    private void setCurrentAmmo(float ammo);

    public void reduceAmmo(float ammo);

    public void reload();

    public float getReloadSpeed();

    private void setReloadSpeed(float reloadSpeed);
}
