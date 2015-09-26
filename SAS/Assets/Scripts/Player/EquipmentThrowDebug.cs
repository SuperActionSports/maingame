using UnityEngine;
using System.Collections;

public class EquipmentThrowDebug : MonoBehaviour {
	
	public float x,y,z,w;
	public Color c;
	public float speed; 
	private Rigidbody rb; 
	public float timeTilGravity;
	private float spawnTime;
	public bool untouched;
	public float directionModifier;
	// Use this for initialization
	void Start () {
		x = 0;
		y = 0;
		z = -1;
		w = 1;
		transform.rotation = new Quaternion(x,y,z,w);
		GetComponent<MeshCollider>().enabled = true;
		rb = gameObject.AddComponent<Rigidbody>();
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezePositionZ;
		rb.mass = 3;
		//rb.AddForce(transform.up * 60f*directionModifier,ForceMode.VelocityChange);
		speed = 40;
		spawnTime = Time.time;
		timeTilGravity = 0.4f;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.rotation = new Quaternion(x,y,z,w);
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Stage") || other.CompareTag("Player"))
		{
			untouched = false;
			GetComponent<Renderer>().material.color = Color.black;
		}
	}
}
