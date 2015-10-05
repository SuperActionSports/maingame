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
	private float gameTime = 0.0f ;
	private float gameLength = 60.0f ;
	private float pitchGap = 3.96f ;
	private float lastPitch = 1.8f ;
	private bool bottomNinth ;

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
		else {
			Tally () ;
		}
	}

	private void Tally ()
	{
		// tally scores and show who is winrar
	}

//	void pointAtBall () {
//		Vector3 p1 = camera.ViewportToWorldPoint(new Vector3(0.5F, 0.5F, 0)) ;
//		Vector3 p2 = Camera.main.ScreenToWorldPoint(GameObject.FindWithTag("ball").transform.position) ;
//		Debug.Log (p1);
//	}
}