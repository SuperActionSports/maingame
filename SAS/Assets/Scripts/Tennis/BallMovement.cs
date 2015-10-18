using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {

	Vector3 position;
	Rigidbody rb;
	GameObject[] players;
	int count;
	bool hasHitTurf;
	public int bounces;
	public Vector3 vel;
	public bool hit;
	public ParticleSystem indicator;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		players = GameObject.FindGameObjectsWithTag ("Player");
		Quaternion angle = Quaternion.AngleAxis(30.0f, Vector3.right);
		rb.AddForce(angle * transform.forward * 1000);
		hasHitTurf = false;
		count = 0;
		bounces = 30;
		hit = false;
	}

	void Update()
	{
		vel = rb.velocity;
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Turf") {
			count++;
			if (count > bounces) {
				Destroy (transform.gameObject);
			}
			//CheckForDoubleBounce();
		} else if (collision.gameObject.tag == "Wall") 
		{
			hasHitTurf = false;
			//Destroy(transform.gameObject);		
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
	
	public void ResetCount()
	{
		count = 0;
	}
	
	public void Hit(Color c)
	{
		//indicator.GetComponent<SetColorToOther>().ResetColor(c);
	}
}
