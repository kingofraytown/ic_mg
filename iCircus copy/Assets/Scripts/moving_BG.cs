using UnityEngine;
using System.Collections;

public class moving_BG : MonoBehaviour {

	// Use this for initialization

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
    public float speed;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(speed, 0f, 0f); 
	}

    public void resetToOrigin(float x)
    {
        Vector3 pos = transform.position;
        
        transform.position = new Vector3(pos.x - (x/20), pos.y, pos.z);
    }
}
