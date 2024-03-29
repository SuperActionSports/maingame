﻿using UnityEngine;
using System.Collections;
using InControl;

public class GolfPlayerController : MonoBehaviour, IPlayerController {

	// Color variables
	public Color color;
	private Renderer[] renderers;
	private bool colorChangeToUniform;
	private float colorLerpT;
	
	//Keyboard Keybinding Stuff
	public KeyCode left;
	public KeyCode right;
	public KeyCode up;
	public KeyCode down;
    public KeyCode attack;
    public KeyCode debugKill;

	// Golf
	public GolfBall ball;
	public float distanceToBall;
	public bool canHitBall;
	public bool putting;
	public bool swinging;
	public float swingStrength;
	public bool canRotateLeft;
	public bool canRotateRight;
	public bool rotatingLeft;

	public InputDevice device {get; set;}

	// Generla Game Player variables
	public bool alive;
	public bool Alive()
	{
		return alive;
	}
	private bool movementAllowed;
	public void MovementAllowed(bool allow)
	{
		movementAllowed = allow;
	}
	public float respawnTime;
	private float timeOfDeath;
	public Vector3 respawnPoint;
	
	private Rigidbody rb;
	public float xDirection;
	public float zDirection;
	public float walkSpeed;

	// Game Objects and Components
    public GameObject[] respawnPoints;
    public CapsuleCollider equipmentCollider;
    public OverheadCameraController cam;
	private AudioSource sound;
    private Animator anim;
    
    public GolfWizard wizard;
	public GolfStatsCard stats;
	public GolfStatsCard Stats {
		get
		{
			return stats;
		}
		set
		{
			stats = value;
		}
	}
	private PaintSplatterProjector paint;
	private Projector proj;

	void Start () {
		// Get Components and Game Objects
		respawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
		if (respawnPoints.Length == 0)
		{
			Debug.Log("There aren't any respawn points, you catastrophic dingus.");
		}
		sound =  GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<OverheadCameraController>();
		rb = GetComponent<Rigidbody>();
        anim = GetComponent <Animator>();
		equipmentCollider = GetComponentsInChildren<CapsuleCollider> ()[1]; // 0 returns collider on THIS object
        equipmentCollider.enabled = false;
		paint = GetComponent<PaintSplatterProjector> ();
		proj = GetComponentInChildren<Projector>();
		Debug.Log("My projector is " + proj);
		paint.Initialize (color);

		// Set up color variables
		GetComponent<Renderer>().material.color = color;
		SetColorToParent[] kids = GetComponentsInChildren<SetColorToParent>();
		Debug.Log("Kids length: " + kids.Length);
		foreach (SetColorToParent s in kids) {
			s.ResetColor(color);
		}
		colorChangeToUniform = false;
		colorLerpT = 0;

		// Set up general player variables
		alive = true;
        anim.SetBool("Alive", true);
		ResetRigidBodyConstraints();
		walkSpeed = 10f;

		// Set up Golf specific variables
		swinging = false;
		putting = false;
		canRotateLeft = true;
		canRotateRight = true;
		stats.ResetStats ();
    }
	public void InitializeStatCard()
	{
		stats = new GolfStatsCard ();
		stats.ResetStats ();
	}
    

	void Update () {
        if (alive && movementAllowed)
        {
			// Toggle aim arrow visibility
			if (putting) { proj.gameObject.SetActive(true); }
			else { proj.gameObject.SetActive(false); }
			// Reset velocity to 0
        	rb.velocity = new Vector3(0,0,0);

			// Update ball object and see if player is close enough to putt
			if (!putting) {
				GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
				float distance = Mathf.Infinity; 
				Vector3 position = transform.position; 

				foreach (GameObject obj in balls)  { 
					float currentDistance = Mathf.Sqrt(Mathf.Pow((obj.transform.position.x - position.x), 2) +
					                                   Mathf.Pow((obj.transform.position.z - position.z), 2));
					if (currentDistance < distance) { 
						ball = obj.GetComponent<GolfBall>(); 
						distance = currentDistance; 
					} 
				} 

				if (ball == null) {
					distanceToBall = 0;
					canHitBall = false;
				}
				else {
					distanceToBall = Mathf.Sqrt (Mathf.Pow ((ball.transform.position.x - transform.position.x), 2)
				                                   + Mathf.Pow ((ball.transform.position.z - transform.position.z), 2));
					canHitBall = (distanceToBall < 2);
				}
			}

			// Move player in x and z directions 
			xDirection = 0;
			zDirection = 0;
            float xVel = GetXVelocity();
			float zVel = GetZVelocity();
			Vector3 newPosition = new Vector3(xVel,0,zVel);
			if (!putting) {
				transform.position = transform.position + newPosition; 	
				// If input has been given change to face new input direction
				if (newPosition != new Vector3(0,0,0)) { 
					transform.rotation = Quaternion.LookRotation(-newPosition); 
					transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y + 90, transform.eulerAngles.z);
				}
			}
			else {
				// If currently aiming a putt allow the stick to rotate the character around the ball
				if (!swinging) 
				{ 
					float dir = walkSpeed*xVel-walkSpeed*zVel;
					if ((dir < 0 && canRotateLeft) || (dir > 0 && canRotateRight)) {
						rotatingLeft = (dir < 0);
						if (rotatingLeft) { canRotateRight = true; }
						else { canRotateLeft = true; }
						Transform sweetspot = transform.FindChild("Sweetspot");
						sweetspot.SetParent (null);
						transform.SetParent (sweetspot);
						sweetspot.RotateAround (ball.transform.position, Vector3.up, dir); 
						transform.SetParent (null);
						sweetspot.SetParent (this.transform);
					}
				}
			}
			// Check if player is attacking
			GetAttacking(putting, canHitBall);
			CheckAnimStateForAttacking();
		}

