using UnityEngine;
using System.Collections;

public class TennisBallLauncherSway : MonoBehaviour {

	public float minAngle;
	public float maxAngle;
	private bool risingAngle = false;
	public float swaySpeed = 1;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,swaySpeed,0);
		if ((transform.eulerAngles.y > maxAngle && swaySpeed > 0) ||
			transform.eulerAngles.y < minAngle +5 && swaySpeed < 0)
		{
			swaySpeed *= -1;
		}
		
	}
}
