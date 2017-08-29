 using UnityEngine;
using System.Collections;
public class ParallaxController : MonoBehaviour
{
	public GameObject[] clouds;
	public GameObject[] nearHills;
	public GameObject[] farHills;

	public float cloudLayerSpeedModifier;
	public float nearHillLayerSpeedModifier;
	public float farHillLayerSpeedModifier;
	public Camera myCamera;

	private Vector3 lastCamPos;
	void Start()
	{
		lastCamPos = myCamera.transform.position;
	}
	void Update()
	{
		Vector3 currCamPos = myCamera.transform.position;
		float xPosDiff = lastCamPos.x - currCamPos.x;
        float yPosDiff = lastCamPos.y - currCamPos.y;
		adjustParallaxPositionsForArray(clouds,
		                                cloudLayerSpeedModifier, xPosDiff, yPosDiff);
		adjustParallaxPositionsForArray(nearHills,
		                                nearHillLayerSpeedModifier, xPosDiff, yPosDiff);
		adjustParallaxPositionsForArray(farHills,
		                                farHillLayerSpeedModifier, xPosDiff, yPosDiff);
		lastCamPos = myCamera.transform.position;
	}
	void adjustParallaxPositionsForArray(GameObject[]
	                                     layerArray, float layerSpeedModifier, float xPosDiff, float yPosDiff)
	{
		for(int i = 0; i < layerArray.Length; i++)
		{
			Vector3 objPos =
				layerArray[i].transform.position;
			objPos.x += xPosDiff * layerSpeedModifier;
            objPos.y += yPosDiff * layerSpeedModifier;
			layerArray[i].transform.position = objPos;
		}
	} }
