﻿using UnityEngine;
using System.Collections;

public class SetColorToParent : MonoBehaviour {
	
	private float t;
	// Use this for initialization
	void Start () {
	ResetColor();
	}
	
	// Update is called once per frame
	void Update () {
		if (t < 1)
		{
			t+=Time.deltaTime;
			ResetColor();
		}
	}
	
	public void ResetColor()
	{
		Renderer r = GetComponent<Renderer>();
		if (transform.parent != null)
		{
			
			Renderer rp = transform.parent.GetComponent<Renderer>();
			r.material.color = Color.Lerp(Color.black,rp.material.color,t);
		}
		else{
			r.material.color = Color.Lerp(r.material.color,Color.black,t);
		}
	}
}