using UnityEngine;
using System.Collections;

public class FencingAudioManager : MonoBehaviour {

	AudioSource[] sources;
	void Start () {
		sources = GetComponents<AudioSource>();
		//Drone, cheer
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PlayCheer()
	{
		sources[1].Play();
	}
}
