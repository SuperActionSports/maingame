using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {

	Vector3 position;
	Rigidbody rb;
	GameObject[] players;
	int count;
	bool hasHitTurf;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		players = GameObject.FindGameObjectsWithTag ("Player");
		Quaternion angle = Quaternion.AngleAxis(30.0f, Vector3.right);
		rb.AddForce(angle * -transform.forward * 1000);
		hasHitTurf = false;
		count = 0;
	}

	void Update()
	{

	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Turf") {
			count++;
			if (count >= 25) {
				Destroy (transform.gameObject);
			}
			//CheckForDoubleBounce();
		} else if (collision.gameObject.tag == "Wall") 
		{
			hasHitTurf = false;
			Destroy(transform.gameObject);		
		}
		else 
		{
			hasHitTurf = false;
		}
	}

	void CheckForDoubleBounce()
	{
		if (hasHitTurf) 
		{
			Debug.Log ("Double bounce");
			Destroy (transform.gameObject);
		} 
		else 
		{
			hasHitTurf = true;
		}
	}
}
