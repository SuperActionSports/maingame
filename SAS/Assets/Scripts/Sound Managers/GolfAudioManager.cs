using UnityEngine;
using System.Collections;

public class GolfAudioManager : MonoBehaviour {

	AudioSource[] sounds;
	// Use this for initialization
	void Start () {
		sounds = GetComponents<AudioSource>();
		// Sank putt, struck putt, crowd cheer, drone
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PlaySankPutt()
	{
		sounds[0].Play ();
	}
	
	public void PlayStruckPutt()
	{
		sounds[1].Play ();
	}
	
	public void PlayCrowdCheer()
	{
		sounds[2].Play ();
	}
}
