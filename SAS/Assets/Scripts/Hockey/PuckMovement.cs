using UnityEngine;
using System.Collections;
using InControl;

public class PuckMovement : MonoBehaviour {
	
	public KeyCode debugKill;
	public Color c1;
	private CapsuleCollider puckCollider;
	private Rigidbody rb;
	public bool inplay;
	public GameObject respawnPoint;
	public InputDevice device {get; set;}
	//public float momentumX;
	//public float momentumZ;
	//[Range(1,200)]
	//public float maxSpeed;
	//[Range(0.7f,1.0f)]
	//public float friction;
	//public float floatAbove;
	//[Range(1,20)]
	//public float speedMagnitude;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		//friction = .97f;
		//maxSpeed = 20;
		//speedMagnitude = 15;
		inplay = true;
		respawnPoint = GameObject.Find("Puck RespawnPoint");

		if (respawnPoint == null)
		{
			Debug.Log("There aren't any respawn points, you catastrophic dingus.");
		}
	}
	
	// Update is called once per frame
	void Update () {
		GetRespawn();
		if (transform.position.y > 2.26) {
			rb.constraints = RigidbodyConstraints.None;
			rb.useGravity = true;
		} else {
			rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
			rb.useGravity = false;
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (inplay == true) {
			if (col.gameObject.name == "Inner West Net" || col.gameObject.name == "Inner East Net") {
				Debug.Log ("Goal Scored");
				inplay = false;
				Respawn();
			}
			if (col.gameObject.tag == "Player")
			{
				Debug.Log ("player hit puck");
				Rigidbody playerRB = col.gameObject.GetComponent<Rigidbody>();
				rb.AddForce (playerRB.velocity);
			}
		}
	}

	private void GetRespawn()
	{
		if (Input.GetKeyDown(debugKill) || (device!= null && device.Command.WasPressed))
		{
			if (inplay)
				MakeDead();
			else
				Respawn();
		} 
	}

	private void MakeDead()
	{
		inplay = false;
		rb.constraints = RigidbodyConstraints.None;
	}

	public void Respawn()
	{
		inplay = true;
		rb.velocity = new Vector3(0, 0, 0);
		transform.position = respawnPoint.transform.position;
	}

	private void ResetRigidBodyConstraints() 
	{
		rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
		transform.rotation = Quaternion.identity;
	}
}
