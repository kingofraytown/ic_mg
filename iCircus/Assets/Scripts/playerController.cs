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


    protected Animator animator;
    int stillCount;

    public enum zState
    {
        far,
        center,
        close
    }
    // Use this for initialization
    void OnEnable()
    {
        Homing.missleEvent += missileHandler;
       // tiltListener.tiltEvent += MoveByZ;
        //PlayerStateController.onStateChange += onStateChange;
    }
    
    void OnDisable()
    {
        //PlayerStateController.onStateChange -= onStateChange;
    }
    void Start()
    {
        animator = GetComponent<Animator> ();
        animator.SetBool("tiltOut",false);
        animator.SetBool("tiltIn", false);
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


                float newX = rightTouch.x * 0.8f * boostConstant;

                if(rightTouch.x < 0)
                    newX = 2 * newX;

                if ((transform.position.x + newX) >= (30 + mCamera.transform.position.x))
                {
                    if(!boostActive)
                    {
                        ActivateBoost();
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
                        ActivateBoost();
                    }
                    else
                    {
                        newX = 0.0f;
                    }
                }


                float newY = rightTouch.y * 0.8f * boostConstant;

                if ((transform.position.y + newY) >= 20 + mCamera.transform.position.y)
                {
                    if(!boostActive)
                    {
                        ActivateBoost();
                    }
                    else
                    {
                        newY = 0.0f;
                    }
                } else if ((transform.position.y + newY) <= -20 + mCamera.transform.position.y)
                {
                    if(!boostActive)
                    {
                        ActivateBoost();
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
                float newZ = leftTouch.y * 0.5f;
                //print("NewZ delta = " + newZ);

               

                if (transform.localScale.x + newZ >= 2.5f)
                {
                    newZ = 0f;
                } else if (transform.localScale.x + newZ <= 0.5f)
                {
                    newZ = 0f;
                }
                transform.Translate(0f, 0f, newZ);

                transform.localScale += new Vector3(newZ, newZ, newZ);
                //print("localscale after = "+ transform.localScale);



            }
            
        } else
        {
            animator.SetBool("tiltOut", false);
            animator.SetBool("tiltIn", false);
            stillCount = 0;
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
