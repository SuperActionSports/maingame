using UnityEngine;
using System.Collections;

public class ManyEffectSingleEventAudioManager : MonoBehaviour {

	AudioSource[] sounds;
	
	void Start () {
		sounds = GetComponents<AudioSource>();
	}
	
	public void Play()
	{
		sounds[Random.Range(0,sounds.Length)].Play();
	}
}
