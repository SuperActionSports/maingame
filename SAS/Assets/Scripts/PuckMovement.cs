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
	public float speedMagnitude;
	public InputDevice device {get; set;}
	public float magSpeedX;
	public float magSpeedZ;
	public float momentumX;
	public float momentumZ;
	public float maxSpeed;
	public float friction;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		speedMagnitude = 10f;
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
	}
	
	public void GoalScored () {
		if (inplay == true) {

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
}
