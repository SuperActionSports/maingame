using UnityEngine;
using System.Collections;

public class FencingEquipmentScript : MonoBehaviour {

	/*
	bool Owned;
	GameObject Owner
	bool armed;
	bool thrown;
	CapsuleCollider attackCollider
	MeshCollider RigidCollider
	SphereCollider PickUpCollider
	Rigidbody rb;
	public float speed; 
	private Rigidbody rb; 
	public float timeTilGravity;
	private float spawnTime;
	public float directionModifier;
	private RapierScript rapierScript;
	private float normalThrowForce;
	private float runThrowForce;	
	*/

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/*
	void OnCollisionEnter(collider other)
	{
		if (other.gameObject.CompareTag("Player")
		{
			otherPlayerScript = other.GetComponent<FencingPlayerController>();
			if (other.GameObject != owner)
			{
				if (armed)
				{
					if (other.GetComponent<FencingPlayerController>().GetHit())
					{
						owner.otherPlayerScript.AddKill();
					}
					Deflect();
				}
				else // Not dangerous, but colliding with a non-owner player, so it's a pick-up
				{
					owner = otherPlayerScript.PickUp(other.gameObject);
					color = otherPlayerScript.color;
					ResetColor();
					pickUpCollider.enabled = false;
				}
			}
		}
		else //Rapier or stage or demons
		{
			Deflect();
			pickUpCollider.enabled = true;
		}
	}
	
		public void ResetColor()
	{
		GetComponent<Renderer>().material.color = color;
		GetComponent<TrailRenderer>().material.color = color;
	}
	
	public void StartAttack()
	{
		attackCollider.enabled = true;
		dangerous = true;
	}
	
	public void EndAttack()
	{
		attackCollider.enabled = false;
		dangerous = false;
	}
	
	public void Deflect()
	{
	
		dangerous = false;
		color = Color.black;
		ResetColor();	
	}
	
	public void Throw()
	{
		Vector3 force = runThrow ? transform.up * runThrowForce : transform.up * normalThrowForce;
	}
	*/
}
