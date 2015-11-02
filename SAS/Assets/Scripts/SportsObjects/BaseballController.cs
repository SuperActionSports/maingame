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
	public ConfettiScript victoryEffect;
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rend = GetComponent<Renderer> ();
		trail = GetComponent<TrailRenderer>();
		trail.material.color = rend.material.color;
		asplode = GetComponent<ParticleSystem> ();
		//scoreboard = FindObjectOfType ;
		ownNumber = 0;
		sploded = false;
		victoryEffect = GetComponentInChildren<ConfettiScript>();
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
		else if (other.gameObject.CompareTag ("Equipment")) {
			int xForce = Mathf.FloorToInt(Random.Range(5,15));
			xForce *= other.transform.eulerAngles.y > 0 ? -1 : 1;
			rb.constraints = RigidbodyConstraints.None;
			float yForce = Random.Range(2,5);
			float zForce = 0;
			while (Mathf.Abs(zForce) < 0.3)
			{
				zForce = Random.Range(-2,2);
			}
			ChangeOwnership(1, other.transform.parent.GetComponentInParent<BaseballPlayerController>().color, new Vector3 (xForce, yForce, zForce));
			
		}
		else if (other.gameObject.tag == "field") {
			victoryEffect.Party(rend.material.color);
			victoryEffect.gameObject.transform.parent = null;
			gameObject.SetActive(false);
			/*
			if (sploded) {
				rb.velocity = Vector3.zero ;
			}
			
			else if (ownNumber == 0){
				Destroy (gameObject) ;
			}
			*/
		}
	}

	private void Scoresplosion () {
		asplode.Play ();
		AudioSource audio = GetComponent<AudioSource>() ;
		audio.Play ();
	}

	private void moveToBack() {
		sploded = true;
		transform.tag = "Deadball";		
		trail.enabled = false;
		rb.constraints = RigidbodyConstraints.None;
		rb.AddForce(new Vector3(0, 0, Random.Range(-2,2) *0.9f));
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