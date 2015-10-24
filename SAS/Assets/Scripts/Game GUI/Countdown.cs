﻿using UnityEngine;
using System.Collections;

public class Countdown : MonoBehaviour {

	// Use this for initialization
	public FencingWizard wizard;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void StartPlay()
	{
		Debug.Log("Plee");
		if (wizard != null)
		{
			wizard.EnableMovement();
		}
	}
	
	public void NoPlay()
	{
		Debug.Log("No plee");
		if (wizard != null)
		{
			wizard.DisableMovement();
		}
	}
}