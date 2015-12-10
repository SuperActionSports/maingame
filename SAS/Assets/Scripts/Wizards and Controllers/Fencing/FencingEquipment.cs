using UnityEngine;
using System.Collections;

public class FencingEquipment : MonoBehaviour {

	
	bool Owned;
	public GameObject owner;
	public bool armed;
	bool thrown;
	CapsuleCollider attackCollider;
	MeshCollider rigidCollider;
	SphereCollider pickUpCollider;
	public float speed; 
	private Rigidbody rb; 
	public float timeTilGravity;
	private float spawnTime;
	private RapierScript rapierScript;
	private float normalThrowForce;
	private float runThrowForce;
	private float throwTime;
	
	// Use this for initialization
	void Start () {
		pickUpCollider = GetComponent<SphereCollider>();
		attackCollider = GetComponent<CapsuleCollider>();
		rigidCollider  = GetComponent<MeshCollider>();
		pickUpCollider.enabled = false;
		attackCollider.enabled = false;
		rigidCollider.enabled = false;		
		
		rb = GetComponent<Rigidbody>();
		SetRigidbodyForEquip();
		
		normalThrowForce = 20;
		runThrowForce = 30;
		timeTilGravity = 0.3f;
		throwTime = Mathf.Infinity;
		thrown = false;
		
		armed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= throwTime + timeTilGravity)
		{
			Debug.Log("Throw time: " + throwTime + " Time: " + Time.time + " time til gravity: " + timeTilGravity);
			rb.useGravity = true;
			throwTime = Mathf.Infinity;
		}
	}
		
	public void SetRigidbodyForEquip()
	{
		rigidCollider.enabled = false;
		//rb.detectCollisions = true;
		rb.isKinematic = true;	
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezeAll;
		thrown = false;
	}
	
	public void SetRigidbodyForThrow()
	{
		thrown = true;
		rigidCollider.enabled = true;
		rb.detectCollisions = true;
		rb.isKinematic = false;
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezePositionZ;
	}
	
	public void SetRigidbodyForDrop()
	{
		thrown = true;
		rigidCollider.enabled = true;
		rb.detectCollisions = true;
		rb.isKinematic = false;
		rb.useGravity = true;
		rb.constraints = RigidbodyConstraints.FreezePositionZ;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			FencingPlayerController otherPlayerScript = other.GetComponent<FencingPlayerController>();
			if (other.gameObject != owner)
			{
				if (armed)
				{
					if (other.GetComponent<FencingPlayerController>().GetHit())
					{
						owner.GetComponent<FencingPlayerController>().AddKill();
					}
					if (thrown)
					{
						Deflect();
						thrown = false;
					}
				}
				else if (owner == null)// Not armed, but colliding with a non-owner player, so it's a pick-up
				{
					owner = otherPlayerScript.PickUp(this.gameObject);
					if (owner != null)
					{
						ResetColor(otherPlayerScript.color);
						pickUpCollider.enabled = false;
						transform.localPosition = new Vector3(0.5f,0,0);
						transform.localEulerAngles = new Vector3(0,0,270);
						SetRigidbodyForEquip();
					}
				}
			}
		}
		else if (thrown && (other.gameObject.CompareTag("Stage") || other.gameObject.CompareTag("Equipment"))) //Rapier or stage. Does not handle demons.
		{
			Deflect();
		}
	}
	
	public void ResetColor(Color c)
	{
		GetComponent<Renderer>().material.color = c;
		GetComponent<TrailRenderer>().material.color = c;
	}
	
	public void StartAttack()
	{
		Debug.Log("Start attack");
		attackCollider.enabled = true;
		armed = true;
	}
	
	public void EndAttack()
	{
		Debug.Log("End attack");
		attackCollider.enabled = false;
		armed = false;
	}
	
	public void Deflect()
	{
		if (owner != null) 
		{
			owner.GetComponent<FencingPlayerController>().Disarm();
		}
		transform.parent = null;
		owner = null;
		armed = false;
		ResetColor(Color.black);	
		EndAttack();
		SetRigidbodyForDrop();
		pickUpCollider.enabled = true;
	}
	
	public void Throw(bool runThrow, float directionModifier)
	{
		thrown = true;
		transform.parent = null;
		transform.eulerAngles = new Vector3(0,0,directionModifier > 0 ? 270 : 90);
		Vector3 force = (runThrow ? transform.up * runThrowForce : transform.up * normalThrowForce);
		SetRigidbodyForThrow();
		rb.constraints = RigidbodyConstraints.FreezePositionZ;
		throwTime = Time.time;
		rb.AddForce(force,ForceMode.VelocityChange);
		StartAttack();
	}
	
}
