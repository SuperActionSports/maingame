using UnityEngine;
using System.Collections;

public class StickeggRules : MonoBehaviour {
	
	public BaseballLauncher pitcher ;
	public AudioClip charge ;
	private float gameTime = 0.0f ;
	private float gameLength = 60.0f ;
	private float pitchGap = 3.96f ;
	private float lastPitch = 1.8f ;
	private bool bottomNinth ;

	void Start () {
		pitcher.Invoke ("Pitch", 5.4f) ;
	}

	void Update () {
		gameTime += Time.deltaTime ;
		if (gameTime <= gameLength) {
			if (gameLength - gameTime <= 15.0f && !bottomNinth) {
				bottomNinth = true ;
				pitchGap = 2.0f ;
				AudioSource.PlayClipAtPoint (charge, transform.position);
			}
			if (gameTime - lastPitch >= pitchGap) {
				lastPitch += pitchGap ;
				pitcher.Pitch () ;
			}
		}
	}
}