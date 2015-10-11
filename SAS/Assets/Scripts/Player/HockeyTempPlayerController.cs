using UnityEngine;
using System.Collections;
using InControl;

public class HockeyTempPlayerController : MonoBehaviour {

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
	public KeyCode lookLeft;
	public KeyCode lookRight;
	public KeyCode lookUp;
	public KeyCode lookDown;
	public InputDevice device {get; set;}
	
	// Generla Game Player variables
	public bool alive;
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
	public CapsuleCollider equipmentCollider;
	public OverheadCameraController cam;
	private AudioSource sound;
	private Animator anim;

	//debug
	public Vector3 vel;

	void Start () {
		// Get Components and Game Objects
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator> ();
		respawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
		renderers = GetComponentsInChildren<Renderer>();
		colorChangeToUniform = false;
		colorLerpT = 0;
        if (respawnPoints.Length == 0)
        {
            Debug.Log("There aren't any respawn points, you catastrophic dingus.");
        }

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

		// Set up hockey player variables
		maxSpeed = 200f;
    }
    

	void Update () {

        if (alive)
        {
			// Move player in x and z directions 
			xDirection = 0;
			zDirection = 0;
			float xVel = GetXVelocity();
			float zVel = GetZVelocity();
			rb.AddForce (2*(walkSpeed/Time.deltaTime)*(new Vector3(xVel, 0, zVel)));

			// Look player in x and z directions using second stick
			xLookDirection = 0;
			zLookDirection = 0;
			float xLookVel = GetXLook();
			float zLookVel = GetZLook();
			transform.Rotate (Vector3.up, walkSpeed*xLookVel-walkSpeed*zLookVel); 
			
			// Cap the max speed
			if (rb.velocity.x > maxSpeed) { rb.velocity = new Vector3(maxSpeed, 0, rb.velocity.z); }
			if (rb.velocity.x < -maxSpeed) { rb.velocity = new Vector3(-maxSpeed, 0, rb.velocity.z); }
			if (rb.velocity.z > maxSpeed) { rb.velocity = new Vector3(rb.velocity.x, 0, maxSpeed); }
			if (rb.velocity.z < -maxSpeed) { rb.velocity = new Vector3(rb.velocity.x, 0, -maxSpeed); }
			vel = rb.velocity;

			// Check if Attacking
			//TODO: Port from golf player minus putting : GetAttacking();
			//TODO: Port from golf player minus putting: CheckAnimStateForAttacking();
		}

		// Update constantly updated variables as needed
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
		alive = false;
        rb.constraints = RigidbodyConstraints.None;
    }
	
	public void Kill()
	{
		rb.AddForce(40, 25, 0, ForceMode.VelocityChange);
        MakeDead();
    }

    public void Kill(Vector3 direction)
    {
        MakeDead();    
    }

    public void Respawn()
    {
        alive = true;
        rb.velocity = new Vector3(0, 0, 0);
        transform.position = respawnPoints[Mathf.FloorToInt(Random.Range(0, respawnPoints.Length))].transform.position;
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

	private void UpdateColor()
	{
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
