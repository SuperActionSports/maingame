using UnityEngine;
using System.Collections;

public class ConfettiScript : MonoBehaviour {

	public KeyCode replay;
	public Color c;
	public ParticleSystem[] parts;
	public ParticleAnimator[] partAnimators;
	public bool active;
	// Use this for initialization
	void Start () {
		active = true;
		parts = GetComponentsInChildren<ParticleSystem>();
		partAnimators = GetComponentsInChildren<ParticleAnimator>();
		//Debug.Log(parts[0]);
	}
	
	// Update is called once per frame
	void Update () {
		 if (Input.GetKeyDown(replay))
		 {
		 	foreach (ParticleSystem p in parts)
		 	{
		 		p.Play ();
		 		
		 	}
		 }
	}
	
	public void Party()
	{
		transform.parent = null;
		Debug.Log("It is time to party.");
		foreach (ParticleSystem p in parts)
		{
			Debug.Log(p.gameObject.name + " is partying.");
			p.Play ();
			
		}
	}
}
