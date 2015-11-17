using UnityEngine;
using System.Collections;

public class GolfBall : MonoBehaviour {


	public bool beingHit;
	private Rigidbody rb;
	private Renderer rend;
	private GolfPlayerController playerHitting;
	public GolfWizard wizard;

	void Start () {
		rb = GetComponent<Rigidbody>();
		rend = GetComponent<Renderer> ();
		beingHit = false;
	}

	void Update () {
		// Slow to Stop over Time
		/*
		float frictionX = friction;
		float frictionZ = friction;
		if (rb.velocity.x > 0) { 
			frictionX *= -1;
		}
		if (rb.velocity.z > 0) { 
			frictionZ *= -1;
		}
		rb.velocity = new Vector3 (rb.velocity.x + frictionX, rb.velocity.y, rb.velocity.z + frictionZ);
		if (rb.velocity.x <= friction && rb.velocity.x >= -friction) {
			rb.velocity = new Vector3 (0, rb.velocity.y, rb.velocity.z);
		}
		if (rb.velocity.z <= friction && rb.velocity.z >= -friction) {
			rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y, 0);
		}
		*/

		// Lock Ball in Place if Someone is Putting with it
		if (beingHit) {
			rb.velocity = Vector3.zero;
			rb.constraints = RigidbodyConstraints.FreezeAll;
		} else {
			rb.constraints = RigidbodyConstraints.None;
		}

		if (transform.position.y < -50) { 
			transform.position = new Vector3 (transform.position.x, 5f, transform.position.z);
			rb.velocity = Vector3.zero;
		}
	}

	void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag == "Goal") {
			wizard.Celebrate(rend.material.color);
			playerHitting.stats.AddMadePutt();
			wizard.ResetBallAndHole(this.gameObject);
			collision.gameObject.transform.position = new Vector3 (Random.Range (-16f, 16f), transform.position.y, Random.Range (-16f, 16f));
			transform.position = new Vector3 (Random.Range (-16f, 16f), transform.position.y, Random.Range (-16f, 16f));
			rb.velocity = Vector3.zero;
			rend.material.color = Color.white;
		}
	}

	public void Putt(Vector3 force, GolfPlayerController player) {
		playerHitting = player;
		Color c1 = player.color;
		if (beingHit) {
			rb.constraints = RigidbodyConstraints.None;
			rb.AddForce (new Vector3(force.x, 0, force.z));
			rend.material.color = c1;
			beingHit = false;
		}
	}
}
