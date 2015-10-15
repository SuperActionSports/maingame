using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BaseballLauncher : MonoBehaviour {

	public float minXPow;
	public float maxXpow;
	public float minYPow;
	public float maxYPow;
	public Rigidbody ball ;
	
	void Start()
	{
		minXPow = -15;
		maxXpow = -4;
		minYPow = 6;
		maxYPow = 8;
	}				
				
	public void Pitch () {
		Rigidbody egg = Instantiate(ball, transform.position-new Vector3(0,0,2.1f), Quaternion.identity) as Rigidbody ;
		float yPow = Random.Range(minYPow,maxYPow);
		float xPow = Random.Range(minXPow,maxXpow);
		//This should be relative to yPow. A higher yPow would mean a lower permissible x range. A lower yPow would necessitate a higher xPow.
		Vector3 pitchPow = new Vector3 (xPow,yPow, 0) ;
		egg.AddForce(pitchPow);
		AudioSource audio = GetComponent<AudioSource>() ;
		audio.Play ();
	}
}