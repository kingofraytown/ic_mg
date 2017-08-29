using UnityEngine;
using System.Collections;

public class button : touch_button {
	
	public bool disableTouches;
	//public Texture2D button_down;
	//public Texture2D button_up;
	
	public override void Update()//(optional) only use Update if you need to
	{
		//you can do some logic before you check for the touches on screen
		
		
		if(!disableTouches)//(optional) dynamically change whether or not to check for touches
			base.Update();//must have this somewhere or TouchLogicV2's Update will be totally overwritten by this class's Update
	}
	
	public override void OnTouchBegan()
	{
		print ("HELLO FROM TEST BUTTON!");
	//	guiTexture.texture = button_down;
	}
	
	public override void OnTouchEnded() {
		print ("hello");
	//	guiTexture.texture = button_up;
		Application.LoadLevel("main game");
	}
}

