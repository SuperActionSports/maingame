﻿using UnityEngine;
using System.Collections;
using InControl;

public class HockeyPlayerController : MonoBehaviour, IPlayerController {

	// Color variables
	public Color color;

	//Keyboard Keybinding Stuff
	public KeyCode left;
	public KeyCode right;
	public KeyCode up;
	public KeyCode down;
	public KeyCode attack;
	public KeyCode debugKill;
	public KeyCode lookLeft;
	public KeyCode lookRight;
	public KeyCode lookUp;
	public KeyCode lookDown;
	public InputDevice device {get; set;}
	
	// Generla Game Player variables
	public Vector3 respawnPoint;
	public float respawnTime;
	private float timeOfDeath;
	public bool alive;
	public bool Alive()
	{
		return alive;
	}
	public bool movementAllowed;
	public void MovementAllowed(bool allow)
	{
		movementAllowed = allow;
	}
	
	
	private Rigidbody rb;
	public float xDirection;
	public float zDirection;
	public float xLookDirection;
	public float zLookDirection;
	public float walkSpeed;

	// Hockey Game Player variables
	public float maxSpeed;
	public float friction;
	
	// Game Objects and Components
	public GameObject[] respawnPoints;
	public GameObject equipmentHand;
	public CapsuleCollider equipmentCollider;
	public OverheadCameraController cam;
	private AudioSource[] sound;
	private Animator anim;
	public float SwingForceMagnitude = 10.0f;
	
	private PaintSplatterProjector paint;
	public HockeyWizard wizard;
	public HockeyStatsCard stats;
	public HockeyStatsCard Stats {
		get
		{
			return stats;
		}
		set
		{
			stats = value;
		}
	}

	public GameObject puck;
	private PuckMovement puckMovementScript;

	//debug
	public Vector3 vel;

	void Start () {
		// Get Components and Game Objects
		puck = GameObject.FindGameObjectWithTag ("Ball");
		puckMovementScript = puck.GetComponent<PuckMovement> ();

		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator> ();
		respawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
		equipmentCollider = equipmentHand.GetComponentInChildren<CapsuleCollider> (); // 0 returns collider on THIS object
		equipmentCollider.enabled = false;
		
        if (respawnPoints.Length == 0)
        {
            Debug.Log("There aren't any respawn points, you catastrophic dingus.");
        }

		SetColorToParent[] renderers = GetComponentsInChildren<SetColorToParent>();
		foreach (SetColorToParent rend in renderers) {
			rend.ResetColor(color);
		}
		GetComponent<Renderer>().material.color = color;

		// Set up general player variables
		alive = true;
		anim.SetBool("Alive", true);
		ResetRigidBodyConstraints();
		walkSpeed = 3.5f;
		maxSpeed = 9;
//		stats.ResetStats ();
		Debug.Log(stats);
		paint = GetComponent<PaintSplatterProjector>();
		paint.Initialize (color);
		friction = 0.95f;
		sound = GetComponents<AudioSource>();
    }
	public void InitializeStatCard()
	{
		stats = new HockeyStatsCard ();
		stats.ResetStats ();
	}

	void Update () {

        if (alive && movementAllowed)
        {
			// Move player in x and z directions 
			xDirection = 0;
			zDirection = 0;
			float xVel = GetXVelocity();
			float zVel = GetZVelocity();
			if (Time.timeScale > 0) { rb.AddForce ((walkSpeed/Time.deltaTime)*(new Vector3(xVel, 0, zVel)));}

			// Look player in x and z directions using second stick
//			xLookDirection = 0;
//			zLookDirection = 0;
//			float xLookVel = GetXLook();
//			float zLookVel = GetZLook();f
			if (Vector2.SqrMagnitude(device.RightStick.Value) > 0.667) { transform.eulerAngles = new Vector3(0,device.RightStick.Angle-90,0); }
			rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x,-maxSpeed,maxSpeed),0,Mathf.Clamp(rb.velocity.z,-maxSpeed,maxSpeed));
			rb.velocity *= friction;
			vel = rb.velocity;

