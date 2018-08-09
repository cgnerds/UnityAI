﻿using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float fireSpeed = 3f;
    private float fireCounter = 0f;
    private bool canFire = true;

    [SerializeField]
    private Transform muzzle;
    [SerializeField]
    private GameObject projectile;

    private bool isLockedOn = false;

    public bool LockedOn
    {
        get { return isLockedOn; }
        set { isLockedOn = value; }
    }
    
	// Update is called once per frame
	void Update () {
		if(LockedOn && canFire)
        {
            StartCoroutine(Fire());
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            animator.SetBool("TankInRange", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            animator.SetBool("TankInRange", false);
        }
    }

    private void FireProjecile()
    {
        GameObject bullet = Instantiate(projectile, muzzle.position, muzzle.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().AddForce(muzzle.forward * 300);
    }

    private IEnumerator Fire()
    {
        canFire = false;
        FireProjecile();
        while (fireCounter < fireSpeed)
        {
            fireCounter += Time.deltaTime;
            yield return null;
        }

        canFire = true;
        fireCounter = 0f;
    }
}
