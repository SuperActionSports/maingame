using UnityEngine;
using System.Collections;
using InControl;

public class PlayerInputHandlerMatt : MonoBehaviour {

	// Use this for initialization
	public KeyCode left;
	public KeyCode right;
	public KeyCode up;
	public KeyCode down;
	public KeyCode attack;
	public KeyCode debugKill;
	public KeyCode throwEquip;
	public KeyCode counter;
	
	public RaycastHit groundHit;
	
	public GameObject player;
	public Rigidbody rb;
	public PlayerControllerMatt control;
	
	public InputDevice device {get; set;}
	private bool deviceActive;
	public bool facingRight;
	
	[Range(1,30)]
	public float speedMagnitude;
	
	private float magSpeedX;
	void Start () {
		deviceActive = device == null ? false : true;
		speedMagnitude = 10;
	}
	
	void Update () {
	
	}
	
	void Move(float xVel) 
	{
		transform.position += new Vector3(xVel,0,0);
		if (xVel < 0 && transform.eulerAngles.y < 180)
		{
			transform.rotation = new Quaternion(transform.rotation.x, 180f, transform.rotation.z, transform.rotation.w);
			facingRight = false;
			
		}
		else if (xVel > 0 && transform.eulerAngles.y > 0)
		{
			transform.rotation = new Quaternion(transform.rotation.x, 0f, transform.rotation.z, transform.rotation.w);
			facingRight = true;
		}
	}
	
	public void HandleInput()
	{
		Move(GetXVelocity());
		GetYVelocity();
		GetCounter();
		GetRespawn();
		GetAttacking();
	}
	
	private void GetRespawn()
	{
		//Handle the control button thing here
		//control.Respawn();
	}
	
	private void GetCounter()
	{
		if (control.armed && Input.GetKeyDown(counter))
		{
			control.Counter();
		}
	}
	
	private void GetAttacking()
	{
		if (device != null)
		{ 
			if (device.RightTrigger.WasPressed)
			{
				Attack ();
			}
			else if (device.LeftTrigger.WasPressed)
			{
				control.ThrowEquipment();
			}
		}
		else if (Input.GetKeyDown(attack))
		{
			control.Attack();
		}
		else if (Input.GetKeyDown(throwEquip))
		{
			control.ThrowEquipment();
		}
	}
	
	private void Attack()
	{
		control.Attack();
	}
	
	private float GetXVelocity()
	{
		return !deviceActive ? GetKeyboardXInput(): GetControllerXInput();
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
		if (Input.GetKeyDown(up))
		{
			//anim.SetTrigger("Jump");
			if (Physics.Raycast(transform.position, Vector3.down, out groundHit, 1.1f))
			{
				if (groundHit.collider.CompareTag("Stage"))
				{
					rb.velocity = new Vector3(rb.velocity.x, speedMagnitude * 4f, rb.velocity.z);
				}
			}
		}
		if (Input.GetKey(down))
		{
			rb.velocity -= new Vector3(0, speedMagnitude * Time.deltaTime, 0);
		}
		
	}
	
	private void GetControllerYInput()
	{
		if (device.Action1 || device.Direction.Y > 0.9)
		{
			if (Physics.Raycast(transform.position, Vector3.down, out groundHit, 1.1f))
			{
				if (groundHit.collider.CompareTag("Stage"))
				{
					rb.velocity = new Vector3(rb.velocity.x, speedMagnitude * 4f, rb.velocity.z);
				}
			}
		}
		if (device.Direction.Y < 0)
		{
			rb.velocity -= new Vector3(0, speedMagnitude * Time.deltaTime, 0);
		}
		
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
}
