using UnityEngine;
using System.Collections;

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
		} 
		else if (other.gameObject.tag == "Equipment") {
			// set playerHit to player# of player who hit the ball
		} 
	}
}	