﻿using UnityEngine;
using System.Collections;

public class ScoreKeeping : MonoBehaviour {
    float currentTime;
    public int score;
    void OnEnable()
    {
        Homing.missleEvent += missileHandler;
    }
	// Use this for initialization
	void Start () {
        currentTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        TextMesh tm = (TextMesh)this.transform.GetComponent(typeof(TextMesh)); 
        tm.text = score.ToString();
        //GetComponent(TextMesh).guiText = score.ToString();

	}

    public void AddToScore(int points)
    {
        score += points;

        Toolbox.Instance.myGlobalVar = score;
    }


    void OnGUI()
    {
       
    }
    public void missileHandler(Homing.missileState ms)
    {

        if (ms == Homing.missileState.miss)
        { 
            AddToScore(100);
        }
    }
}
