using UnityEngine;
using System.Collections;

public class Wind_Controller : MonoBehaviour {

    public GameObject target;
    public GameObject rightWind;
    public GameObject backWind;
    public GameObject frontWind;
    private ParticleSystem rightSystem;
    private ParticleSystem backSystem;
    private ParticleSystem frontSystem;
    private Quaternion lastRotation;
    public float speed;


    void OnEnable()
    {
        CameraController.cameraSpeedEvent += changeSpeed;
        playerController.playerStateEvent += changeState;
        // tiltListener.tiltEvent += MoveByZ;
        //PlayerStateController.onStateChange += onStateChange;
    }
    void OnDisable()
    {
        CameraController.cameraSpeedEvent -= changeSpeed;
        playerController.playerStateEvent -= changeState;
        // tiltListener.tiltEvent += MoveByZ;
        //PlayerStateController.onStateChange += onStateChange;
    }
	// Use this for initialization
	void Start () 
    {
        rightSystem = rightWind.GetComponent<ParticleSystem>();
        backSystem = backWind.GetComponent<ParticleSystem>();
        frontSystem = frontWind.GetComponent<ParticleSystem>();

        backWind.SetActive(false);
        frontWind.SetActive(false);
        lastRotation = transform.rotation;
	}
	
	// Update is called once per frame
    void Update()
    {
        /*print("cam r = " + transform.rotation.z + " | player r = " + playerObject.transform.rotation.z);
        if (this.transform.rotation.z > playerObject.transform.rotation.z)
        {
            this.transform.Rotate(new Vector3(0, 0, 1), -2, Space.World);
        }
    
        if (this.transform.rotation.z < playerObject.transform.rotation.z)
        {
            this.transform.Rotate(new Vector3(0, 0, 1), 2, Space.World);
        
        }*/
        
        /*if (transform.position.x >= 999)
        {
            Vector3 pos = transform.position;
            transform.position = new Vector3(0f, pos.y, pos.z);

        }*/
        transform.rotation = Quaternion.Lerp(lastRotation, target.transform.rotation, 15f);
    }

    public void changeSpeed(float sp)
    {
        rightSystem.startSpeed = rightSystem.startSpeed + (10 * sp * 2);
        rightSystem.emissionRate += 1;
    }

    public void changeState(playerController.zState zState)
    {
        switch (zState)
        {
            case playerController.zState.close:
                //print("state close");
                if(!frontWind.activeSelf){
                    backWind.SetActive(true);
                    rightWind.SetActive(false);
                    frontWind.SetActive(false);
                }
                break;
            case playerController.zState.far:
                //print("state far");
                if(!backWind.activeSelf){
                    backWind.SetActive(false);
                    rightWind.SetActive(false);
                    frontWind.SetActive(true);
                }
                break;
            case playerController.zState.center:
                //print("state center");
                if(!rightWind.activeSelf){
                    backWind.SetActive(false);
                    rightWind.SetActive(true);
                    frontWind.SetActive(false);
                }
                break;
                
        }
    }
}
