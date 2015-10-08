using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BaseballLauncher : MonoBehaviour {

	public Rigidbody ball ;
				
	public void Pitch () {
		Rigidbody egg = Instantiate(ball, transform.position, Quaternion.identity) as Rigidbody ;
		Vector3 pitchPow = new Vector3 (Random.Range(-15, -2), 11, 0) ;
		egg.AddForce(pitchPow);
		AudioSource audio = GetComponent<AudioSource>() ;
		audio.Play ();
	}
}