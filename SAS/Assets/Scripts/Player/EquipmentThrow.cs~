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
	
	public bool thrown;
	// Use this for initialization
	void Start () {
		PickUp();
		frame = GetComponent<MeshCollider>();
		rb = GetComponent<Rigidbody>();
//		Debug.Log("Is rb a thing? " + rb);
		DeactivateRigidbody();
		
	}
	
	// Update is called once per frame
	void Update () {
		if (thrown && Time.time >= spawnTime + timeTilGravity)
		{
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
	}
	
	public void Throw()
	{
		transform.parent = null;
		ActivateRigidbody(false);
		untouched = true;
		rapierScript.Attack();
		//owner.hasHit = false;
		frame.enabled = true;
		transform.rotation = new Quaternion(0,0,-directionModifier,1);
		rapierScript.setArmed(true);
		if (directionModifier > 0)
		{
			rb.AddForce(transform.up*60f,ForceMode.VelocityChange);
		}
		else 
		{
			rb.AddForce(transform.up*60f,ForceMode.VelocityChange);
		}
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
	}
}
