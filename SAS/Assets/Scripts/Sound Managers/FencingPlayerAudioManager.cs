using UnityEngine;
using System.Collections;

public class FencingPlayerAudioManager : MonoBehaviour {

	AudioSource[] sources;
	void Start () {
		sources = GetComponents<AudioSource>();
		//Dead, Swing, Clash, Equip
	}
	
	public void PlaySwing()
	{
		sources[1].Play ();
	}
	
	public void PlayClash()
	{
		sources[2].Play ();
	}
	
	public void PlayEquip()
	{
		sources[3].Play ();
	}
	
	public void PlayDead()
	{
		sources[0].Play ();
	}
}
