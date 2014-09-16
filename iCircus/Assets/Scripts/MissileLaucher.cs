﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissileLaucher : MonoBehaviour {

    public GameObject missile;
    public GameObject[] missileArray;
    private Homing homey;
    private Homing deadHomey;
    public int missileArraySize = 5;
    public float velocity = 40f;
    public float missileTurn = 8f;
    public float missileLifeTime = 8f;
    public bool ready = false;
    int gatherCount = 100;

    //public List <GameObject> missilesList = new List<GameObject>();

	// Use this for initialization
	void Start () {
        ready = false;
	
        /*missileArray = new GameObject[missileArraySize];    
        for (int i = 0; i < missileArraySize; i++)
        {

            missileArray[i] = Instantiate(missile,transform.position, transform.rotation) as GameObject;
            if(missileArray[i] == null)
                print("nope");
            //missileArray[i].transform.parent = this.transform;
            missileArray[i].SetActive(false);
        }*/
	}

    public bool GetReady()
    {
        missileArray = new GameObject[missileArraySize];    
        for (int i = 0; i < missileArraySize; i++)
        {
            
            missileArray[i] = Instantiate(missile,transform.position, transform.rotation) as GameObject;
            if(missileArray[i] == null)
                print("nope");
            //missileArray[i].transform.parent = this.transform;
            missileArray[i].SetActive(false);
        }

        //ready = true;
        return true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //roll the dice with double seeded numbers



	    /*if(Input.GetMouseButtonDown(0))
        {
            Instantiate(missile,transform.position, transform.rotation);
        }*/
	}

    public void Update()
    {
        if (ready)
        {
            int seed1 = Random.Range(1, 200);
            int seed2 = (int)(Time.time * seed1);
            Random.seed = seed2;
            int roll = Random.Range(1, 500);
            //print("roll = " + roll);
            if (roll == 4)
            {
                FireMissile();
                //Instantiate(missile,transform.position, transform.rotation);
            }
        
            if (roll == 15)
            {
                FireMissile();

                //Instantiate(missile,transform.position, transform.rotation);
            }
        
            if (roll == 32)
            {
                FireMissile();

                //Instantiate(missile,transform.position, transform.rotation);
            }
        
            if (roll == 44)
            {
                FireMissile();

                //Instantiate(missile,transform.position, transform.rotation);
            }

            gatherCount--;

            if (gatherCount < 0)
            {
                // gatherMissiles();
                gatherCount = 100;
            }
        }
    }

    public void FireMissile()
    {
       
        for (int i = 0; i < missileArraySize; i++)
        {
            homey = missile.GetComponent<Homing> ();
            if(missileArray[i].activeInHierarchy == false)
            {
                missileArray[i].transform.position = gameObject.transform.position;
                homey = missileArray[i].GetComponent<Homing> ();
                homey.lifetime = missileLifeTime;
                homey.missileVelocity = velocity;
                homey.turn = missileTurn;
                homey.startTime = Time.time;
                homey.hit = false;
                missileArray[i].SetActive(true);
                return;
            }
        }
    }

    /*public void gatherMissiles()
    {
        foreach (GameObject missile in missileArray)
        {
            deadHomey = missile.GetComponent<Homing> ();
            if(deadHomey.activeMissile == false)
            {
                deadHomey.activeMissile = true;

            }
        }
    }*/

    public int MissileCount()
    {
        int count = 0;
        for (int i = 0; i < missileArraySize; i++)
        {
            homey = missile.GetComponent<Homing> ();
            if(missileArray[i].activeInHierarchy == true)
            {
                count++;
            }
        }

        return count;
    }
}
