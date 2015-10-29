﻿using UnityEngine;
using System.Collections;
using InControl;

public class BaseballPlayerController : MonoBehaviour, IPlayerController {

	public Color color;
	public int playerNumber;
		
	//Keyboard Keybinding Stuff
	public KeyCode left;
	public KeyCode right;
	public KeyCode up;
	public KeyCode down;
    public KeyCode attack;
    public KeyCode debugKill;
	
	public InputDevice device {get; set;}
	public bool hasDevice;	

	public bool alive;
	public bool Alive()
	{
		return alive;
	}
	public float respawnTime;
	private float timeOfDeath;
	public bool movementAllowed;
	public void MovementAllowed(bool allowed)
	{
		movementAllowed = allowed;
	}
	public bool MovementAllowed()
	{
		return movementAllowed;
	}
	
	private Rigidbody rb;
	public RaycastHit groundHit;
	public float magSpeedX;
	public float magSpeedY;
    public float debugYMod;
	public Vector3 speed;
    private bool doubleJumpAllowed;

    public Vector3 respawnPoint;
    public GameObject equipment;
    private CapsuleCollider equipmentCollider;
    public float impactMod;
	public BaseballWizard wizard;

    public BaseballCameraController cam;
	private PaintSplatter paint;
	private AudioSource sound;
    private Animator anim;

	[Range(1,20)]
	public float speedMagnitude;
	[Range(20,60)]
	public float jumpMagnitude;

	public BaseballStatsCard stats;
	// Use this for initialization
	void Start () {
	 	sound =  GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<BaseballCameraController>();
		rb = GetComponent<Rigidbody>();
        anim = GetComponent <Animator>();
        equipment = transform.FindChild("BatHand").gameObject;
        equipmentCollider = equipment.GetComponent<CapsuleCollider>();
		//rend.material.color = c;
		speedMagnitude = 12f;
		jumpMagnitude = 40;
		alive = true;
        anim.SetBool("Alive", true);
		ResetRigidBodyConstraints();
        doubleJumpAllowed = true;
		impactMod = 7.5f;
        GetComponent<Renderer>().material.color = color;
		paint = GetComponent<PaintSplatter>();
		stats.ResetStats ();
		//paint.color = color;
    }
    
	
	// Update is called once per frame
	void Update () {
		hasDevice = !(device == null);
        if (alive && movementAllowed)
        {
            magSpeedX = 0;
            magSpeedY = 0;
            
            float xVel = GetXVelocity();
			GetYVelocity();
			if (transform.position.y > 1.6f)
			{
				anim.SetBool("Jumping",true);
				stats.AddJump();
			}
			else 
			{
				anim.SetBool("Jumping",false);
			}
			transform.position = transform.position + new Vector3(xVel,0,0);
			if (xVel < 0)
			{
				anim.SetBool("running", true);
				transform.rotation = new Quaternion(transform.rotation.x, 180f, transform.rotation.z, transform.rotation.w);
            }
			else if (xVel > 0 && transform.rotation.y > 0)
            {
				anim.SetBool("running", true);
				transform.rotation = new Quaternion(transform.rotation.x, 0f, transform.rotation.z, transform.rotation.w);
            }
			else
			{
				anim.SetBool ("running", false) ;
			}
			GetAttacking();
			//CheckAnimStateForAttacking();
		}
		else if (Time.time >= timeOfDeath + respawnTime)
		{
			Respawn();
		}
		
		GetRespawn();
	}
	
	private void ResetRigidBodyConstraints() 
	{
		rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
		transform.rotation = Quaternion.identity;
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - 1.1f, transform.position.z));
	}
	
	private void GetRespawn()
	{
		if (Input.GetKeyDown(debugKill) || (device!= null && device.Command.WasPressed))
		{
			if (alive)
			MakeDead();
			else
			Respawn();
		}

	}

	private void MakeDead()
	{
		alive = false;
		//Need the normal of the local x axis of bat
       // paint.Paint(transform.position,paint.color);
        rb.constraints = RigidbodyConstraints.None;
        alive = false;
        anim.SetBool("Alive", false);
        cam.PlayShake(transform.position);
        timeOfDeath = Time.time;
		stats.EndLifeTime ();
		stats.AddDeath ();
    }
	
	private void CheckAnimStateForAttacking()
	{
		if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
		{
			equipmentCollider.enabled = true;
		}
		else if (equipmentCollider.enabled)
		{
			equipmentCollider.enabled = false;
		}
	}
	
	public void Kill()
	{
		//Magic Number
		rb.AddForce(40, 25, 0, ForceMode.VelocityChange);
        MakeDead();
    }

    public void Kill(Vector3 direction)
    {
		//Magic Number
		rb.AddForce(Vector3.Cross(new Vector3(impactMod,impactMod,impactMod), direction), ForceMode.VelocityChange);
        MakeDead();    
    }

    public void Respawn()
    {
        alive = true;
        ResetRigidBodyConstraints();
        rb.velocity = new Vector3(0, 0, 0);
        anim.SetBool("Alive", true);
        //Debug.Log("Length: " + respawnPoints.Length);
        transform.position = respawnPoint;
		stats.StartLifeTime ();
    }
	
	private void GetAttacking()
	{
		if (Input.GetKey(attack) || (device != null && (device.LeftTrigger.WasPressed || device.RightTrigger.WasPressed)))
		{
			Attack ();
		}
		else if (Input.GetKey(attack))
		{
			Attack();
		}
	}
	
	private void Attack()
    {
		AudioSource bat = GetComponent<AudioSource> ();
		if (!bat.isPlaying) {
			bat.Play ();
		}
		anim.SetTrigger("Attack");
		stats.AddAttemptedHit ();
    }

	public void StartAttack()
	{
		equipmentCollider.enabled = true;
	}

	public void StopAttack()
	{
		equipmentCollider.enabled = false;
	}
    
    private float GetXVelocity()
    {
    	return device == null ? GetKeyboardXInput(): GetControllerXInput();
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
			if (doubleJumpAllowed)
			{
				anim.SetBool("Jumping", false);
				rb.velocity = new Vector3(rb.velocity.x, jumpMagnitude, rb.velocity.z);
				doubleJumpAllowed = false;
			}
			else if (Physics.Raycast(transform.position, Vector3.down, out groundHit, 1.1f))
			{
				if (groundHit.collider.CompareTag("field"))
				{
					doubleJumpAllowed = true;
					anim.SetBool("Jumping",true);
					rb.velocity = new Vector3(rb.velocity.x, jumpMagnitude, rb.velocity.z);
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
		if (device.Action1.WasPressed || device.Direction.Y > 0.9)
		{
			if (doubleJumpAllowed)
			{
//				anim.SetBool("Jumping", false);
				rb.velocity = new Vector3(rb.velocity.x, jumpMagnitude, rb.velocity.z);
				doubleJumpAllowed = false;
			}
			else if (Physics.Raycast(transform.position, Vector3.down, out groundHit, 1.1f))
			{
				if (groundHit.collider.CompareTag("field"))
				{
					doubleJumpAllowed = true;
					anim.SetBool("Jumping",true);
					rb.velocity = new Vector3(rb.velocity.x, jumpMagnitude, rb.velocity.z);
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
			magSpeedX = -2;
		}
		if (Input.GetKey(right))
		{
			magSpeedX = 2;
		}
		return speedMagnitude * magSpeedX * Time.deltaTime;
	}
	
	private float GetControllerXInput()
	{
		return speedMagnitude * device.Direction.X * Time.deltaTime * 2;
	}
}
