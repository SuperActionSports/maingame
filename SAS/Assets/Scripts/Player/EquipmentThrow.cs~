using UnityEngine;
using System.Collections;

public class EquipmentThrow : MonoBehaviour {
	
	public float x,y,z,w;
	public Color c;
	public float speed; 
	private Rigidbody rb; 
	public float timeTilGravity;
	private float spawnTime;
	public bool untouched;
	public float directionModifier;
	private MeshCollider frame;
	private RapierOwnership owner;
	// Use this for initialization
	void Start () {
		Debug.Log(directionModifier);
		//GameObject tip = transform.FindChild("Tip").gameObject;
		untouched = true;
		//tip.SetActive(true);
		//tip.GetComponent<Renderer>().material.color = c;
		frame = GetComponent<MeshCollider>();
		owner = GetComponent<RapierOwnership>();
		owner.hasHit = false;
		GetComponent<EquipmentScript>().setArmed(true);
		frame.enabled = true;
		z = -directionModifier;
		w = directionModifier;
		transform.rotation = new Quaternion(0,0,-directionModifier,1);
		GetComponent<MeshCollider>().enabled = true;
		rb = gameObject.AddComponent<Rigidbody>();
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezePositionZ;
		rb.mass = 3;
		if (directionModifier > 0)
		{
			rb.AddForce(transform.up*60f,ForceMode.VelocityChange);
		}
		else 
		{
			rb.AddForce(transform.up*60f,ForceMode.VelocityChange);
		}
		rb.AddTorque(0,0,-1000f);
		speed = 40;
		spawnTime = Time.time;
		timeTilGravity = 0.4f;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(Time.time + " >= " + spawnTime + " + " + timeTilGravity);
		if (Time.time >= spawnTime + timeTilGravity)
		{
			rb.useGravity = true;
			GetComponent<RapierOwnership>().resetOwnership();
			
		}
	}
}
