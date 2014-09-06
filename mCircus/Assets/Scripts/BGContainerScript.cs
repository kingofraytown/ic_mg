using UnityEngine;
using System.Collections;

public class BGContainerScript : MonoBehaviour {

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
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void resetToOrigin(float x)
    {
        Vector3 pos = transform.position;
        
        transform.position = new Vector3(pos.x - x, pos.y, pos.z);
    }
    //
}
