using UnityEngine;
using System.Collections;

public class FencingPlayerController : MonoBehaviour {

	/*
	Color color;
	SphereCollider shield
	bool Armed
	GameObject Equipment
	FencingEquipment equipScript
	Animator anim
	bool Alive
	GameObject[] respawnPoints
	Rigidbody rb
	Wizard wizard
	FencingCamera cam
	bool MovementAllowed
	Statscard stats
	GameObject equipmentHand
	*/
	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {
	
	}
	
	/*
	/// <summary>
	/// Starts the Attack animation. Other attack methods are exclusively called by the animator.
	/// </summary>
	public void Attack()
	{
		anim.SetTrigger("Attack");
		stats.AddStabAttempts ();
	}
		public void AttackStart()
	{
		equipScript.Attack();
	}
	
	public void AttackEnd()
	{
		equipScript.StopAttack();
	}
	public void ThrowRapier()
	{
		anim.SetTrigger("Throw");
		stats.AddThrowAttempts ();
	}
	
	public void ThrowEquipment()
	{
		int directionModifier = input.facingRight ? 1 : -1;
		equipScript.Throw(anim.GetFloat("Run")>17,directionModifer);
		
		if (equipmentThrow == null)
		{
			Debug.Log("Equipment " + equipment + " has no EquipmentThrow script.");
		}
		else
		{
			armed = false;
			rapierScript.Attack();	
			equipmentThrow.directionModifier = input.facingRight ? 1 : -1;
			equipmentThrow.Throw(anim.GetFloat("Run") > 17);
			equipment = null;
			equipmentThrow = null;	
			rapierScript = null;
		}
	}
	
	public bool GetHit()
	{
		if (!anim.getBool("Counter"))
		{
			MakeDead();
		}
	}
	
		private void MakeDead()
	{
		deathScript.Party(color);
        rb.constraints = RigidbodyConstraints.None;
        alive = false;
        cam.PlayShake(transform.position);
		if (rapierScript != null) { rapierScript.Deflect();}
        gameObject.SetActive(false);
        Debug.Log("Finna update player count.");
        wizard.UpdatePlayerCount();
        Debug.Log("Yeah.");
        Destroy(gameObject,3);
		stats.EndLifeTime ();
		stats.AddDeath ();

    }
	
	public void Counter()
	{
		anim.setBool("Counter", true);
	}
	
	public void StartCounter()
	{
		shield.enabled = true;
	}	
	
	public void EndCounter()
	{
		shield.enabled = false;
		anim.setBool("Counter", false);
	}
	
	public GameObject PickUp(GameObject equip)
	{
		equipScript = equip.GetComponent<FencingEquipment>();
		equip.transform.parent = equipHand;
		return this.gameObject;
	}
	
		private void ResetRigidBodyConstraints() 
	{
		rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
		transform.rotation = Quaternion.identity;
	}
	
		private void ResetRotation()
	{
		if (transform.position.x > 0) 
		{
			transform.eulerAngles = new Vector3(0,180,0);
			input.facingRight = false;			
		}
		else{
			transform.eulerAngles = new Vector3(0,0,0);
			input.facingRight = true;
		}
	}
	
		private void ResetRigidBodyConstraints() 
	{
		rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
		transform.rotation = Quaternion.identity;
	}
	*/
}
