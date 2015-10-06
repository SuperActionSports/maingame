﻿using UnityEngine;
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
	
	public InputDevice device;
	public bool deviceActive;
	public bool facingRight;
	
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
		deviceActive = device == null ? false : true;
		speedMagnitude = 20;
		jumpForce = 32;
		sprintSpeedMod = 1.1f;
		doubleTapDelay = 0.5f;
		previousXVel = 0;
		player = gameObject;
		rb = GetComponent<Rigidbody>();
		control = GetComponent<PlayerControllerMatt>();
	}
	
	void Update () {
	
	}
	
	void Move(float xVel) 
	{
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
		control.SetRotation(facingRight);
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
		if (control.armed && Input.GetKeyDown(counter) || (deviceActive && device.Action3.WasPressed))
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
				control.ThrowRapier();
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
		//float vel = !deviceActive ? GetKeyboardXInput(): GetControllerXInput();
		//return xIsDoubleTap(vel) ? vel * speedMagnitude: vel;	
		float vel;
		vel = !deviceActive ? GetKeyboardXInput(): GetControllerXInput();
		control.setRun(Mathf.Abs(vel));
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
			if (Physics.Raycast(transform.position, Vector3.down, out groundHit, 1.1f))
			{
				if (groundHit.collider.CompareTag("Stage"))
				{
					rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
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
					rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
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
		return speedMagnitude * magSpeedX;
	}
	
	private float GetControllerXInput()
	{
		return speedMagnitude * device.Direction.X;
	}
}
