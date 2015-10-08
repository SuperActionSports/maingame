using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class BaseballController : MonoBehaviour {
	
	//public StickeggRules scoreboard;
	public int ownNumber;
	private Rigidbody rb;
	private Renderer rend ;
	private TrailRenderer trail;
	private ParticleSystem asplode;
	private bool sploded;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		rend = GetComponent<Renderer> ();
		trail = GetComponent<TrailRenderer>();
		trail.material.color = rend.material.color;
		asplode = GetComponent<ParticleSystem> ();
		//scoreboard = FindObjectOfType ;
		ownNumber = 0;
		sploded = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "killzone") {
			if (ownNumber > 0 && !sploded){
				moveToBack () ;
			}
			else if (sploded)
			{
				rb.velocity = Vector3.zero ;
			}
			else if (ownNumber == 0){
				Destroy (gameObject);
			}
		}
		if (other.gameObject.tag == "field") {
			if (sploded) {
				rb.velocity = Vector3.zero ;
			}
			else if (ownNumber == 0){
				Destroy (gameObject) ;
			}
		}
	}

	private void Scoresplosion () {
		asplode.Play ();
		AudioSource audio = GetComponent<AudioSource>() ;
		audio.Play ();
	}

	private void moveToBack() {
		sploded = true;
		transform.tag = "deadball";
		rb.AddForce(new Vector3(0, 0, 2));
	}

	public void ChangeOwnership (int hitter, Color hitColor, Vector3 hitForce)
	{
		ownNumber = hitter;
		rend.material.color = hitColor;
		trail.material.color = hitColor;
		asplode.startColor = hitColor;
		Scoresplosion();
		rb.velocity = Vector3.zero;
		rb.AddForce (hitForce);
	}
}	