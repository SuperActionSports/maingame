﻿using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {

	Vector3 position;
	//Rigidbody rb;
	GameObject lastHitBy;
	int count;
	bool hasHitTurf;
	int value;
	public Vector3 vel;
	public bool hit;
	public TennisWizard wizard;
	public float deathTime = 1;
	public float currentDeathTime = 0;
	private AudioSource ballSound;

	private Renderer tr;
	// Use this for initialization
	void Start () 
	{
		ResetValue();
		tr = GetComponent<TrailRenderer>().GetComponent<Renderer>();
		hasHitTurf = false;
		count = 0;
		hit = false;
		ballSound = GetComponent<AudioSource>();
	}

	void Update()
	{
		
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Turf") {
			count++;
			CheckForDoubleBounce();
			ballSound.Play();
		} else if (collision.gameObject.tag == "Wall") 
		{
		//	hasHitTurf = false;
		}
		else if (collision.gameObject.CompareTag("BrickWall"))
		{
		Debug.Log("Ball hit wall");
			if (!hasHitTurf)
			{
				wizard.GetComponent<TennisWizard>().BallHitWall();
				IncreaseValue();
			}
			ballSound.Play();
		}
		else
		{
			//hasHitTurf = false;
		}
	}
	
	void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.CompareTag("Turf"))
		{
			currentDeathTime += Time.deltaTime;
			if (currentDeathTime >= deathTime)
			{
				KillBall();
			}
		}	
	}
	
	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.CompareTag("Turf"))
		{
			currentDeathTime = 0;
		}
	}

	void CheckForDoubleBounce()
	{
		if (hasHitTurf) 
		{
		//	Debug.Log ("Double bounce");
			if (lastHitBy != null) lastHitBy.GetComponent<TennisControllerGans>().ScorePoints(value);
			value = 0;
			wizard.BallHitTurfTwice();
		} 
		else 
		{
			hasHitTurf = true;
		}
	}
	
	public void ResetValue()
	{
		value = 0;
	}
	
	public void IncreaseValue()
	{
		value = Mathf.Clamp(value + 1,1,10);
	}
	
	public void ResetCount()
	{
		count = 0;
	}
	
	public void Hit(GameObject hittee)
	{
		lastHitBy = hittee;
		tr.material.color = hittee.GetComponent<TennisControllerGans>().color;
		GetComponent<TrailRenderer>().material.color = hittee.GetComponent<TennisControllerGans>().color;
		hasHitTurf = false;
	}
	
	public void KillBall()
	{
		wizard.KillBall();
		Destroy(this.gameObject);
	}
}
