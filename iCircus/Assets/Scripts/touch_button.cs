using UnityEngine;
using System.Collections;

public class touch_button : MonoBehaviour {
	
	public static int currTouch = 0;
	
	public virtual void Update () 
	{
		if (Input.touches.Length <= 0) {
			
			//do whatever we want when there are no touches on screen
		} else {
			
			//loop through the touches
			for(int i = 0; i < Input.touchCount; i++) {
				currTouch = i;
				if(this.GetComponent<GUITexture>() != null && (this.GetComponent<GUITexture>().HitTest(Input.GetTouch(i).position)))
				{
					//if currTouch hits the guiTexture
					if(Input.GetTouch(i).phase == TouchPhase.Began)
					{
						//when the button first starts to be hit, change it's appearance?
						OnTouchBegan();
						print("Are we here?");
						
					}
					if(Input.GetTouch(i).phase == TouchPhase.Ended) 
					{
						//when the button is let up, change it back to original?
						OnTouchEnded();
						print("touchphase ended");
					}
				}
				
			}
		}
	}
	
	public virtual void OnTouchBegan() { print ("we are in onTouchBegan"); } 
	public virtual void OnTouchEnded() {
		print ("we are in ontouchended");
	}
}
