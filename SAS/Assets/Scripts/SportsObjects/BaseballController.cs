using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BaseballController : MonoBehaviour {

	public int playerHit = 0 ;

	void Start () {
	
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
			// explosion visual
			AudioSource audio = GetComponent<AudioSource>() ;
			audio.Play ();
		} 
		else if (other.gameObject.tag == "Equipment") {
			// set playerHit to player# of player who hit the ball
		} 
	}
}	