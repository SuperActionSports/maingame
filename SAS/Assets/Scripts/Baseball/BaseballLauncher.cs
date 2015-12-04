using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BaseballLauncher : MonoBehaviour {

	public float minXPow = -15;
	public float maxXpow = -4;
	public float minYPow = 6;
	public float maxYPow = 8;
	public Rigidbody ball ;
	
	void Start()
	{
	}				
				
	public void Pitch () {
		Rigidbody egg = Instantiate(ball, transform.position-new Vector3(0,0,0.8f), Quaternion.identity) as Rigidbody ;
		float yPow = Random.Range(minYPow,maxYPow);
		float xPow = Random.Range(minXPow,maxXpow);
		//This should be relative to yPow. A higher yPow would mean a lower permissible x range. A lower yPow would necessitate a higher xPow.
		Vector3 pitchPow = new Vector3 (xPow,yPow, 0) ;
		egg.AddForce(pitchPow);
		AudioSource audio = GetComponent<AudioSource>() ;
		audio.Play ();
	}
}