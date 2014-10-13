using UnityEngine;
using System.Collections;

public class LaunchManager : MonoBehaviour {

    public GameObject[] launchers = new GameObject[5];
    public float progressionSpeed = 1f;
    private float startTime;
    private int stage = 0;
    private MissileLaucher lScript;
    public GameObject UIobject;
    private UIController uic;
    private int activeMissiles = 0;
    private float updateTime;
    private int updateInterval = 1;



	// Use this for initialization
	void Start () {
        updateTime = Time.time;
        uic = UIobject.GetComponent<UIController>();
        for (int i = 0; i < 5; i++)
        {
            //if(launchers[i].name != "launcher1")
            //launchers[i].SetActive(true);
            lScript = launchers[i].GetComponent<MissileLaucher> ();
            lScript.GetReady();

            //

        }

        //remove loading slide

        for (int i = 0; i < 5; i++)
        {
            if(launchers[i].name == "launcher1")
            {
            //launchers[i].SetActive(true);
            lScript = launchers[i].GetComponent<MissileLaucher> ();
            lScript.ready = true;
            }
            
        }

        startTime = Time.time;
	}
	
    void LateUpdate()
    {
        if (Time.time > updateTime + updateInterval)
        {
            getActiveMissileCount();
            updateTime = Time.time;
        }
    }
	// Update is called once per frame
	void Update () {

        //at 30 seconds after add another launcher
        if (Time.time >= (startTime + 20) && stage < 1)
        {
            //uic.TimeText.text ="Stage 1 ";
            MissileLaucher l = GetLauncherScriptWithName("launcher2");
            l.ready = true;
            stage = 1;
        }

        //at 60 sec add another launcher
        if (Time.time >= (startTime + 40) && stage < 2)
        {
            //uic.TimeText.text ="Stage 2 ";
            MissileLaucher l = GetLauncherScriptWithName("launcher3");
            l.ready = true;
            stage = 2;
        }
        //at 90 increase missile velocity
        if (Time.time >= (startTime + 60) && stage < 3)
        {
            //uic.TimeText.text ="Stage 3 ";
            print("Stage 3 ");

            IncreaseSpeedForAll(0.5f);
            stage = 3;
        }  

        if (Time.time >= (startTime + 90) && stage < 4)
        {
            print("Stage 4 ");
            MissileLaucher l = GetLauncherScriptWithName("launcher4");
            l.ready = true;
            //IncreaseSpeedForAll(2);
            stage = 4;
        }  

        if (Time.time >= (startTime + 120) && stage < 5)
        {
            print("Stage 5 ");
            MissileLaucher l = GetLauncherScriptWithName("launcher5");
            l.ready = true;
            IncreaseSpeedForAll(0.7f);
            stage = 5;
        }


	
	}

    MissileLaucher GetLauncherScriptWithName(string strName)
    {
        GameObject launcher = new GameObject();

        //GameObject[] glist = GameObject.FindGameObjectsWithTag("Launcher");
        //print("glist count " + glist.Length);
        for(int j = 0;j <launchers.Length; j++)
        {

            //print("GO name = " + go.name);
            if(launchers[j].name == strName)
            {
                print("launcher found");
                launcher = launchers[j];
                //break;
            }
        }

        MissileLaucher ml = launcher.GetComponent<MissileLaucher>();
        return ml;

    }

    void IncreaseSpeedForAll(float speedup)
    {

        for (int j = 0; j <launchers.Length; j++)
        {
            lScript = launchers [j].GetComponent<MissileLaucher>();
            lScript.velocity += speedup;
        }
    }

    void getActiveMissileCount()
    {
        int count = 0;
        for(int j = 0;j <launchers.Length; j++)
        {
            lScript = launchers[j].GetComponent<MissileLaucher> ();
            count += lScript.MissileCount();
        }

        uic.TimeText.text = "missiles: " + count.ToString();
    }

}
