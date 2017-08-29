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
    private Vector2 FirstTouchCoord;
    private Vector2 SecondTouchCrood;
    private float tapStartTime = 0.0f;
    public float tapDeltaTime = 0.5f;
    public float tapDistanceLimit = 3f; 
    public int tapCount = 0;
    public GameObject mCamera;
    private TouchPhase newTouchPhase;
    private Touch currentRightTouch = new Touch();

    public float coolDownTime;
    public float coolDownCount;
    public bool coolDownActive;
    public GameObject boostBar;
    private BoostDisplayController boostCon;


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
        boostCon = boostBar.GetComponent<BoostDisplayController> ();

    }
    /*void LateUpdate()
    {
        if(boostActive){
            float boostPercent = Time.time / boostCount;

            if (boostPercent > 1)
            {
                boostPercent = 1;
                boostCon.UpdateBoost(1 - boostPercent);
                boostActive = false;
                boostPercent = 0;
                boostCon.UpdateBoost(1);
                coolDownActive = true;
                playerSpeed = originalSpeed;
            }

            boostCon.UpdateBoost(1 - boostPercent);
        }

    }*/

    void FixedUpdate()
    {

        //this.rigidbody.AddForce( new Vector3(2.0f, 0f,0f));
        /*if (boostActive == true && (boostCount <= 0))
        {
            playerSpeed -= 0.2f;
            if (playerSpeed <= originalSpeed)
            {
                //boostActive = false;
                //boostCon.UpdateBoost(1);
                //coolDownActive = true;
                playerSpeed = originalSpeed;

            }


        }*/
        if (boostActive)
        {
            float boostPercent = boostCount / boostTime;
            if (boostCount < 0)
            {
                boostPercent = 0;
                boostCon.UpdateBoost(boostPercent);
                boostActive = false;
                boostPercent = 0;
                //boostCon.UpdateBoost(1);
                coolDownActive = true;
                coolDownCount = coolDownTime;
                playerSpeed = originalSpeed;
            }
            if(!coolDownActive)
            {
                boostCon.UpdateBoost(boostPercent);
            }

            boostCount -= 0.1f;

        }

        if (coolDownActive)
        {
            float coolDownPercent = coolDownCount/ coolDownTime;
            if(coolDownCount < 0)
            {
                coolDownPercent = 1;
                boostCon.UpdateCoolDown(0);
                boostCon.FullGauge.SetActive(true);
                coolDownActive = false;
            }
            if(!boostCon.FullGauge.activeSelf)
            {
                boostCon.UpdateCoolDown(coolDownPercent);
            }

            coolDownCount -= 0.1f;
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
            //Touch currentRightTouch = new Touch();
            foreach (Touch tch in Input.touches)
            {

                //if the touch is on the right side of the screen
                if (tch.position.x > (Screen.width / 2))
                {
                    rightTouch.x = tch.deltaPosition.x;
                    rightTouch.y = tch.deltaPosition.y;


                    if(tch.phase == TouchPhase.Ended)
                    {
                        currentRightTouch = tch;
                        print("currentRightTouch has been updated");
                    }
                    newTouchPhase = tch.phase;
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
                        currentZState  = zState.far;
                        //playerStateEvent(currentZState);
                        stillCount = 0;
                    } else if (tch.deltaPosition.y < -0.1f)
                    {
                        animator.SetBool("tiltIn", true);
                        animator.SetBool("tiltOut", false);
                        currentZState = zState.close;
                        //playerStateEvent(currentZState);
                        stillCount =0;
                    } 
                    else if(stillCount > 10)
                    {
                        print("sationary touch");
                        animator.SetBool("tiltOut", false);
                        animator.SetBool("tiltIn", false);
                        currentZState = zState.center;
                        //playerStateEvent(currentZState);


                    }

                    leftTouch.x = tch.deltaPosition.x;
                    leftTouch.y = tch.deltaPosition.y;
                    leftBool = true;
                }



            }

            if (rightBool)
            {


                if(newTouchPhase == TouchPhase.Ended)
                {
                    tapCount++;
                    //tapStartTime = Time.time; 
                    //FirstTouchCoord = rightTouch;
                }



                if(tapCount > 1 && (!boostActive && !coolDownActive))
                {
                    SecondTouchCrood = currentRightTouch.position;;
                    float secondTapTime = Time.time;
                    print("tap delta" + (secondTapTime - tapStartTime));
                    if(((secondTapTime - tapStartTime) <= tapDeltaTime ) && (!boostActive))
                    {
                        print("passed time test");
                        print("tap distance " + Vector2.Distance(FirstTouchCoord, SecondTouchCrood));
                        if(Vector2.Distance(FirstTouchCoord, SecondTouchCrood) <= tapDistanceLimit)
                        {
                            print("passed distance test");
                            ActivateBoost();
                            tapCount = 0;
                        }
                    }
                    else
                    {
                        tapCount = 0;
                    }

                }

                if(tapCount == 1)
                {
                    tapStartTime = Time.time;
                    FirstTouchCoord = currentRightTouch.position;
                }

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
                float newZscale = leftTouch.y; //* //0.5f;
                float newZPos = leftTouch.y; //* 1.5f;

             
                //print("NewZ delta = " + newZ);

                ////translate the player position.z by the oppisite of newZ

                if (transform.position.z - newZPos <= -15f)
                {
                    //newZscale = 0f;
                    newZPos = 0f;
                } 
                else if (transform.position.z - newZPos >= 60f)
                {
                    //newZscale = 0f;
                    newZPos = 0f;
                }
                transform.Translate(0f, 0f, -newZPos );

              

            }
            
        } else
        {
            animator.SetBool("tiltOut", false);
            animator.SetBool("tiltIn", false);
            stillCount = 0;
            //currentZState = zState.center;
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
      
    }

    public void ActivateBoost()
    {
        originalSpeed = playerSpeed;
        playerSpeed = boostSpeed;
        boostCount = boostTime;
        boostCon.FullGauge.SetActive(false);
        boostActive = true;
        print("Boost!!!");

    }



   
    public void missileHandler(Homing.missileState ms)
    {
        if (ms == Homing.missileState.hit)
        {

        }
    }
}
