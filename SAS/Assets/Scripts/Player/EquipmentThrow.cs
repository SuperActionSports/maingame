using UnityEngine;
using System.Collections;

public class EquipmentThrow : MonoBehaviour {

	public float speed; 
	private Rigidbody rb; 
	public float timeTilGravity;
	private float spawnTime;
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
		rapierScript = GetComponent<RapierScript>();
		DeactivateRigidbody();
		timeTilGravity = 0.5f;
		thrown = false;
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
	
	public void DeactivateRigidbody()
	{
		rb.isKinematic = true;
		rb.detectCollisions = false;
	}
	
	public void ActivateRigidbody(bool useGravity)
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
	}
	
	public void Throw(bool runThrow)
	{
		transform.parent = null;
		ActivateRigidbody(false);
		rapierScript.Attack();
		frame.enabled = true;
		transform.rotation = new Quaternion(0,0,-directionModifier,1);
		if (runThrow) {
			//transform.position = new Vector3 (transform.position.x, transform.position.y, 0);
		}
		Vector3 force = runThrow ? transform.up * runThrowForce : transform.up * normalThrowForce;
		rb.AddForce(force,ForceMode.VelocityChange);
		spawnTime = Time.time;
		thrown = true;
		
	}	
	
	public void PickUp(PlayerControllerMatt newOwner)
	{
		rb = transform.GetComponent<Rigidbody>();
		DeactivateRigidbody();
		
		timeTilGravity = 0.5f;
		spawnTime = Time.time;//update attack collider and owned
		thrown = false;	
		rapierScript.ResetOwnership(newOwner);
	}
}
