using UnityEngine;
using System.Collections;

public class Score_text : MonoBehaviour {
    public TextMesh scoreMesh;
	// Use this for initialization
	void Start () {
        scoreMesh.text = Toolbox.Instance.myGlobalVar.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
