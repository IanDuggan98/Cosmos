using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

public class GrappleHook : MonoBehaviour
{
	public GameObject playerShip;
	public GameObject spawnPoint;

    public GameObject attachedEnemy;

	private FixedJoint2D joint;
	private bool isShot;
	private bool isAttached;
	public GrappleHook()
	{
		isShot = false;
		isAttached = false;
	}

	void Update()
	{
		transform.rotation = playerShip.transform.rotation;
		transform.position = spawnPoint.transform.position;

        FixedJoint2D currentjoint = this.GetComponent<FixedJoint2D>();
        if (currentjoint == null)
        {
            SetIsAttached(false);
        }

        if (attachedEnemy.IsDestroyed())
        {
            SetIsAttached(false);
        }

		//Debug.Log("Current joint connected body is " + joint.connectedBody.ToString());

		//if (joint.connectedBody)
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("TwistyEnemy") && GetIsAttached() == false)
        {
            Debug.Log("Attaching to a twisty enemy");
            attachedEnemy = collision.gameObject;
            this.Attach(collision);
        }

        if (collision.gameObject.CompareTag("ZoomyEnemy") && GetIsAttached() == false)
        {
            Debug.Log("Attaching to a zoomy enemy");
            attachedEnemy = collision.gameObject;
            this.Attach(collision);
        }

        if (collision.gameObject.CompareTag("TankyEnemy") && GetIsAttached() == false)
        {
            Debug.Log("Attaching to a tanky enemy");
            attachedEnemy = collision.gameObject;
            this.Attach(collision);
        }
    }

    //void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.gameObject.CompareTag("TwistyEnemy") && GetIsAttached() == false)
    //    {
    //        this.Attach(collider);
    //    }

    //    if (collider.gameObject.CompareTag("ZoomyEnemy") && GetIsAttached() == false)
    //    {
    //        this.Attach(collider);
    //    }

    //    if (collider.gameObject.CompareTag("TankyEnemy") && GetIsAttached() == false)
    //    {
    //        this.Attach(collider);
    //    }
    //}

    public bool GetIsShot()
	{
		return this.isShot;
	}

	public void SetIsShot(bool isShot)
	{
		this.isShot = isShot;
	}

    public bool GetIsAttached()
    {
        return this.isAttached;
    }

    public void SetIsAttached(bool isAttached)
    {
		this.isAttached = isAttached;
    }

	public void Attach(Collision2D col)
	{
        joint = this.AddComponent<FixedJoint2D>();

        joint.anchor = col.contacts[0].point;

        joint.connectedBody = col.contacts[0].collider.transform.GetComponentInParent<Rigidbody2D>();

        joint.enableCollision = false;

        SetIsAttached(true);
	}
}
