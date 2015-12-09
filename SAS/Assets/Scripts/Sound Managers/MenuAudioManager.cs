using UnityEngine;
using System.Collections;

public class MenuAudioManager : MonoBehaviour {

	AudioSource[] sounds;
	// Use this for initialization
	void Start () {
		sounds = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PlayMoveSelection()
	{
		sounds[0].Play();
	}
	
	public void PlayConfirm()
	{
		sounds[1].Play();
	}
	
	public void PlayDecline()
	{
		sounds[2].Play();
	}
	
}
