using UnityEngine;
using System.Collections;
using InControl;

public class LiaisonTestPlayerController : MonoBehaviour {

	// Color variables
	public Color c1;
	
	//Keyboard Keybinding Stuff
	public KeyCode left;
	public KeyCode right;
	public KeyCode up;
	public KeyCode down;
    public KeyCode attack;
    public KeyCode debugKill;

	public InputDevice device {get; set;}

	// Generla Game Player variables
	public bool alive;
	private Rigidbody rb;
	public float xDirection;
	public float zDirection;
	public float walkSpeed;

	// Game Objects and Components
    public GameObject[] respawnPoints;

	void Start () {
		// Get Components and Game Objects
		respawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
		if (respawnPoints.Length == 0)
		{
			Debug.Log("There aren't any respawn points, you catastrophic dingus.");
		}
		rb = GetComponent<Rigidbody>();

		// Set up color variables
		

		// Set up general player variables
		alive = true;
		ResetRigidBodyConstraints();
		walkSpeed = 10f;

    }
    

	void Update () {
        if (alive)
        {
			// Reset velocity to 0
        	rb.velocity = new Vector3(0,0,0);

			// Move player in x and z directions 
			xDirection = 0;
			zDirection = 0;
            float xVel = GetXVelocity();
			float zVel = GetZVelocity();
			Vector3 newPosition = new Vector3(xVel,0,zVel);
			gameObject.transform.position += newPosition;

		}	
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
    }
	
	public void Kill()
	{
		//Magic Number
		rb.AddForce(40, 25, 0, ForceMode.VelocityChange);
        MakeDead();
    }

    public void Kill(Vector3 direction)
    {
		rb.AddForce(direction);
		MakeDead();
    }

    public void Respawn()
    {
        alive = true;
        ResetRigidBodyConstraints();
        rb.velocity = new Vector3(0, 0, 0);
        //Debug.Log("Length: " + respawnPoints.Length);
        transform.position = respawnPoints[Mathf.FloorToInt(Random.Range(0, respawnPoints.Length))].transform.position;
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
}
