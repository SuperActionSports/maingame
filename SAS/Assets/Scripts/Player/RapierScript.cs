using UnityEngine;
using System.Collections;

public class RapierScript : MonoBehaviour {

	public bool owned;
	public GameObject owner;
	public bool available;
	public bool hasHit;
	public GameObject parent;
	private SphereCollider pickUpCollider;
	private CapsuleCollider attackCollider;
	private SetColorToParent colorScript;
	private EquipmentThrow equipmentThrow;
	public Color c;
	public string trueName;
	AudioSource sound;
	// Use this for initialization
	void Start () {		
		sound = GetComponentInParent<AudioSource>();
		hasHit = false;
		available = false;
		owned = false;
		resetOwnership();
		/*Transform grand = transform.parent;
		Debug.Log("Truename is " + trueName);
		while ((grand != null) && grand.name != trueName)
		{
			grand = grand.transform.parent;
		}
		parent = grand.transform.gameObject;*/
		equipmentThrow = GetComponent<EquipmentThrow>();
		pickUpCollider = GetComponent<SphereCollider>();
		attackCollider = GetComponent<CapsuleCollider>();
		pickUpCollider.enabled = false;
		colorScript = GetComponent<SetColorToParent>();
		colorScript.ResetColor(c);
		GetComponent<SetTrailRenderToParent>().ResetColor(c);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -10)
		{
			transform.position = new Vector3 (transform.position.x, 0.5f, transform.position.z);
		}
		
	}
	
	public void setArmed(bool armed)
	{
		GetComponent<CapsuleCollider>().enabled = armed;
	}
	
	public bool Available()
	{
		return !owned && hasHit;
	}
	
	public void Attack()
	{
		attackCollider.enabled = true;
		GetComponent<Rigidbody>().detectCollisions = true;
		Debug.Log(transform.gameObject.name + " is attacking!");		
	}
	
	public void StopAttack()
	{
		attackCollider.enabled = false;
		GetComponent<Rigidbody>().detectCollisions = false;
		Debug.Log(transform.gameObject.name + " stopped attacking!");
	}
	
	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Trigger is on! I've hit " + other.gameObject);
		if (other.CompareTag("Player") && owned)
		{
			//Attacking rapier is about to make swiss cheese of a player
			PlayerControllerMatt victim = other.GetComponent<PlayerControllerMatt>();
			if (victim.alive)
			{	
				sound.Play();
				victim.Kill(new Vector3 (transform.position.x * -1, transform.position.y,transform.position.z));
				GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
				
			}
		}
		else if(hasHit == false && (other.CompareTag("Stage") || other.CompareTag("Player")) && other.gameObject != parent)
		{
			//Thrown rapier is about to make a player-kebab 
			hasHit = true;
			setArmed(false);
			GetComponent<Renderer>().material.color = Color.black;
			GetComponent<CapsuleCollider>().enabled = false;
			pickUpCollider.enabled = true;
			Debug.Log("Hit stage 1");
		}
		else if (hasHit && other.CompareTag("Player") && !other.GetComponent<PlayerControllerMatt>().armed)
		{
			//Rapier is getting picked up 
			other.GetComponent<PlayerControllerMatt>().PickUp(this.transform.gameObject);
			pickUpCollider.enabled = false;
			colorScript.ResetColor();
		}
		Debug.Log("That didn't work, so now I'm going to try to see if " + other.tag + " is \"Player\" and if I'm owned: " + owned);
	}
	
	public void resetOwnership()
	{
		if (!owned && transform.parent != null)
		{
			owned = true;
			Transform grand = transform.parent;
			while (grand.transform.parent!= null && grand.transform.parent.name != "Players")
			{
				grand = grand.transform.parent;
			}
			parent = grand.transform.gameObject;
		}
		else
		{
			transform.parent = null;
			owned = false;
			GetComponent<SetColorToParent>().ResetColor();
			GetComponent<SetTrailRenderToParent>().ResetColor();
		}
	}
	
}
