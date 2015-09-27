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
	private RapierOwnership owner;
	private bool thrown;
	// Use this for initialization
	void Start () {
		PickUp();
		frame = GetComponent<MeshCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		if (thrown && Time.time >= spawnTime + timeTilGravity)
		{
			rb.useGravity = true;
			GetComponent<RapierOwnership>().resetOwnership();
		}
	}
	
	private void ActivateRigidbody(bool useGravity)
	{
		rb = gameObject.AddComponent<Rigidbody>();
		rb.useGravity = useGravity;
		rb.constraints = RigidbodyConstraints.FreezePositionZ;
		rb.mass = 3;
	}
	
	public void Drop()
	{
		ActivateRigidbody(true);
		frame.enabled = true;
		owner.resetOwnership();
	}
	
	public void Throw()
	{
		transform.parent = null;
		ActivateRigidbody(false);
		untouched = true;
		Debug.Log("I am a rapier and I am owned by " + owner);
		owner.hasHit = false;
		frame.enabled = true;
		transform.rotation = new Quaternion(0,0,-directionModifier,1);
		owner.setArmed(true);
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
		//owner.resetOwnership();
	}	
	
	public void PickUp()
	{
		if (rb != null)
		{
			Destroy (rb);
		}
		owner = GetComponent<RapierOwnership>();
		thrown = false;
		timeTilGravity = 0.4f;
		spawnTime = Time.time;
		thrown = false;	
	}
}
