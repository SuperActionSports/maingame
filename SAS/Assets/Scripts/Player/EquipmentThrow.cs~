using UnityEngine;
using System.Collections;

public class EquipmentThrow : MonoBehaviour {
	
	public Color c;
	public float speed; 
	private Rigidbody rb; 
	public float timeTilGravity;
	private float spawnTime;
	public bool untouched;
	public float directionModifier;
	private MeshCollider frame;
	private RapierScript rapierScript;
	private float normalThrowForce;
	private float runThrowForce;
	
	public bool thrown;
	// Use this for initialization
	void Start () {
		normalThrowForce = 60;
		runThrowForce = 80;
		//PickUp();
		frame = GetComponent<MeshCollider>();
		rb = GetComponent<Rigidbody>();
//		Debug.Log("Is rb a thing? " + rb);
		rapierScript = GetComponent<RapierScript>();
		rapierScript.owned = true;
		DeactivateRigidbody();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (thrown && Time.time >= spawnTime + timeTilGravity)
		{
			//Debug.Log("From Throw update");
			rb.useGravity = true;
			GetComponent<RapierScript>().resetOwnership();
		}
	}
	
	private void DeactivateRigidbody()
	{
		rb.isKinematic = true;
		rb.detectCollisions = false;
	}
	
	private void ActivateRigidbody(bool useGravity)
	{
		rb.detectCollisions = true;
		rb.isKinematic = false;	
		rb.useGravity = useGravity;
		rb.constraints = RigidbodyConstraints.FreezePositionZ;
		rb.mass = 3;
	}
	
	public void Drop()
	{
		ActivateRigidbody(true);
		frame.enabled = true;
		rapierScript.resetOwnership();
		//Debug.Log("From Throw Drop");
	}
	
	public void Throw(bool runThrow)
	{
		transform.parent = null;
		ActivateRigidbody(false);
		untouched = true;
		rapierScript.Attack();
		rapierScript.parent = null;
		//owner.hasHit = false;
		frame.enabled = true;
		transform.rotation = new Quaternion(0,0,-directionModifier,1);
		rapierScript.setArmed(true);
//		if (directionModifier > 0)
//		{
//			rb.AddForce(transform.up*60f,ForceMode.VelocityChange);
//		}
//		else 
//		{
//			rb.AddForce(transform.up*60f,ForceMode.VelocityChange);
//		}
		Vector3 force = runThrow ? transform.up * runThrowForce : transform.up * normalThrowForce;
		rb.AddForce(force,ForceMode.VelocityChange);
		spawnTime = Time.time;
		thrown = true;
	}	
	
	public void PickUp()
	{
		rb = transform.GetComponent<Rigidbody>();
		if (rb != null)
		{	
			DeactivateRigidbody();
		}
		rapierScript = GetComponent<RapierScript>();
		rapierScript.owned = true;
		
		timeTilGravity = 0.4f;
		spawnTime = Time.time;
		thrown = false;	
		rapierScript.resetOwnership();
		//Debug.Log("From Pickup");
	}
}
