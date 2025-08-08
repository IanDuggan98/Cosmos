using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.Users;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class PlayerGameplayScript : MonoBehaviour
{
    public GameObject player;
    public GameObject currentPrimary;
    public GameObject currentSecondary;
    public GameObject currentSupport;
    public GameObject projectileSpawn1;
    public GameObject projectileSpawn2;
    public GameObject projectileSpawn3;
    public GameObject projectileSpawn4;

    public Bomb bomb;
    public Laser laser;
    //public GrappleHook grappleHook;

    private GameObject primaryFire1;
    private GameObject primaryFire2;
    private GameObject primaryFire3;
    private GameObject secondaryFire;
    //private GameObject supportFire;

    private Vector3 playerMovement;
    private float currentAngle;
    private String currentControlScheme;
    
    public Camera cam;
    public float speed;

    bool primaryHold;
    bool secondaryHold;
    bool dualFire;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        speed = 5f;
        currentAngle = 0f;
        currentControlScheme = "KeyboardMouse";
        //supportFire = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        player.transform.position += playerMovement * Time.deltaTime * speed;
        if (currentControlScheme == "KeyboardMouse")
        {
            faceMouse();
        }
        setCamToPlayer();
    }

    public void OnControlsChanged(PlayerInput value)
    {
        currentControlScheme = value.currentControlScheme;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        var v = context.ReadValue<Vector2>();
        playerMovement = new Vector3(v.x, v.y, 0);
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        var v = context.ReadValue<Vector2>();
        float angleRadians = Mathf.Atan2(v.y, v.x);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        if (angleDegrees != 0)
        {
            currentAngle = angleDegrees;
        }

        transform.rotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
    }

    public void OnShoot_primary(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (currentPrimary.CompareTag("Laser"))
            {
                if (laser.getCurrentAmmo() > 0)
                {
                    if (context.interaction is SlowTapInteraction)
                    {
                        primaryHold = true;
                    }
                    if (context.interaction is TapInteraction)
                    {
                        primaryHold = false;
                    }

                    if (primaryHold)
                    {
                        StartCoroutine(holdPrimaryFire(laser));
                    }
                    else
                    {
                        singlePrimaryFire(laser);
                    }
                }
            }

        }
        else 
        {
            if (primaryHold)
            {
                primaryHold = false;
            }
        }

        if (!laser.IsReloading() && laser.getCurrentAmmo() == 0)
        {
            StartCoroutine(Reload(laser, laser.getReloadSpeed()));
        }
    }

    public void OnShoot_secondary(InputAction.CallbackContext context)
    {
        if (currentSecondary.CompareTag("Bomb"))
        {
            if (bomb.getCurrentAmmo() > 0)
            {
                secondaryFire = Instantiate(currentSecondary, projectileSpawn1.transform.position, transform.rotation) as GameObject;

                secondaryFire.transform.localRotation = player.transform.localRotation;

                bomb.reduceAmmo(1);

                if (bomb.getCurrentAmmo() == 0)
                {
                    StartCoroutine(Reload(bomb, bomb.getReloadSpeed()));
                }

                StartCoroutine(DestroyProjectile(secondaryFire, 5));
            }
            else
            {
                if (!bomb.IsReloading())
                {
                    StartCoroutine(Reload(bomb, bomb.getReloadSpeed()));
                }
            }
        }

    }
    public void OnShoot_support(InputAction.CallbackContext context)
    {
        
    }

    private void faceMouse()
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = -cam.transform.position.z;

        Vector3 point = cam.ScreenToWorldPoint(screenPoint);

        float t = cam.transform.position.x / (cam.transform.position.x - point.x);

        Vector3 finalPoint = new Vector3(t * (point.x - cam.transform.position.x) + cam.transform.position.x, t * (point.y - cam.transform.position.y) + cam.transform.position.y, 1);

        Vector2 halfangle, toward;
        Quaternion q;
        toward = (point - transform.position);
        toward.Normalize();
        halfangle = new Vector2(2f + (2f * toward.x) + toward.y, 1f - toward.x + (2f * toward.y));
        halfangle.Normalize();
        q = new Quaternion(0f, 0f, halfangle.y, halfangle.x);
        transform.localRotation = q;
    }

    private void setCamToPlayer()
    {
        cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -14);
    }
    private void singlePrimaryFire(Projectile primary)
    {
        primaryFire1 = Instantiate(currentPrimary, projectileSpawn1.transform.position, transform.rotation) as GameObject;
        primaryFire1.transform.localRotation = player.transform.localRotation;

        laser.reduceAmmo(1);

        StartCoroutine(DestroyProjectile(primaryFire1, 1f));
    }

    private IEnumerator holdPrimaryFire(Projectile primary)
    {
        while (primaryHold)
        {
            if (primary.getCurrentAmmo() > 0)
            {
                primaryFire2 = Instantiate(currentPrimary, projectileSpawn2.transform.position, transform.rotation) as GameObject;
                primaryFire2.transform.localRotation = player.transform.localRotation;

                primaryFire3 = Instantiate(currentPrimary, projectileSpawn3.transform.position, transform.rotation) as GameObject;
                primaryFire3.transform.localRotation = player.transform.localRotation;


                primary.reduceAmmo(1);

                if (primary.getCurrentAmmo() == 0)
                {
                    StartCoroutine(Reload(primary, primary.getReloadSpeed()));
                }

                StartCoroutine(DestroyProjectile(primaryFire2, 1f));
                StartCoroutine(DestroyProjectile(primaryFire3, 1f));
                yield return new WaitForSeconds(0.5f);
            }
            
        }
    }

    private IEnumerator DestroyProjectile(GameObject projectile, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(projectile);
    }

    private IEnumerator Reload(Projectile projectile, float reloadSpeed)
    {
        if (projectile.getName() == "Bomb")
        {
            bomb.setReloadStatus(true);
            yield return new WaitForSeconds(reloadSpeed);
            bomb.reload();
            bomb.setReloadStatus(false);
        }
        if (projectile.getName() == "Laser")
        {
            laser.setReloadStatus(true);
            yield return new WaitForSeconds(reloadSpeed);
            laser.reload();
            laser.setReloadStatus(false);
        }
    }

    //private IEnumerator ExtendGrappleHook(GameObject hook, float duration)
    //{
    //    var startScale = hook.transform.localScale;
    //    Vector3 scale1 = new Vector3(2f, 0.1f, 0.1f);
    //    var endScale = scale1;
    //    var elapsed = 0f;

    //    if (grappleHook.GetIsAttached() == false)
    //    {
    //        while (elapsed < duration)
    //        {
    //            var t = elapsed / duration;

    //            hook.transform.localScale = Vector3.Lerp(startScale, endScale, t);
    //            elapsed += Time.deltaTime;
    //            yield return null;
    //        }
    //        hook.transform.localScale = endScale;
    //    }

    //    //Debug.Log("Attached enemy is destroyed: " + grappleHook.attachedEnemy.IsDestroyed().ToString());
        
    //    if (grappleHook.GetIsAttached() == false)
    //    {
    //        grappleHook.SetIsAttached(false);
    //        StartCoroutine(RetractGrappleHook(hook, duration));
    //    }
    //}

    //private IEnumerator RetractGrappleHook(GameObject hook, float duration)
    //{
    //    var startScale = hook.transform.localScale;
    //    Vector3 scale1 = new Vector3(0.01f, 0.1f, 0.1f);
    //    var endScale = scale1;
    //    var elapsed = 0f;

    //    while (elapsed < duration)
    //    {
    //        var t = elapsed / duration;

    //        hook.transform.localScale = Vector3.Lerp(startScale, endScale, t);
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }
    //    hook.transform.localScale = endScale;
    //    grappleHook.SetIsShot(false);
    //    StartCoroutine(DestroyProjectile(hook, 0.1f));
    //}
}
