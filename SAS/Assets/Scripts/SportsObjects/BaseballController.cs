using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BaseballController : MonoBehaviour {

	public int playerHit = 0 ;
	private Renderer rend ;
	private 

	void Start () {
		rend = GetComponent<Renderer> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y < 0) {
			Destroy (gameObject);
		}

	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "field") {
			Destroy (gameObject);
		} 
		else if (other.gameObject.tag == "killzone") {
			Destroy (gameObject);
			AudioSource audio = GetComponent<AudioSource>() ;
			audio.Play ();
			// explosion visual
		} 
		else if (other.gameObject.tag == "Equipment") {
			// set playerHit to player# of player who hit the ball
			PlayerController scorer = other.transform.root.GetComponent<PlayerController> () ;
			rend.material.color = scorer.c1 ;
		} 
	}
}	