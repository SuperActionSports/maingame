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
		timeTilGravity = 0.5f;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (thrown && Time.time >= spawnTime + timeTilGravity)
		{
			//Debug.Log("From Throw update");
			rb.useGravity = true;
			//GetComponent<RapierScript>().resetOwnership();
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
		transform.parent = null;
		rapierScript.ResetOwnership();
	}
	
	public void Throw(bool runThrow)
	{
		transform.parent = null;
		ActivateRigidbody(false);
		untouched = true;
		rapierScript.MakeDangerous();
		rapierScript.parent = null;
		frame.enabled = true;
		transform.rotation = new Quaternion(0,0,-directionModifier,1);
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
		
		timeTilGravity = 0.5f;
		spawnTime = Time.time;//update attack collider and owned
		thrown = false;	
		rapierScript.ResetOwnership(this.gameObject);
		//Debug.Log("From Pickup");
	}
}