			// Check if Attacking
			GetAttacking();
			//TODO: Port from golf player minus putting: CheckAnimStateForAttacking();
		}
		
		else if (timeOfDeath + respawnTime < Time.time)
		{
			Respawn();
		}

		// Update constantly updated variables as needed
		GetRespawn();
	}

	public void ResetRigidBodyConstraints() 
	{
		rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
		transform.rotation = Quaternion.identity;
	}
	
	public float TotalScore()
	{
		return stats.TotalScore();
	}

	private void GetAttacking()
	{
		if (Input.GetKeyDown (attack) || (device != null && (device.LeftTrigger || device.RightTrigger))) {
			Attack ();
		}
	}
	
	private void Attack()
	{
		anim.SetBool("Attack", true);
		stats.AddAttemptedAttack ();
		sound[1].Play();
	}
	
	private void StartAttacking()
	{
		Debug.Log ("start attack");
		equipmentCollider.enabled = true;
	}
	
	private void StopAttack()
	{
		Debug.Log ("stop attack");
		equipmentCollider.enabled = false;
		anim.SetBool("Attack", false);
	}
	
	private void GetRespawn()
	{
		if (Input.GetKeyDown(debugKill))
		{
			if (alive)
			MakeDead();
			else
			Respawn();
		} 
	}
	
	private void MakeDead()
	{
		wizard.SmallEvent();
		timeOfDeath = Time.time;
		alive = false;
        //rb.constraints = RigidbodyConstraints.None;
		anim.SetBool ("Alive", false);
		stats.EndLifeTime ();
		stats.AddDeath ();
		sound[0].Play();
    }
	
	public void Kill()
	{
		Debug.Log("Kill, no direction");
		rb.AddForce(40, 25, 0, ForceMode.VelocityChange);
        MakeDead();
    }

    public void Kill(Vector3 direction)
	{
		Vector3 rbForce = new Vector3(-direction.x,direction.y,-direction.z);
		rbForce *= 10;
		rbForce = CorrectRBForce(rbForce);
		Debug.Log("RbForce: " + rbForce);	
		rb.AddForce(rbForce, ForceMode.VelocityChange);
		paint.Splatter (transform.position, direction);
        MakeDead();
    }
    
    private Vector3 CorrectRBForce(Vector3 f)
    {
    	if (f.x < 0)
    	{
			f.x = Mathf.Clamp(f.x,-50,-10);
    	}
    	else
    	{
			f.x = Mathf.Clamp(f.x,10,50);
    	}
    	if (f.z < 0)
    	{
			f.z = Mathf.Clamp(f.z,-50,-10);
    	}
    	else
    	{
			f.z = Mathf.Clamp(f.z,10,50);
    	}
    	return f;
    }
	
	public void GoalScored()
	{
		stats.AddSuccesfulGoal();
	}

    public void Respawn()
    {
		alive = true;
		anim.SetBool ("Attack", false);
		anim.SetBool ("Alive", true);
        transform.position = respawnPoint;
		ResetRigidBodyConstraints();
		rb.velocity = new Vector3(0, 0, 0);
        transform.eulerAngles = new Vector3(0,0,0);
		stats.StartLifeTime ();
		wizard.FindPlayers();
    }

	private float GetXVelocity() {
		return device == null ? GetKeyboardXInput(): GetControllerXInput();
	}
	
	private float GetZVelocity() {
		return device == null ? GetKeyboardZInput(): GetControllerZInput();
	}
	private float GetControllerXInput() {
		return walkSpeed * device.Direction.X * Time.deltaTime;
	}
	private float GetControllerZInput() {
		return walkSpeed * device.Direction.Y * Time.deltaTime;
	}
	private float GetKeyboardXInput() {
		if (Input.GetKey(left))
		{
			xDirection = -1;
		}
		if (Input.GetKey(right))
		{
			xDirection = 1;
		}
		return walkSpeed * xDirection * Time.deltaTime;
	}
	private float GetKeyboardZInput()
	{
		if (Input.GetKey(up))
		{
			zDirection = 1;
		}
		if (Input.GetKey(down))
		{
			zDirection = -1;
		}
		return walkSpeed * zDirection * Time.deltaTime;
	}
	private float GetXLook() {
		return device == null ? GetKeyboardXLookInput(): GetControllerXLookInput();
	}
	
	private float GetZLook() {
		return device == null ? GetKeyboardZLookInput(): GetControllerZLookInput();
	}
	
	private float GetControllerXLookInput() {
		// TODO: Control input for second stick
		return 0;
	}
	
	private float GetControllerZLookInput() {
		// TODO: controller input for second stick
		return 0;
	}
	
	private float GetKeyboardXLookInput() {
		if (Input.GetKey(lookLeft))
		{
			xLookDirection = -1;
		}
		if (Input.GetKey(lookRight))
		{
			xLookDirection = 1;
		}
		return walkSpeed * xLookDirection * Time.deltaTime;
	}
	
	private float GetKeyboardZLookInput() {
		if (Input.GetKey(lookDown))
		{
			zLookDirection = -1;
		}
		if (Input.GetKey(lookUp))
		{
			zLookDirection = 1;
		}
		return walkSpeed * zLookDirection * Time.deltaTime;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Ball"))
		{
			Debug.Log ("inPlay: " + puckMovementScript.inPlay);
			other.GetComponent<PuckMovement>().HitPuck(this);
			if(puckMovementScript.inPlay)
			{

				stats.AddPuckPossession();
			}
		}
	}

}
