using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

    public GameObject HealthBar;
    public GameObject boostBar;
    public TextMesh ScoreText;
    public TextMesh TimeText;
    public TextMesh FrameRate;
    private float accum   = 0; // FPS accumulated over the interval
    private int   frames  = 0; // Frames drawn over the interval
    private float timeleft; // Left time for current interval
    public float updateInterval = 0.5f;

    protected Animator healthAnimator;
    void OnEnable()
    {
        Homing.missleEvent += missileHandler;

    }

    void OnDisable()
    {
        Homing.missleEvent -= missileHandler;
    }

	// Use this for initialization
	void Start () 
    {
        healthAnimator = HealthBar.GetComponent<Animator> ();
        healthAnimator.SetInteger("healthBarAmount", 4);
        timeleft = updateInterval; 
	}
	
	// Update is called once per frame
	void Update () {
        //check for health bar animator
        if (healthAnimator == null)
        {
            healthAnimator = HealthBar.GetComponent<Animator>();
        }

        timeleft -= Time.deltaTime;
        accum += Time.timeScale/Time.deltaTime;
        ++frames;
        
        // Interval ended - update GUI text and start new interval
        if( timeleft <= 0.0 )
        {
            // display two fractional digits (f2 format)
            float fps = accum/frames;
            string format = System.String.Format("{0:F2} FPS",fps);
            FrameRate.text = format;
            
            //if(fps < 30)
               // guiText.material.color = Color.yellow;
            //else 
                if(fps < 10)
                    //guiText.material.color = Color.red;
            //else
                //guiText.material.color = Color.green;
            //  DebugConsole.Log(format,level);
            timeleft = updateInterval;
            accum = 0.0F;
            frames = 0;
        }
    
	}

    public void missileHandler(Homing.missileState ms)
    {
        print("getting missile event");
        if (ms == Homing.missileState.hit)
        {
            //healthAnimator = HealthBar.GetComponent<Animator> ();
            print("healthbar = " + healthAnimator);
            int newHealth = healthAnimator.GetInteger("healthBarAmount");
            newHealth--;
            if(newHealth > 0)
            {
                healthAnimator.SetInteger("healthBarAmount", newHealth);
            }

            if(newHealth == 0)
            {
                DontDestroyOnLoad(gameObject);
                Application.LoadLevel("GameOver");
            }
        }
    }
}
