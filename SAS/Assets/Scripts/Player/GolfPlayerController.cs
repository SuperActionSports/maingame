using UnityEngine;
using System.Collections;
using InControl;

public class GolfPlayerController : MonoBehaviour {

	// Color variables
	public Color c1;
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

	public InputDevice device {get; set;}

	// Generla Game Player variables
	public bool alive;
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

	void Start () {
		// Get Components and Game Objects
		respawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
		if (respawnPoints.Length == 0)
		{
			Debug.Log("There aren't any respawn points, you catastrophic dingus.");
		}
		sound =  GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<OverheadCameraController>();
		renderers = GetComponentsInChildren<Renderer>();
		rb = GetComponent<Rigidbody>();
        anim = GetComponent <Animator>();
		equipmentCollider = GetComponentsInChildren<CapsuleCollider> ()[1]; // 0 returns collider on THIS object
        equipmentCollider.enabled = false;

		// Set up color variables
		foreach (Renderer rend in renderers) {
			if (rend.material.name == "PlayerMaterial") rend.material.color = c1;
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
    }
    

	void Update () {
        if (alive)
        {
			// Reset velocity to 0
        	rb.velocity = new Vector3(0,0,0);

			// Update ball object and see if player is close enough to putt
			ball = GolfBall.FindObjectOfType<GolfBall>();
			if (ball == null) {
				distanceToBall = 0;
				canHitBall = false;
			}
			else {
				distanceToBall = Mathf.Sqrt (Mathf.Pow ((ball.transform.position.x - transform.position.x), 2)
			                                   + Mathf.Pow ((ball.transform.position.z - transform.position.z), 2));
				canHitBall = (distanceToBall < 2);
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
					Transform sweetspot = transform.FindChild("Sweetspot");
					sweetspot.SetParent (null);
					transform.SetParent (sweetspot);
					sweetspot.RotateAround (ball.transform.position, Vector3.up, walkSpeed*xVel-walkSpeed*zVel); 
					transform.SetParent (null);
					sweetspot.SetParent (this.transform);
				}
			}
			// Check if player is attacking
			GetAttacking(putting, canHitBall);
			CheckAnimStateForAttacking();
		}	

		// Update player color and respawn player if necessary
		UpdateColor();
		GetRespawn();
	}
	
	private void ResetRigidBodyConstraints() 
	{
		rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
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
		rb.AddForce(direction);
		if (putting) {
			ball.beingHit = false;
		}
		putting = false;
		swinging = false;
		MakeDead();
    }

    public void Respawn()
    {
        alive = true;
        ResetRigidBodyConstraints();
        rb.velocity = new Vector3(0, 0, 0);
        anim.SetBool("Alive", true);
        //Debug.Log("Length: " + respawnPoints.Length);
        transform.position = respawnPoints[Mathf.FloorToInt(Random.Range(0, respawnPoints.Length))].transform.position;
        colorChangeToUniform = true;
    }
	
	private void GetAttacking(bool putting, bool CanHitBall)
	{
		if (putting) {
			if (Input.GetKeyDown (attack) && !swinging) {
				BackSwing ();
			}
			else if (Input.GetKey (attack) && swinging) {
				BackSwing ();
			}
			else if (Input.GetKeyUp (attack) && swinging) {
				Swing ();
			}
		} 
		else {
			if (Input.GetKeyDown (attack) || (device != null && (device.LeftTrigger || device.RightTrigger))) {
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

		Transform sweetspot = transform.FindChild("Sweetspot");
		sweetspot.SetParent (null);
		transform.SetParent (sweetspot);
		sweetspot.position = ball.transform.position;
		transform.SetParent (null);
		sweetspot.SetParent (this.transform);

		putting = true;
		(ball as GolfBall).beingHit = true;

	}

	private void BackSwing() {
		// Begin swinging when button is pressed
		if (!swinging) {
			swinging = true;
			swingStrength = 5f;
		}
		// Increase swing strength while button is held
		else {
			swingStrength += 0.25f;
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
		(ball as GolfBall).Putt (60f*swingStrength*transform.forward, c1);
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
					rend.material.color = Color.Lerp (new Color (0, 0, 0, 0), c1, colorLerpT);
					if (colorLerpT >= 1) {
						colorChangeToUniform = false;
						colorLerpT = 0;
					}
				} else {
					rend.material.color = Color.Lerp (c1, new Color (0, 0, 0, 0), colorLerpT);
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
