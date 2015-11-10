using UnityEngine;
using System.Collections;

public class RapierScript : MonoBehaviour {/*

	public PlayerControllerMatt owner;
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

	public bool Available()
	{
		return owner == null && hasHit;
	}

	public void Attack()
	{
		attackCollider.enabled = true;
		if (equipmentThrow.thrown) { equipmentThrow.ActivateRigidbody (false); }
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
		//Rigidbody rb = GetComponent<Rigidbody> ();
		timer = 60f;
		owner = null;
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("LOGGING3");
		Debug.Log ("I have collided with " + other.gameObject.name);
		if (other.CompareTag("Fencer") && owner != null)
		{
			//Attacking rapier is about to make swiss cheese of a player
			PlayerControllerMatt victim = other.GetComponent<PlayerControllerMatt>();
			if (victim.alive && other.gameObject != owner.gameObject)
			{	
				sound.Play();
				if (equipmentThrow.thrown)
				{ 
					Parry();
				}
				victim.Kill(new Vector3 (transform.position.x * -1, transform.position.y,transform.position.z));
				if(owner.stats.AttackFlag) {owner.stats.AddStabKills();}
				else {owner.stats.AddThrowKills();}
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
			PlayerControllerMatt victim = other.GetComponent<PlayerControllerMatt>();
			victim.stats.AddBlocksSuccessful();
		}
		else if (!hasHit && other.CompareTag("Stage"))
		{
			if (owner == null || equipmentThrow.thrown) {
				Debug.Log ("WALL/FLOOR COLLISION");
				Parry ();
			}
		}
		else if (hasHit && other.CompareTag("Fencer") && !other.GetComponent<PlayerControllerMatt>().armed)
		{
			//Rapier is getting picked up 
			other.GetComponent<PlayerControllerMatt>().PickUp(this.transform.gameObject);
			pickUpCollider.enabled = false;
		}
	}
	
	public void ResetOwnership()
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
	
	public void ResetOwnership(PlayerControllerMatt newOwner)
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

	public void MakeDangerous()
	{
		attackCollider.enabled = true;
	}
	
	public void MakeSafe()
	{
	
	}
*/
}
