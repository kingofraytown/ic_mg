using UnityEngine;
using System.Collections;

public class play_button : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {

        if (Input.touches.Length > 0)
		{

			RaycastHit2D[] hitInfos = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
            print("hello1");
            print("hitInfos = " + hitInfos.Length);
            foreach (RaycastHit2D hitInfo in hitInfos)
            {

                    print("hello2");
                	if(hitInfo.collider.gameObject.tag == "play_button")
                    {
                        Application.LoadLevel("main game");
                    }

            }

		}
    }
}

