using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {

	Vector3 position;
	Rigidbody rb;
	Collider collider;

	// Use this for initialization
	void Start () {
		position = transform.position;
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = true;
		rb.AddForce (transform.forward * 5000);
		collider = GetComponent<SphereCollider> ();
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player") {
			Physics.IgnoreCollision(collision.collider, collider);
			Debug.Log ("Hit player :(");
		}
		if (collision.gameObject.tag == "BackWall") {
			rb.AddForce(transform.forward * -1000);
			Debug.Log ("Hit back wall");
		}
		if (collision.gameObject.tag == "FrontWall") {
			rb.AddForce(transform.forward * 1000);
			Debug.Log ("Hit front wall");
		}
	}
}
