using UnityEngine;
using System.Collections;

public class GolfHoleScript : MonoBehaviour {

	public GameObject confettiObject;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.CompareTag ("field")) {
			Debug.Log ("Hole down: " + transform.position);
			GetComponent<SphereCollider>().isTrigger = true;
			Rigidbody rb = GetComponent<Rigidbody>();
			rb.isKinematic = true;
			rb.useGravity = false;
			rb.velocity = Vector3.zero;
			rb.constraints = RigidbodyConstraints.FreezeAll;
			transform.position = new Vector3(transform.position.x, transform.position.y-0.4f, transform.position.z);
		}
		if (col.gameObject.CompareTag ("Ball")) {
			confettiObject.GetComponent<ConfettiScript>().Party();
		}
	}
}
