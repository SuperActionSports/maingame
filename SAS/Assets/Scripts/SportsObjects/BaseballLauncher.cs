using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BaseballLauncher : MonoBehaviour {

	public Rigidbody ball ;
	private bool loading ;


	void Start () {
		loading = true ;
		// play 'charge' here
		Invoke ("Pitch", 4.0f) ;
	}

	void Update () {
		if (!loading) {
			loading = true ;
			Invoke ("Pitch", 4.0f) ;
		}
	}
				
	void Pitch () {
		Rigidbody egg = Instantiate(ball, transform.position, Quaternion.identity) as Rigidbody ;
		Vector3 pitchPow = new Vector3 (Random.Range(-23, -2), 40, 0) ;
		egg.AddForce(pitchPow);
		AudioSource audio = GetComponent<AudioSource>() ;
		audio.Play ();
		loading = false ;
	}
}
