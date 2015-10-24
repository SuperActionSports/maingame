﻿using UnityEngine;
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
		// do the thing to get the # of players from player info
	}

	void Update () {
//		pointAtBall () ;
		/*if (Time.time <= gameLength) {
			if (gameLength - Time.time <= finalInning && !bottomNinth) {
				bottomNinth = true;
				pitchGap /= 10;
				AudioSource.PlayClipAtPoint (charge, new Vector3 (0.0f, 5.0f, 10.0f));
			}
			else if (Time.time - lastPitch >= pitchGap) {
				lastPitch += pitchGap;
				pitcher.Pitch ();
			}
		} 
		else if (bottomNinth && Time.time > gameLength + madnessGap){
			bottomNinth = false;
			Tally () ;
		}*/
		if (Time.time - lastPitch >= pitchGap) {
			lastPitch += pitchGap;
			pitcher.Pitch ();
		}
	}

	private void Tally ()
	{
		scoreBalls = GameObject.FindGameObjectsWithTag ("Deadball");

		foreach (GameObject ball in scoreBalls) {
			BaseballController bc = ball.GetComponent<BaseballController>() ;
			int scorer = bc.ownNumber ;
			switch (scorer){
			case 1:
				p1Score += 4;
				break;
			case 2:
				p2Score += 4;
				break;
			case 3:
				p3Score += 4;
				break;
			case 4:
				p4Score += 4;
				break;
			default:
				break;
			}
		}
		p1Score += p1Roster;
		p2Score += p2Roster;
		p3Score += p3Roster;
		p4Score += p4Roster;
	}

//	void pointAtBall () {
//		Vector3 p1 = camera.ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0)) ;
//		Vector3 p2 = Camera.main.ScreenToWorldPoint(GameObject.FindWithTag("ball").transform.position) ;
//		Debug.Log (p1);
//	}
}