using UnityEngine;
using System.Collections;

public class StickeggRules : MonoBehaviour {
	
	public BaseballLauncher pitcher ;
	public AudioClip charge ;
	private float gameTime = 0.0f ;
	private float gameLength = 60.0f ;
	private float pitchGap = 4.0f ;
	private float lastPitch = 5.4f ;

	void Start () {

	}
	
	void Update () {
		gameTime += Time.deltaTime ;
		if (gameTime <= gameLength) {
			if (gameLength - gameTime <= 15.0f) {
				pitchGap = 2.0f;
				AudioSource.PlayClipAtPoint (charge, transform.position);
			}
			if (gameTime - lastPitch >= pitchGap) {
				lastPitch += pitchGap;
				pitcher.Pitch ();
			}
		}
	}
}