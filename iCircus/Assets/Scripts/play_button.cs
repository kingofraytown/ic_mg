using UnityEngine;
using System.Collections;

public class play_button : MonoBehaviour
{

    // Use this for initialization
    public Texture2D playButton;
    void Start()
    {
        GetComponent<GUITexture>().texture = playButton;
    }
	
    // Update is called once per frame
    void Update()
    {

        if (Input.touches.Length > 0  && (Input.GetTouch(0).phase == TouchPhase.Ended))
		{
            if(GetComponent<GUITexture>().HitTest(Input.GetTouch(0).position))
            {
                Application.LoadLevel("main game");
            }
            /*Touch t = Input.GetTouch(0);
			RaycastHit2D hitInfo = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), Vector2.zero);
            print("hello1");
            //print("hitInfos = " + hitInfos.Length);
            //foreach (RaycastHit2D hitInfo in hitInfos)
            //{

               //     print("raycast position = " + hitInfo.point);
               // print("ray to world point = " + Camera.main.scre)
               // print("position of touch = " + t.position);
              ///  print("touch in world = " + Camera.main.ScreenToWorldPoint(t.position));
                	if(hitInfo.collider.tag == "play_button")

                    {
                        Application.LoadLevel("main game");
                    }

            //}*/

		}
    }
}

