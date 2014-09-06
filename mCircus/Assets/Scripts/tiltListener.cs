using UnityEngine;
using System.Collections;

public class tiltListener : MonoBehaviour {

    float initZ;
    float startTime;
    bool tiltCapReached = false;
    bool EventStarted = false;
    bool EventSent = false;
    direction currentDir; 
    int count = 0;
    public delegate void tiltHandler(tiltListener.direction tiltDirection);
    public static event tiltHandler tiltEvent;

    public enum direction
    {
        up,
        down,
        center
    }

    // Use this for initialization
	void Start () {
	    //get initial z axis
        initZ = Input.acceleration.z;
        print("init z = " + initZ);
	}
	
	// Update is called once per frame
	void Update () {
        float currentZaxis = Input.acceleration.z - initZ;

	    if((currentZaxis > 0.2) && (EventStarted == false))
        {
            EventStarted = true;
            startTime = Time.time;
            tiltCapReached = true;
            currentDir = direction.up;
            EventSent = false;

        }

        else if((currentZaxis < -0.3) && (EventStarted == false))
        {
            EventStarted = true;
            startTime = Time.time;
            tiltCapReached = true;
            currentDir = direction.down;
            EventSent=false;
        }

        if (((Time.time - startTime) < 1) && tiltCapReached)
        {
            EventStarted = false;
            startTime = -1.0f;
            tiltCapReached = false;

            if (currentDir == direction.down)
            {
                print("tilt DOWN");
                if(!EventSent)
                {
                    print("currentZ = " + currentZaxis);
                    SendEvent(direction.down);
                }
            } else if (currentDir == direction.up)
            {
                print("tilt UP");
                if(!EventSent)
                {
                    SendEvent(direction.up);
                }
            }
        } 
        else
        {
            //print("NO");
            EventSent = false;
            count = 0;
        }

	}

    public void SendEvent(tiltListener.direction newDirection)
    {
        if (!EventSent && count == 0)
        {
            count++;
            tiltEvent(newDirection);

            if (newDirection == direction.up)
                print("UP event " + count);

            if (newDirection == direction.down)
                print("DOWN event " + count);

            EventSent = true;
           // count++;
        }
    }

}
