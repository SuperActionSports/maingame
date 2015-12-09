using UnityEngine;
using System.Collections;

public class BaseballAudioManager : MonoBehaviour {

	AudioSource[] sounds;
	// Use this for initialization
	void Start () {
		sounds = GetComponents<AudioSource>();
		// Cannon, Goal cheer, ball hit, drone
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PlayCannonFire ()
	{
		sounds[0].Play ();
	}
	
	public void PlayGoal ()
	{
		sounds[1].Play();
	}
	
	public void PlayBallHit ()
	{
		sounds[2].Play();
	}
}
