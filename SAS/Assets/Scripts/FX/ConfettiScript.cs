﻿using UnityEngine;
using System.Collections;

public class ConfettiScript : MonoBehaviour {

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
		c = GetComponentInParent<PlayerControllerMatt>().color;
		countdown = 10;
		played = false;
		//ParticleAnimator particleAnimator = transform.GetComponentInChildren<ParticleAnimator>();
		//Color[] modifiedColors = particleAnimator.colorAnimation;
		//modifiedColors[2] = Color.yellow;
		//particleAnimator.colorAnimation = modifiedColors;
		for(int i = 0; i < parts.Length; i++)
		{
			partRends[i].material.color = c;
			parts[i].startColor = c;
		}
	}
	
	// Update is called once per frame
	void Update () {
		 if (Input.GetKeyDown(replay))
		 {
		 	Party();
		 }
		 if (played)
		 {
		 	if (countdown <= 0)
		 	{
		 		Destroy(transform.gameObject);
		 	}
		 	else
		 	{
		 		countdown -= Time.deltaTime;
		 	}
		 }
	}
	
	public void Party()
	{
		transform.parent = null;
		foreach (ParticleSystem p in parts)
		{
			p.Play ();
			
		}
		played = true;
	}
}