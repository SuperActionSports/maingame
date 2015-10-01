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
		Quaternion dir = Quaternion.AngleAxis (30.0f, Vector3.forward);
		rb.AddForce (dir * transform.forward * 1500);
		collider = GetComponent<SphereCollider> ();
	}

	void Update(){

	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Player") {
			Physics.IgnoreCollision(collision.collider, collider);
			Debug.Log ("Hit player :(");
		}
		if (collision.gameObject.tag == "BackWall") {
			Debug.Log ("Hit back wall");
			Destroy(transform.gameObject);
		}
		if (collision.gameObject.tag == "FrontWall") {
			Debug.Log ("Hit front wall");
			Destroy(transform.gameObject);
		}
	}
}
