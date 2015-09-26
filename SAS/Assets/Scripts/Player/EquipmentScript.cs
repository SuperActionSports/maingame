﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentScript : MonoBehaviour {
    
	//public Text scoreText;
	//private int count;
	//public Text winText;

	// Use this for initialization
	AudioSource sound;
	void Start () {
	sound = GetComponentInParent<AudioSource>();
		//count = 0;
		SetScoreText ();
		//winText.text = "";
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController victim = other.GetComponent<PlayerController>();
            if (victim.alive)
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
