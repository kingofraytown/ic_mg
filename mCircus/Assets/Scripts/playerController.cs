using UnityEngine;
using System.Collections;

public class playerController :  MonoBehaviour
{

    //int zpos = 0;
    float AccelX = 0f;
    float PlayerAngle = 0;
    public zState currentZState = zState.center;
    public float playerSpeed;
    public float boostSpeed = 4;
    public bool boostActive = false;
    private float originalSpeed;
    public float boostTime = 1.0f;
    public float boostCount = 0.0f;
    public GameObject mCamera;

    public float coolDownTime;
    public float coolDownCount;
    public bool coolDown;

    public float speedUpTime;
    private float speedUp = 0;

    protected Animator animator;
    int stillCount;

    public enum zState
    {
        far,
        center,
        close
    }
    public delegate void playerSpeedDelegate(float speed);
    public static event playerSpeedDelegate playerSpeedEvent;

    public delegate void playerStateDelegate(zState playerState);
    public static event playerStateDelegate playerStateEvent;


    public delegate void resetDelegate(float x);
    public static event resetDelegate resetEvent;

    // Use this for initialization
    void OnEnable()
    {
        Homing.missleEvent += missileHandler;
       // tiltListener.tiltEvent += MoveByZ;
        //PlayerStateController.onStateChange += onStateChange;
    }
    
    void OnDisable()
    {
        Homing.missleEvent -= missileHandler;
        //PlayerStateController.onStateChange -= onStateChange;
    }
    void Start()
    {
        animator = GetComponent<Animator> ();
        animator.SetBool("tiltOut",false);
        animator.SetBool("tiltIn", false);
        speedUpTime = Time.time;

        currentZState = zState.center;
    }

