using UnityEngine;
using System.Collections;
using InControl;

public class PuckMovement : MonoBehaviour {


	public KeyCode left;
	public KeyCode right;
	public KeyCode up;
	public KeyCode down;
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
		if (inplay)
		{
			magSpeedX = 0;
			magSpeedZ = 0;
			
			
			// Move Character
			float xVel = GetXVelocity();
			float zVel = GetZVelocity();
			momentumX = (momentumX + xVel)*friction;
			momentumZ = (momentumZ + zVel)*friction;
			if(momentumX > maxSpeed) {momentumX = maxSpeed;}
			if(momentumX < 0) {if (momentumX < (-1)*maxSpeed){momentumX = (-1)*maxSpeed;}}
			if(momentumZ > maxSpeed) {momentumZ = maxSpeed;}
			if(momentumZ < 0) {if (momentumZ < (-1)*maxSpeed){momentumZ = (-1)*maxSpeed;}}
			Vector3 newPosition = new Vector3(momentumX,0,momentumZ);
			if (newPosition != new Vector3(0,0,0)) { transform.rotation = Quaternion.LookRotation(-newPosition); }
		}	

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

	private float GetKeyboardZInput()
	{
		if (Input.GetKey(up))
		{
			magSpeedZ = 1;
		}
		if (Input.GetKey(down))
		{
			magSpeedZ = -1;
		}
		return speedMagnitude * magSpeedZ * Time.deltaTime;
	}
	
	private float GetKeyboardXInput()
	{
		if (Input.GetKey(left))
		{
			magSpeedX = -1;
		}
		if (Input.GetKey(right))
		{
			magSpeedX = 1;
		}
		return speedMagnitude * magSpeedX * Time.deltaTime;
	}
	private float GetControllerZInput()
	{
		return speedMagnitude * device.Direction.Y * Time.deltaTime;
	}
	private float GetControllerXInput()
	{
		return speedMagnitude * device.Direction.X * Time.deltaTime;
	}
	private float GetXVelocity()
	{
		return device == null ? GetKeyboardXInput(): GetControllerXInput();
	}
	
	private float GetZVelocity()
	{
		return device == null ? GetKeyboardZInput(): GetControllerZInput();
	}
}