		// Update player color and respawn player if necessary
		
		else if (Time.time >= timeOfDeath + respawnTime)
		{
			proj.gameObject.SetActive(false);
			Respawn();
		}
		GetRespawn();
	}

	public void OnCollisionEnter(Collision col) {
		if (col.gameObject.CompareTag ("Wall")) {
			if (putting && !swinging) {
				if (rotatingLeft) { canRotateLeft = false; }
				else { canRotateRight = false; }
			}
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
        rb.constraints = RigidbodyConstraints.None;
        alive = false;
        anim.SetBool("Alive", false);
        device.Vibrate(10);
		timeOfDeath = Time.time;
		proj.gameObject.SetActive(false);
		stats.EndLifeTime ();
		stats.AddDeath ();
    }
	
	private void CheckAnimStateForAttacking()
	{
		// Check Anim State for Putting
		if (putting) { 
			anim.SetBool ("Putting", true);
			if (swinging) { 
				if (anim.GetBool ("Swing") != true) { anim.SetBool ("BackSwing", true); }
			}
		} else {
			anim.SetBool ("Putting", false);
		}
	}
	
	public void Kill()
	{
		//Magic Number
		rb.AddForce(40, 25, 0, ForceMode.VelocityChange);
		if (putting) {
			ball.beingHit = false;
		}
		putting = false;
		swinging = false;
        MakeDead();
    }

    public void Kill(Vector3 direction)
    {
		if (putting) {
			ball.beingHit = false;
		}
		putting = false;
		swinging = false;
		Vector3 rbForce = direction * 40;
		rbForce.x *=-1;
		rbForce.z *= -1;
		rb.AddForce (rbForce, ForceMode.VelocityChange);
		paint.Splatter (transform.position, rbForce);
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
	
	private bool AttackButtonsDown()
	{
		return device == null ? Input.GetKeyDown(attack) : (device.LeftTrigger.WasPressed || device.RightTrigger.WasPressed);
	}
	
	private bool AttackButtonsUp()
	{
		return device == null ? Input.GetKeyUp(attack) : (device.LeftTrigger.WasReleased || device.RightTrigger.WasReleased);
	}
	
	private bool AttackButtons()
	{
		return device == null ? Input.GetKey(attack) : (device.LeftTrigger || device.RightTrigger);
	}
	
	private void GetAttacking(bool putting, bool CanHitBall)
	{
		if (putting) {
			if (AttackButtonsDown() && !swinging) {
				BackSwing ();
			}
			else if (AttackButtons() && swinging) {
				BackSwing ();
			}
			else if (AttackButtonsUp() && swinging) {
				Swing ();
			}
		} 
		else {
			if (AttackButtonsDown()) {
				if (CanHitBall && !ball.beingHit) {
					StartPutting ();
				} else {
					Attack ();
				}
			}
		}
	}
	
	private void Attack()
    {
		anim.SetBool("Attack", true);
		stats.AddAttemptedAttack ();
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

	private void StartPutting() {
		putting = true;
		ball.beingHit = true;
		Transform sweetspot = transform.FindChild("Sweetspot");
		sweetspot.SetParent (null);
		transform.SetParent (sweetspot);
		sweetspot.position = ball.transform.position;
		transform.SetParent (null);
		sweetspot.SetParent (this.transform);
		stats.AddAttemptedPutt ();

	}

	private void BackSwing() {
		// Begin swinging when button is pressed
		if (!swinging) {
			swinging = true;
			swingStrength = 5f;
		}
		// Increase swing strength while button is held
		else {
			swingStrength += 0.5f;
		}
	}

	private void Swing() {
		anim.SetBool ("BackSwing", false);
		anim.SetBool ("Swing", true);
	}

	private void FinishSwing() {
		swinging = false;
		putting = false;
		anim.SetBool ("BackSwing", false);
		anim.SetBool ("Swing", false);
		Debug.Log ("I hit it this hard: " + swingStrength);
		ball.Putt (100f*swingStrength*transform.forward, this);
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
			zDirection = 1;
		}
		if (Input.GetKey(down))
		{
			zDirection = -1;
		}
		return walkSpeed * zDirection * Time.deltaTime;
	}
	
	private float GetControllerZInput()
	{
		return walkSpeed * device.Direction.Y * Time.deltaTime;
	}
	
	private float GetKeyboardXInput()
	{
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
	
	private float GetControllerXInput()
	{
		return walkSpeed * device.Direction.X * Time.deltaTime;
	}
	
    private void UpdateColor()
	{
		if (alive) {
			colorLerpT += Time.deltaTime;
			foreach (Renderer rend in renderers) {
				if (colorChangeToUniform && alive) {
					rend.material.color = Color.Lerp (new Color (0, 0, 0, 0), color, colorLerpT);
					if (colorLerpT >= 1) {
						colorChangeToUniform = false;
						colorLerpT = 0;
					}
				} else {
					rend.material.color = Color.Lerp (color, new Color (0, 0, 0, 0), colorLerpT);
					if (colorLerpT >= 1) {
						if (alive) {
							colorChangeToUniform = true;
							colorLerpT = 0;
						}
					}
				}
			}
		}
    }
}