    void FixedUpdate()
    {

        //this.rigidbody.AddForce( new Vector3(2.0f, 0f,0f));
        if (boostActive == true && (boostCount- Time.time <= 3))
        {
            playerSpeed -= 0.2f;
            if(playerSpeed <= originalSpeed)
            {
                boostActive = false;
                playerSpeed = originalSpeed;
                boostCount = 0.0f;
            }
        }
        if (boostActive)
        {
            boostCount -= 0.1f;
        }

    }
    // Update is called once per frame
    void Update()
    {

        /*if((speedUpTime + 5) < Time.time)
        {
            playerSpeed += 0.1f;
            speedUp += 0.1f;
            speedUpTime = Time.time;
            playerSpeedEvent(playerSpeed);
        }*/

        if (transform.position.x >= 7000)
        {
            resetEvent(transform.position.x);
            Vector3 pos = transform.position;

            transform.position = new Vector3(0f, pos.y, pos.z);
            
        }
        this.transform.Translate(playerSpeed, 0f, 0f);
        //print("local " + transform.localPosition);
        //print("global " + transform.position);
        // float dpos = 0.0f;
        if (Input.touchCount > 0)
        {
            Vector2 rightTouch = new Vector2();
            bool rightBool = false;
            Vector2 leftTouch = new Vector2();
            bool leftBool = false;

            foreach (Touch tch in Input.touches)
            {
                //if the touch is on the right side of the screen
                if (tch.position.x > (Screen.width / 2))
                {
                    rightTouch.x = tch.deltaPosition.x;
                    rightTouch.y = tch.deltaPosition.y;
                    rightBool = true;
                    //print("Right touch = " + tch);
                    // print("Right local touch = " + tch);

                }

                if (tch.position.x < (Screen.width / 2))
                {

                    if(tch.deltaPosition.y == 0)
                    {
                        stillCount++;
                    }

                    //print(tch.deltaTime);
                    if (tch.deltaPosition.y > 0.1f)
                    {
                        animator.SetBool("tiltOut", true);
                        animator.SetBool("tiltIn", false);
                        stillCount = 0;
                    } else if (tch.deltaPosition.y < -0.1f)
                    {
                        animator.SetBool("tiltIn", true);
                        animator.SetBool("tiltOut", false);
                        stillCount =0;
                    } 
                    else if(stillCount > 10)
                    {
                        print("sationary touch");
                        animator.SetBool("tiltOut", false);
                        animator.SetBool("tiltIn", false);
                        currentZState = zState.center;


                    }

                    leftTouch.x = tch.deltaPosition.x;
                    leftTouch.y = tch.deltaPosition.y;
                    leftBool = true;
                }



            }

            if (rightBool)
            {


                float boostConstant = 1f;
                if(boostActive)
                {
                    boostConstant = boostSpeed/2;
                }


                float newX = rightTouch.x * 0.8f * boostConstant + speedUp;

                if(rightTouch.x < 0)
                    newX = 2 * newX;

                if ((transform.position.x + newX) >= (30 + mCamera.transform.position.x))
                {
                    if(!boostActive)
                    {
                        //ActivateBoost();
                    }
                    else
                    {
                        newX = 0.0f;
                    }
                } 
                else if ((transform.position.x + newX) <= -30 + mCamera.transform.position.x)
                {
                    if(!boostActive)
                    {
                        //ActivateBoost();
                    }
                    else
                    {
                        newX = 0.0f;
                    }
                }


                float newY = rightTouch.y * 0.8f * boostConstant + speedUp;

                if ((transform.position.y + newY) >= 20 + mCamera.transform.position.y)
                {
                    if(!boostActive)
                    {
                        //ActivateBoost();
                    }
                    else
                    {
                        newY = 0.0f;
                    }
                } else if ((transform.position.y + newY) <= -20 + mCamera.transform.position.y)
                {
                    if(!boostActive)
                    {
                        //ActivateBoost();
                    }
                    else
                    {
                        newY = 0.0f;
                    }
                }

                this.transform.Translate(newX, newY, 0f);
            }

            if (leftBool)
            {
                float newZscale = leftTouch.y * 0.5f;
                float newZPos = leftTouch.y;

                //print("NewZ delta = " + newZ);

                ////translate the player position.z by the oppisite of newZ

                if (transform.localScale.x + newZscale >= 2.5f)
                {
                    newZscale = 0f;
                    newZPos = 0f;
                } 
                else if (transform.localScale.x + newZscale <= 0.5f)
                {
                    newZscale = 0f;
                    newZPos = 0f;
                }
                transform.Translate(0f, 0f, -newZPos);

                transform.localScale += new Vector3(newZscale, newZscale, newZPos);
                //print("localscale after = "+ transform.localScale);

                if(leftTouch.y > 0)
                {
                    currentZState  = zState.far;
                    playerStateEvent(currentZState);
                }
                if(leftTouch.y < 0)
                {
                    currentZState = zState.close;
                    playerStateEvent(currentZState);
                }

            }
            
        } else
        {
            animator.SetBool("tiltOut", false);
            animator.SetBool("tiltIn", false);
            stillCount = 0;
            currentZState = zState.center;
            //playerStateEvent(currentZState);
        }
        //get the acceleration difference on z axis
        AccelX = Input.acceleration.x;

        //print("x accel = " + Input.acceleration.x);
        //print("player angle = " + PlayerAngle

        PlayerAngle = 2.0f;
        if (AccelX > 0.3)
        {
            //PlayerAngle += 0.05f;
            this.transform.Rotate(new Vector3(0,0,1) ,PlayerAngle,Space.World);
            //boostActive = true;

        } 
        else if (AccelX < -0.3)
        {
            //PlayerAngle -= 0.05f;
            this.transform.Rotate(new Vector3(0,0,1), -1 * PlayerAngle, Space.World);
            //boostActive = true;
        }
        /*if (Input.acceleration.z > 0.6)
        {
            if (zpos != 1)
            {
                transform.Translate(0f, 0f, 5f);
                zpos++;
            }
        } else if (Input.acceleration.z < -0.6)
        {
            if(zpos != -1)
            {
                transform.Translate(0f, 0f, -5f);
                zpos--;
            }
        }*/

       // print("accel z = " + Input.acceleration.z);

    }

    public void ActivateBoost()
    {
        originalSpeed = playerSpeed;
        playerSpeed = boostSpeed;
        boostCount = Time.time + boostTime;
        boostActive = true;
        print("Boost!!!");

    }



    /*public void MoveByZ(tiltListener.direction newDirection)
    {

        if (newDirection == tiltListener.direction.up)
        {

                if (changeZState(1))
                {
                    transform.Translate(0f, 5f, -5f);
                    transform.localScale.Scale(new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + 1f));
                    
                    zpos++;
                }
               
           
        } else if (newDirection == tiltListener.direction.down)
        {
                if (changeZState(-1))
                {
                    transform.Translate(0f, -5f, 5f);
                transform.localScale.Scale(new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z - 1f));
                    zpos--;
                }
        }
    }*/

    /*public bool changeZState(int i)
    {
        bool success = false;
        if (i == 1)
        {
            if(currentZState == zState.far)
            {
                currentZState = zState.center;
                success = true;
            }
            else if(currentZState == zState.center)
            {
                currentZState = zState.close;
                success = true;
            }
            else if(currentZState == zState.close)
            {
                success = false;
            }
        } 

        else if (i == -1)
        {
            if(currentZState == zState.close)
            {
                currentZState = zState.center;
                success = true;
            }
            else if(currentZState == zState.center)
            {
                currentZState = zState.far;
                success = true;
            }
            else if(currentZState == zState.far)
            {
                success = false;
            }
        }

        return success;

    }*/

    public void missileHandler(Homing.missileState ms)
    {
        if (ms == Homing.missileState.hit)
        {

        }
    }
}
