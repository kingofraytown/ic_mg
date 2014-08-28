using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour {

    public GameObject HealthBar;
    public GameObject boostBar;
    public TextMesh ScoreText;

    protected Animator healthAnimator;
    void OnEnable()
    {
        Homing.missleEvent += missileHandler;
    }

	// Use this for initialization
	void Start () {
        healthAnimator = HealthBar.GetComponent<Animator> ();
        healthAnimator.SetInteger("healthBarAmount", 4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void missileHandler(Homing.missileState ms)
    {
        print("getting missile event");
        if (ms == Homing.missileState.hit)
        {


            int newHealth = healthAnimator.GetInteger("healthBarAmount");
            newHealth--;
            if(newHealth >= 0)
            {
                healthAnimator.SetInteger("healthBarAmount", newHealth);
            }
        }
    }
}
