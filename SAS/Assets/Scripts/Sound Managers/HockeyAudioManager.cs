using UnityEngine;
using System.Collections;

public class HockeyAudioManager : MonoBehaviour {
	
	public AudioSource[] sources;
	// Use this for initialization
	void Start () {
		sources = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PlayGoal()
	{
		sources[0].Play ();
		sources[1].Play ();
	}
	
	public void PlayHitPuck()
	{
		sources[2].Play ();
	}
	
	
}
