using UnityEngine;
using System.Collections;

public class GolfPlayerAudioManager : MonoBehaviour {

	AudioSource[] audioSource;
	// Use this for initialization
	void Start () {
		audioSource = GetComponents<AudioSource>();
		//Swing, Death
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PlaySwing()
	{
		audioSource[0].Play();
	}
	
	public void PlayDead()
	{
		audioSource[1].Play();
	}
}
