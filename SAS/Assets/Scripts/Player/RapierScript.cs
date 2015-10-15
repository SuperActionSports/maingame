using UnityEngine;
using System.Collections;

public class RapierScript : MonoBehaviour {

	public GameObject owner;
	public bool hasHit;
	public SphereCollider pickUpCollider;
	public CapsuleCollider attackCollider;
	private SetColorToParent colorScript;
	private EquipmentThrow equipmentThrow;
	private Rigidbody rb;
	public Color c;
	public string trueName;
	AudioSource sound;
	private float timer = 0;
	// Use this for initialization
	void Start () {		
		sound = GetComponentInParent<AudioSource>();
		hasHit = false;
		equipmentThrow = GetComponent<EquipmentThrow>();
		pickUpCollider = GetComponent<SphereCollider>();
		attackCollider = GetComponent<CapsuleCollider>();
		pickUpCollider.enabled = false;
		colorScript = GetComponent<SetColorToParent>();
		rb = GetComponent<Rigidbody>();
		ResetColor();
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
/*	public void setArmed(bool armed)
	{
		GetComponent<CapsuleCollider>().enabled = armed;
	}
*/	
	public bool Available()
	{
		return owner == null && hasHit;
	}

	public void Attack()
	{
		attackCollider.enabled = true;
		equipmentThrow.ActivateRigidbody (false);
	}
	
	public void StopAttack()
	{
		attackCollider.enabled = false;
		equipmentThrow.DeactivateRigidbody ();
	}
	
	private void StopMoving()
	{
		rb.velocity = new Vector3(0,0,0);
	}
	
	public void Parry()
	{
		hasHit = true;
		StopMoving ();
		StopAttack ();
		equipmentThrow.Drop();
		DeactivateRapier ();
	}

	private void DeactivateRapier()
	{
		c = Color.black;
		ResetColor ();
		attackCollider.enabled = false;
		pickUpCollider.enabled = true;
		Rigidbody rb = GetComponent<Rigidbody> ();
		timer = 60f;
		owner = null;
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("LOGGING3");
		Debug.Log ("I have collided with " + other.gameObject.name);
		if (other.CompareTag("Player") && owner != null)
		{
			//Attacking rapier is about to make swiss cheese of a player
			PlayerControllerMatt victim = other.GetComponent<PlayerControllerMatt>();
			if (victim.alive && other.gameObject != owner)
			{	
//				sound.Play();
				if (equipmentThrow.thrown)
				{ 
					Parry();
				}
				victim.Kill(new Vector3 (transform.position.x * -1, transform.position.y,transform.position.z));
			}
		}
		else if (other.CompareTag("Shield"))
		{
			if (owner != null)
			{
				if (!equipmentThrow.thrown)  transform.parent.transform.parent.GetComponent<PlayerControllerMatt>().armed = false;
				//equipmentThrow.Drop();
			}
			Parry ();
		}
		else if (!hasHit && other.CompareTag("Stage"))
		{
			if (owner == null || equipmentThrow.thrown) {
				Debug.Log ("WALL/FLOOR COLLISION");
				Parry ();
			}
		}
		else if (hasHit && other.CompareTag("Player") && !other.GetComponent<PlayerControllerMatt>().armed)
		{
			//Rapier is getting picked up 
			other.GetComponent<PlayerControllerMatt>().PickUp(this.transform.gameObject);
			pickUpCollider.enabled = false;
		}
	}
	
/*	public void ResetOwnership()
	{
		//Debug.Log("Ownership resetting");
		if (!owned && owner != null)
		{
			owned = true;
			hasHit = false;
			equipmentThrow = GetComponent<EquipmentThrow>();
			pickUpCollider = GetComponent<SphereCollider>();
			attackCollider = GetComponent<CapsuleCollider>();
		}
		else
		{
			if (owner == null) 
			{
				Debug.Log("I have no owner!");
				owned = false;
			}
			ResetColor();
		}
	}
*/	
	public void ResetOwnership(GameObject newOwner)
	{
		hasHit = false;
		owner = newOwner;
		ResetColor();
	}
	
	public void ResetColor()
	{
		GetComponent<Renderer> ().material.color = c;
		GetComponent<TrailRenderer>().material.color = c;
	//	GetComponent<AccessoryColor>().ResetColor();
	//	GetComponent<AccessoryColor>().ResetColor(c);
	}
/*
	public void MakeDangerous()
	{
		attackCollider.enabled = true;
	}
	
	public void MakeSafe()
	{
	
	}
*/
}
