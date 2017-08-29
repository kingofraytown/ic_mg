using UnityEngine;
using System.Collections;

public class BoostDisplayController : MonoBehaviour {

    public GameObject BoostGauge;
    public GameObject CoolDownGauge;
    public GameObject FullGauge;
    public float boostAmount;
    public float coolAmount;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void UpdateBoost(float newAmount)
    {
        boostAmount = newAmount;
        BoostGauge.transform.localScale = new Vector3(boostAmount, 1f,1f);
    }

    public void UpdateCoolDown(float newAmount)
    {
        coolAmount = newAmount;
        CoolDownGauge.transform.localScale = new Vector3(coolAmount, 1f, 1f);
    }
}
