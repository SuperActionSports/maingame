using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BaseballController : MonoBehaviour {
	
	//public StickeggRules scoreboard;
	private Rigidbody rb;
	private Renderer rend ;
	private TrailRenderer trail;
	private ParticleSystem asplode;
	private int ownNumber;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		rend = GetComponent<Renderer> ();
		trail = GetComponent<TrailRenderer>();
		trail.material.color = rend.material.color;
		asplode = GetComponent<ParticleSystem> ();
		//scoreboard = FindObjectOfType ;
		ownNumber = 0;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "field" || other.gameObject.tag == "killzone") {
			if (ownNumber > 0){
				Scoresplosion () ;
			}
			Destroy (gameObject);
		}
	}

	private void Scoresplosion () {
		AudioSource audio = GetComponent<AudioSource>() ;
		audio.Play ();
		asplode.Play ();
		/*
		switch (ownNumber)
		{
			case 1:
				scoreboard.p1Score += 4;
				break ;
			case 2:
				scoreboard.p2Score += 4;
				break ;
			case 3:
				scoreboard.p3Score += 4;
				break ;
			case 4:
				scoreboard.p4Score += 4;
				break ;
			default:
				break ;
		}
		*/
	}

	public void ChangeOwnership (int hitter, Color hitColor, Vector3 hitForce)
	{
		ownNumber = hitter;
		rend.material.color = hitColor;
		trail.material.color = hitColor;
		asplode.startColor = hitColor;
		rb.velocity = Vector3.zero;
		rb.AddForce (hitForce);
	}
}	