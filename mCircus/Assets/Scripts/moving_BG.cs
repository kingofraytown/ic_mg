using UnityEngine;
using System.Collections;

public class moving_BG : MonoBehaviour {

	// Use this for initialization
    public float speed;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(speed, 0f, 0f); 
	}
}
