using UnityEngine;
using System.Collections;

public class VictoryScript : MonoBehaviour {

	public KeyCode replay;
	public Color c;
	public ParticleSystem[] parts;
	public ParticleAnimator[] partAnimators;
	public ParticleSystemRenderer[] partRends;
	private bool played;
	public float countdown;
	// Use this for initialization
	void Start () {
		parts = GetComponentsInChildren<ParticleSystem>();
		partRends = GetComponentsInChildren<ParticleSystemRenderer>();
		countdown = 10;
		played = false;
	}
	
	// Update is called once per frame
	void Update () {
		 if (Input.GetKeyDown(replay))
		 {
		 	Party(Color.gray);
		 }
	}
	
	public void Party(Color c)
	{	
		for(int i = 0; i < parts.Length; i++)
		{
			partRends[i].material.color = c;
			parts[i].startColor = c;
		}
		foreach (ParticleSystem p in parts)
		{
			p.Play ();
			
		}
		played = true;
	}
}
