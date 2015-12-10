using UnityEngine;
using System.Collections;

public class FencingPlayerAudioManager : MonoBehaviour {

	AudioSource[] sources;
	public ManyEffectSingleEventAudioManager deathSounds;
	public ManyEffectSingleEventAudioManager clashSounds;
	void Start () {
		sources = GetComponents<AudioSource>();
		//Dead, Swing, Clash, Equip
	}
	
	public void PlaySwing()
	{
		//sources[1].Play ();
	}
	
	public void PlayClash()
	{
		//clashSounds.Play();
	}
	
	public void PlayEquip()
	{
		//sources[3].Play ();
	}
	
	public void PlayDead()
	{
		//deathSounds.Play();
	}
}
