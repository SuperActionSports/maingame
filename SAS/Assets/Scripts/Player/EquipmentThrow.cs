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
	// Use this for initialization
	void Start () {
		Debug.Log(directionModifier);
		GameObject tip = transform.FindChild("Tip").gameObject;
		untouched = true;
		tip.SetActive(true);
		tip.GetComponent<Renderer>().material.color = c;
		transform.rotation = new Quaternion(0,0,-directionModifier,directionModifier);
		GetComponent<MeshCollider>().enabled = true;
		rb = gameObject.AddComponent<Rigidbody>();
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezePositionZ;
		rb.mass = 3;
		rb.AddForce(transform.up * 60f*directionModifier,ForceMode.VelocityChange);
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
		}
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
