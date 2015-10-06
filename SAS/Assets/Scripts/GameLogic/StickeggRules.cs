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
	private float gameLength = 60.0f ;
	private float pitchGap = 3.96f ;
	private float lastPitch = 1.8f ;
	private bool bottomNinth ;
	private GameObject[] scoreBalls ;

	void Start () {
		pitcher.Invoke ("Pitch", 5.4f) ;
		// do the thing to get the # of players from player info
	}

	void Update () {
//		pointAtBall () ;
		gameTime += Time.deltaTime ;
		if (gameTime <= gameLength) {
			if (gameLength - gameTime <= 15.0f && !bottomNinth) {
				bottomNinth = true;
				pitchGap = 2.0f;
				AudioSource.PlayClipAtPoint (charge, new Vector3 (0.0f, 5.0f, 10.0f));
			}
			if (gameTime - lastPitch >= pitchGap) {
				lastPitch += pitchGap;
				pitcher.Pitch ();
			}
		} 
		else if (bottomNinth){
			bottomNinth = false;
			Tally () ;
		}
	}

	private void Tally ()
	{
		scoreBalls = GameObject.FindGameObjectsWithTag ("deadball");

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