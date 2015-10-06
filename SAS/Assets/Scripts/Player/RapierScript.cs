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
	private Rigidbody rb;
	public Color c;
	public string trueName;
	AudioSource sound;
	// Use this for initialization
	void Start () {		
		sound = GetComponentInParent<AudioSource>();
		hasHit = false;
		available = false;
		owned = true;
		//resetOwnership();
		equipmentThrow = GetComponent<EquipmentThrow>();
		pickUpCollider = GetComponent<SphereCollider>();
		attackCollider = GetComponent<CapsuleCollider>();
		pickUpCollider.enabled = false;
		colorScript = GetComponent<SetColorToParent>();
		rb = GetComponent<Rigidbody>();
//		Debug.Log("Is rb a thing to RapierScript? " + rb);
		ResetColor();
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
		Debug.Log("Attacking!");
		attackCollider.enabled = true;
		GetComponent<CapsuleCollider>().enabled = true;
		rb.detectCollisions = true;
		if (!owned && transform.parent != null)
		{
			Debug.Log("And I'm loved!");
			owned = true;
		}
	}
	
	public void StopAttack()
	{
		if (owned)
		{
			attackCollider.enabled = false;
			rb.detectCollisions = false;
		}
	}
	
	private void StopMoving()
	{
		rb.velocity = new Vector3(0,0,0);
		StopAttack();
		if (equipmentThrow.thrown)
		{
			Parry ();
		}
	}
	
	private void Parry()
	{
		hasHit = true;
		setArmed(false);
		GetComponent<Renderer>().material.color = Color.black;
		GetComponent<CapsuleCollider>().enabled = false;
		pickUpCollider.enabled = true;
		equipmentThrow.Drop();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player")) Debug.Log("Other: " + other.gameObject.name + " owned: " + owned);
		if (other.CompareTag("Player") && owned)
		{
			//Attacking rapier is about to make swiss cheese of a player
			PlayerControllerMatt victim = other.GetComponent<PlayerControllerMatt>();
			if (victim.alive && other.gameObject != parent)
			{	
				Debug.Log("I'm gonna hit " + other.transform.gameObject.name);
				//Debug.Log("and my parent is " + parent.gameObject.name);
//				sound.Play();
				victim.Kill(new Vector3 (transform.position.x * -1, transform.position.y,transform.position.z));
				if (equipmentThrow.thrown)
				{ 
					StopMoving();
					ResetOwnership();
				}
				
			}
		}
		else if (other.CompareTag("Shield"))
		{
			Debug.Log("Path of the shield");
			if (owned)
			{
				if (!equipmentThrow.thrown)  transform.parent.transform.parent.GetComponent<PlayerControllerMatt>().armed = false;
				equipmentThrow.Drop();
			}
			Parry ();
			StopMoving();
		}
		else if(hasHit == false && (other.CompareTag("Stage") || other.CompareTag("Player")) && other.gameObject != parent)
		{
			Debug.Log("Path of the unhit");
			Debug.Log("Owned? " + owned + " Thrown? " + equipmentThrow.thrown);
			if (!owned || equipmentThrow.thrown) {
				Parry ();
				}
		}
		else if (hasHit && other.CompareTag("Player") && !other.GetComponent<PlayerControllerMatt>().armed)
		{
			Debug.Log("Path of the pickup");
			//Rapier is getting picked up 
			other.GetComponent<PlayerControllerMatt>().PickUp(this.transform.gameObject);
			pickUpCollider.enabled = false;
			colorScript.ResetColor();
		}
		//Debug.Log("That didn't work, so now I'm going to try to see if " + other.tag + " is \"Player\" and if I'm owned: " + owned);
	}
	
	public void ResetOwnership()
	{
		//Debug.Log("Ownership resetting");
		if (!owned && transform.parent != null)
		{
			owned = true;
			hasHit = false;
			equipmentThrow = GetComponent<EquipmentThrow>();
			pickUpCollider = GetComponent<SphereCollider>();
			attackCollider = GetComponent<CapsuleCollider>();
		}
		else
		{
			if (transform.parent == null) 
			{
				Debug.Log("I have no owner!");
				owned = false;
			}
			ResetColor();
		}
	}
	
	public void ResetOwnership(GameObject newOwner)
	{
		//Debug.Log("Ownership resetting");
		owned = true;
		hasHit = false;
		owner = newOwner;
		ResetColor();
		equipmentThrow = GetComponent<EquipmentThrow>();
		pickUpCollider = GetComponent<SphereCollider>();
		attackCollider = GetComponent<CapsuleCollider>();
		
	}
	
	public void ResetColor()
	{
		GetComponent<AccessoryColor>().ResetColor();
		GetComponent<AccessoryColor>().ResetColor(c);
	}
	
	public void MakeDangerous()
	{
		attackCollider.enabled = true;
	}
	
	public void MakeSafe()
	{
	
	}
	
}
