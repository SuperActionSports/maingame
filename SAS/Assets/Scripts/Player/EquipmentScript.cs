﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentScript : MonoBehaviour {
    
	//public Text scoreText;
	//private int count;
	//public Text winText;

	// Use this for initialization
	AudioSource sound;
	RapierOwnership owner;
	void Start () {
	sound = GetComponentInParent<AudioSource>();
		//count = 0;
		SetScoreText ();
		owner = GetComponent<RapierOwnership>();
	//winText.text = "";
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setArmed(bool armed)
	{
		GetComponent<CapsuleCollider>().enabled = armed;
	}

    void OnTriggerEnter(Collider other)
    {
    	Debug.Log("POW! I hit " + other.gameObject);
        if (other.CompareTag("Player") && !owner.Available())
        {
        	Debug.Log("And it was a player!");
            PlayerControllerMatt victim = other.GetComponent<PlayerControllerMatt>();
			Debug.Log("And the rapier that hit it hast hit? " + GetComponent<RapierOwnership>().hasHit);
            if (victim.alive && GetComponent<RapierOwnership>().hasHit)
            {	
            	sound.Play();
            	Debug.Log("Hit detected, sending "+ (transform.right*-1));
                victim.Kill(new Vector3 (transform.position.x * -1, transform.position.y,transform.position.z));
                //This causes no movement at the center of the field
			//	count++;
				SetScoreText();
            }
        }
    }

	void SetScoreText ()
	{
		//scoreText.text = "Score: " + count.ToString ();
	//	if (count >= 3)
	//	{
		//	winText.text = "Winnner!";
	//	}
	}
}
