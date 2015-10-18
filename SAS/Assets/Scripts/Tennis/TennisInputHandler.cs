using UnityEngine;
using System.Collections;
using InControl;
public class TennisInputHandler : MonoBehaviour {

	//Keyboard Keybinding Stuff
	public KeyCode left;
	public KeyCode right;
	public KeyCode up;
	public KeyCode down;
	public KeyCode swing;
	public KeyCode attack;
	public KeyCode debugKill;
	public KeyCode jump;
	public GameObject tennisBall;
	
	public InputDevice device;
	
	private Rigidbody rb;
	public RaycastHit groundHit;
	public float magSpeedX;
	public float magSpeedZ;
	public Vector3 speed;
	public float speedMagnitude;
	public float jumpForce;
	
	public TennisController control;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		ResetRigidBodyConstraints();
		speedMagnitude = 10;
		jumpForce = 25;
	}
	
	// Update is called once per frame
	public void CheckInput () {
		magSpeedX = 0;
		magSpeedZ = 0;
		// Move Character
		float xVel = GetXVelocity();
		float zVel = GetZVelocity();
		Vector3 newPosition = new Vector3(xVel,0,zVel);
		transform.position += newPosition;
		GetYVelocity();
		//Debug.Log("Applied: " + rb.velocity);
		
		// If input has been given change to face new input direction
		if (newPosition != new Vector3(0,0,0)) { transform.rotation = Quaternion.LookRotation(newPosition); }
		CheckServe();
		GetSwinging();
		GetAttacking();
		
	}
	
	private void CheckServe()
	{
		if (Input.GetKeyDown (KeyCode.Space) || (device != null && device.RightBumper.WasPressed)) {
			control.Serve();
		}
	}
	
	private void ResetRigidBodyConstraints() 
	{
		rb.constraints = RigidbodyConstraints.FreezeRotation;
		transform.rotation = Quaternion.identity;
	}
	
	private void GetRespawn()
	{
		if (Input.GetKeyDown(debugKill) || (device!= null && device.Command.WasPressed))
		{
			if (control.alive)
			{
				ResetRigidBodyConstraints();
				control.MakeDead();
			}
			else
			{
				control.Respawn();
			}
		}
	}
	
	private void GetSwinging()
	{
		if (Input.GetKeyDown (swing) || (device != null && device.RightTrigger.WasPressed)) 
		{
			control.Swing();
		}
	}
	
	private void GetAttacking()
	{
		if (Input.GetKeyDown (attack) || (device != null && device.LeftTrigger.WasPressed))
		{
			control.Attack ();
		}
		else if (Input.GetKeyDown (attack))
		{
			control.Attack();
		}
	}
	
	private float GetXVelocity()
	{
		return device == null ? GetKeyboardXInput(): GetControllerXInput();
	}
	
	private float GetZVelocity()
	{
		return device == null ? GetKeyboardZInput(): GetControllerZInput();
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
	
	private float GetControllerZInput()
	{
		return speedMagnitude * device.Direction.Y * Time.deltaTime;
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
	
	private float GetControllerXInput()
	{
		return speedMagnitude * device.Direction.X * Time.deltaTime;
	}	
	
	private void GetYVelocity()
	{
		
		if (device == null )
			GetKeyboardYInput();
		else 
			GetControllerYInput();
	}
	
	private void GetKeyboardYInput()
	{
		
		if (Input.GetKeyDown(jump))
		{
			//anim.SetTrigger("Jump");
			if (Physics.Raycast(transform.position, Vector3.down, out groundHit, 2f))
			{
				if (groundHit.collider.CompareTag("Turf"))
				{
					rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
				}
			}
		}
		
	}
	
	private void GetControllerYInput()
	{
		if (device.Action1)
		{
			if (Physics.Raycast(transform.position, Vector3.down, out groundHit, 2f))
			{
				if (groundHit.collider.CompareTag("Turf"))
				{
					rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
				}
			}
		}
	}
	
	public Vector2 GetStickForSwing()
	{
		return new Vector2 (device.Direction.X, device.Direction.Y);
	}
}
