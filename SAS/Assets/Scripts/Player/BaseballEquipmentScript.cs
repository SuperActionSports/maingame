﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseballEquipmentScript : MonoBehaviour {
    
//	public Text scoreText;
	private int count;
	private BaseballPlayerController player;
	public Text winText;

	// Use this for initialization
	void Start () {
		count = 0;
//		SetScoreText ();
//		winText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		player = transform.parent.GetComponent<BaseballPlayerController>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Player")) {
			BaseballPlayerController victim = other.GetComponent<BaseballPlayerController> ();
			if (victim.alive && player.alive) {
				Debug.Log ("Hit detected, sending " + (transform.right * -1));
				victim.Kill (new Vector3 (transform.position.x * -1, transform.position.y, transform.position.z));
				//This causes no movement at the center of the field
				AudioSource hit = GetComponent<AudioSource>() ;
				hit.Play ();
				count++;
//				SetScoreText();
			}
		} else if (other.CompareTag ("Ball")) {
			BaseballController ball = other.GetComponent<BaseballController>() ;
			int xHit = 20 ;
			if (player.transform.position.x > transform.position.x)
			{
				xHit = -20;
			}
			ball.ChangeOwnership(1, player.c1, new Vector3 (xHit, 10, 0));
		}
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Vector3 sweet = new Vector3(transform.position.x, transform.localPosition.y, transform.position.z);
        Gizmos.DrawLine(sweet, transform.forward * 1.5f);
    }

//	void SetScoreText ()
//	{
//		scoreText.text = "Score: " + count.ToString ();
//		if (count >= 3)
//		{
//			winText.text = "Winnner!";
//		}
//	}
}
