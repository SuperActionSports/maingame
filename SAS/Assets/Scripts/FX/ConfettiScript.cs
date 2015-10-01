using UnityEngine;
using System.Collections;

public class ConfettiScript : MonoBehaviour {

	public KeyCode replay;
	public Color c;
	public ParticleSystem[] parts;
	// Use this for initialization
	void Start () {
		parts = GetComponentsInChildren<ParticleSystem>();
		
		Debug.Log(parts[0]);
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
}
