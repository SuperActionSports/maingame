using UnityEngine;
using System.Collections;

public class StickeggRules : MonoBehaviour {
	
	public BaseballLauncher pitcher ;
	public AudioClip charge ;
	public Camera camera ;
	public int p1Score = 0;
	public int p2Score = 0;
	public int p3Score = 0;
	public int p4Score = 0;
	public int p1Roster = 40;
	public int p2Roster = 40;
	public int p3Roster = 40;
	public int p4Roster = 40;
	private float gameTime = 0.0f ;
	private float gameLength = 15.0f ;
	public float pitchGap = 1.5f ;
	private float lastPitch = 1.8f ;
	private float finalInning;
	private bool bottomNinth ;
	private GameObject[] scoreBalls ;
	private float madnessGap;

	void Start () {
		pitcher.Invoke ("Pitch", 5.4f) ;
		finalInning = 10;
		madnessGap = 5;
	}

	void Update () {
		if (Time.time - lastPitch >= pitchGap) {
			lastPitch += pitchGap;
			pitcher.Pitch ();
		}
	}
	
//	void pointAtBall () {
//		Vector3 p1 = camera.ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0)) ;
//		Vector3 p2 = Camera.main.ScreenToWorldPoint(GameObject.FindWithTag("ball").transform.position) ;
//		Debug.Log (p1);
//	}
}