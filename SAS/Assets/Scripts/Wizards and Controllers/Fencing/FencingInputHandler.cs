using UnityEngine;
using System.Collections;
using InControl;

public class FencingInputHandler : MonoBehaviour {

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
	public FencingPlayerController control;  // Layla
	
	public InputDevice device; // Layla
	public bool deviceActive; // Deprecated after Layla is implemented
	public bool facingRight;
	public bool canJump;
	private float doubleTapCooler;
	private float doubleTapDelay;
	private int doubleTapCount;
	private float previousXVel;
	private float grandPreviousXVel;
	
	[Range(1,30)]
	public float speedMagnitude;
	public float sprintSpeedMod;
	[Range(25,46)]
	public float jumpForce;
	
	private float magSpeedX;

	void Start () {
		canJump = true;
		facingRight = true;
		deviceActive = device == null ? false : true;
		speedMagnitude = 10;
		jumpForce = 16;
		sprintSpeedMod = 1.1f;
		doubleTapDelay = 0.5f;
		previousXVel = 0;
		player = gameObject;
		rb = GetComponent<Rigidbody>();
		control = GetComponent<FencingPlayerController>();
	}

	void Move(float xVel) 
	{
		if ((transform.position.x > 7.3f && xVel > 0) || (transform.position.x < -7.3f && xVel < 0))
		{
			xVel = 0;
		}
		transform.position += new Vector3(xVel * Time.deltaTime,0,0);
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
		//control.SetRotation(facingRight);
	}
	
	public void HandleInput()
	{
		magSpeedX = 0;
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
		if (Input.GetKeyDown(counter) || (deviceActive && device.Action3.WasPressed))
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
				ThrowRapier();
			}
		}
		else if (Input.GetKeyDown(attack))
		{
				Attack();
		}
		else if (Input.GetKeyDown(throwEquip))
		{
			ThrowRapier();
		}
	}
	
	private void Attack()
	{
		control.Attack();
	}

	private void ThrowRapier()
	{
		control.ThrowRapier();
	}
	
	private float GetXVelocity()
	{
		//float vel = !deviceActive ? GetKeyboardXInput(): GetControllerXInput();
		//return xIsDoubleTap(vel) ? vel * speedMagnitude: vel;	
		float vel = 0;
		vel = !deviceActive ? GetKeyboardXInput(): GetControllerXInput(); 
		control.SetRun(Mathf.Abs(vel));
		return vel;
	}
	
	private bool xIsDoubleTap(float xVel)
	{
		bool doubleTap = false;
		if (doubleTapCooler > 0 && doubleTapCount > 1)
		{
			doubleTap = true;
		}
		else if (Mathf.Abs(previousXVel) < Mathf.Abs(xVel))
		{
			if (doubleTapCooler > 0)
			{
				if ((xVel < 0 && grandPreviousXVel < 0) || (xVel > 0 && grandPreviousXVel > 0))
				{
					doubleTapCount++;
				}
			}
			else
			{
				doubleTapCooler = doubleTapDelay;
			}
			doubleTapCooler -= Time.deltaTime;
		}
		else
		{
			doubleTapCooler = 0;
			doubleTapCount = 0;
		}
		
		if (doubleTapCooler > 0)
		{
			if (Mathf.Abs(xVel) < Mathf.Abs(previousXVel))
			{
				previousXVel = xVel;
			}
		}
		else 
		{
			previousXVel = xVel;		
			grandPreviousXVel = previousXVel;
		}

		return doubleTap;
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
			if (groundHit.collider.CompareTag("Stage") || groundHit.collider.CompareTag("Equipment"))
			{
				if (groundHit.collider.CompareTag("Stage"))
				{
					rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
					control.Stats.AddJump();
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
		if (canJump) {
				rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
				control.Stats.AddJump();
				canJump = false;
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
		return speedMagnitude * magSpeedX;
	}
	
	private float GetControllerXInput()
	{
		if ((device.Direction.X > 0 &&  control.CanMoveRight) || (device.Direction.X < 0 && control.CanMoveLeft))
		{
			return speedMagnitude * device.Direction.X;
		}
		else
		{
			return 0;
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.CompareTag ("Stage")) { 
			canJump = true;
		}
	}
	
}
