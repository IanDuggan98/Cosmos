using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class PlayerShootingScript : MonoBehaviour
{
    public GameObject player;
    public GameObject primaryProjectile;
    public GameObject secondaryProjectile;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        checkInputs();
    }

    private void checkInputs()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            firePrimary();
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            fireSecondary();
        }

        if (Input.GetKey(KeyCode.Mouse2))
        {
            fireSupport();
        }
    }

    private void firePrimary() 
    {
        var primaryFire = Instantiate(primaryProjectile, transform.position, transform.rotation) as GameObject;

        primaryFire.transform.localRotation = player.transform.localRotation;

        StartCoroutine(DestroyProjectile(primaryFire, 3));
    }

    private void fireSecondary()
    {
        var secondaryFire = Instantiate(secondaryProjectile, transform.position, transform.rotation) as GameObject;

        secondaryFire.transform.localRotation = player.transform.localRotation;

        StartCoroutine(DestroyProjectile(secondaryFire, 5));
    }

    private void fireSupport()
    {
        
    }
    private IEnumerator DestroyProjectile(GameObject projectile, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(projectile);
    }
}
