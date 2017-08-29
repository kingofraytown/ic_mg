using UnityEngine;
using System.Collections;

public class windScript : MonoBehaviour {


	public float cloudSpeed;
	public GameObject bg1;
	public GameObject bg2;
	public GameObject bg3;
	public GameObject[] bgArray;
    private float spriteSize;
    private float firstX;
    private float lastX;
    private BoxCollider2D bc;
    private float initY;

	// Use this for initialization
	void Start () {
		bgArray[0] = bg1;
		bgArray[1] = bg2;
		bgArray[2] = bg3;
        bc = bg1.GetComponent<BoxCollider2D>();
        spriteSize = bc.size.x;
        firstX = bg1.transform.localPosition.x;
        lastX = bg3.transform.localPosition.x;
        initY = bg3.transform.localPosition.y;
        print("init last x = " + bg3.transform.localPosition.x); 
	}
	
	// Update is called once per frame
	void Update () {
		foreach (GameObject bg in bgArray) 
		{
            bg.transform.Translate( new Vector3(cloudSpeed,0f,0f));

            if(bg.transform.localPosition.x > firstX + (spriteSize *3))
            {
                if(bgArray[0] == bg)
                {
                    print("position before reset x = " + bg.transform.localPosition.x);
                    
                }
                if(bgArray[2] == bg)
                {
                    print("position x 2 before reset = " + bg.transform.localPosition.x);
                    
                }
                //bg.transform.localPosition = new Vector3( ,initY ,0f);
            }


		}
	
	}
}
