using UnityEngine;
using System.Collections;

public class TennisBallLauncher : MonoBehaviour {

	public GameObject tennisBallPrefab;
	public float minLaunchVector;
	public float maxLaunchVector;
	public TennisWizard wizard;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
		LaunchTennisBall();
		}
	}
	
	public void LaunchTennisBall()
	{
		Vector3 launchVector = new Vector3(0,Random.Range(minLaunchVector,maxLaunchVector),0);
		GameObject t = (GameObject)Instantiate(tennisBallPrefab,transform.position,Quaternion.identity);
		t.GetComponent<Rigidbody>().AddForce(launchVector);
		t.GetComponent<BallMovement>().wizard = wizard;
	}
}
