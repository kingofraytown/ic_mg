﻿using UnityEngine;
using System.Collections;
public class Homing : MonoBehaviour
{
//js
/*var _velocity : float = 10;
var _torque: float = 5;
var _target : Transform;
var _rigidbody : Rigidbody;
*/

//cs
    public float lifetime = 10f; 
    public float missileVelocity = 0f;
    private float originalVelocity;
    public float turn = 5f;
    public float fuseDelay;
    public Rigidbody homingMissile;
    public GameObject missileMod;
    public ParticleSystem smokePrefab;
    public bool hit = false;
    private Transform target;
    public float startTime;
    private bool speedMatched;
    public bool activeMissile = true;
    public bool flying = false;
    public float eRate; 
    public int updateCount = 30;
    public string parentName = "un named";

    public delegate void MissileDelegate(missileState newState);
    public static event MissileDelegate missleEvent;

    public enum missileState
    {
        hit,
        miss
    };

    void OnEnable()
    {
        playerController.resetEvent += resetToOrigin;
        // tiltListener.tiltEvent += MoveByZ;
        //PlayerStateController.onStateChange += onStateChange;
    }

    void OnDisable()
    {
        playerController.resetEvent -= resetToOrigin;
        //PlayerStateController.onStateChange -=
        //  onPlayerStateChange;
    }
    void Start()
    {
        //this.transform.position = transform.parent.transform.position;
        //this.transform.rotation = transform.parent.transform.rotation;
        eRate = smokePrefab.emissionRate;
        startTime = Time.time;
        homingMissile = transform.rigidbody;
        originalVelocity = missileVelocity;
    Fire();
        //GameObject go = GameObject.FindGameObjectWithTag("Player");
        //target = go.transform; 
    }

    void Fire()
    {
        flying = true;
        float distance = Mathf.Infinity;
    //var distance = Mathf.Infinity;
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player"))
        {
            float diff = (go.transform.position - transform.position).sqrMagnitude;

            if(diff < distance)
            {
                distance = diff;
                target = go.transform;
            }
        }
        smokePrefab.emissionRate = eRate;
    }

    void LateUpdate()
    {
        //print("Missile velocity = " + missileVelocity);

    }

    void FixedUpdate()
    {
        //if (updateCount < 0)
        //{
            float dist = Vector3.Distance(target.position, transform.position);
            //print("distance = " + dist);
            if (target == null || homingMissile == null)
            {
                return;
            }
            homingMissile.velocity = transform.forward * missileVelocity;

            /*if (dist < 15)
            {
                missileVelocity = 40f;
                speedMatched = true;
            }

            if (missileVelocity != (originalVelocity * 0.8f) && speedMatched)
            {
                missileVelocity += 1.5f;
            }*/
            //print("missile velocity " + missileVelocity);

        float newZ = homingMissile.transform.position.z;
            //print("missils z scale = " + newZ);
            //print("missils z position = " + homingMissile.transform.position.z);
            //newZ += 0.5f;
            //scale capping

            if (newZ > 0)
            {
                float tz = newZ * 0.0033f;
                newZ = 1 - tz;
            } 
            else if (newZ < 0)
            {
                float tz = newZ * -0.01f;
                newZ = tz;
            }
            
            //newZ *= -1f;
            //gameObject.transform.localScale = (new Vector3(newZ, newZ, newZ));
            //print("Local Z = " + newZ);
            /*if ((Time.time - startTime) < lifetime - 3f)
        { 
            missileVelocity += 0.1f;
        }*/

            if ((Time.time - startTime) < lifetime - 2f)
            { 
                Quaternion targetRotation = new Quaternion();
                targetRotation = Quaternion.LookRotation(target.position - transform.position);
                homingMissile.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turn));
            }



            if ((Time.time - startTime) > lifetime && hit == false)
            {
                missleEvent(missileState.miss);
                destroyMissile();

            }

        if (homingMissile.transform.position.z < -142)
        {
            int a;
            int b;
            a = 2;
            b = 4;
            int c = a + b;
        }
            //updateCount = -1;
        //}

        //updateCount--;
       // print(gameObject.name + "pos_x= " + transform.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "player1")
        {
            hit = true;
            missleEvent(missileState.hit);
            destroyMissile();
            //DestroyImmediate(gameObject);
        }

    }

    public void destroyMissile()
    {
        //activeMissile = false;
        //flying = true;
        //print("inside destroy missile");
        //smokePrefab.emissionRate = 0.0f;
        gameObject.SetActive(false); 
        //Destroy(missileMod);
        //yield WaitForSeconds(5);
        //Destroy(gameObject, 0.01f);
    }

    public void restoreMissile()
    {
        activeMissile = true;
        flying = false;
        smokePrefab.emissionRate = eRate;
        this.missileMod.SetActive(true);

        Fire();
    }

    public void resetToOrigin(float x)
    {
        Vector3 pos = transform.position;
        
        transform.position = new Vector3(pos.x - x, pos.y, pos.z);
        //print(gameObject.name + " reset position");
    }
}