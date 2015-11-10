using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	
	private Vector3 movementVector;
	private CharacterController characterController;
	private float movementSpeed = 8;
	private float jumpPower = 15;
	private float gravity = 40;
	public KeyCode left;
	public KeyCode right;
	public KeyCode up;
	public KeyCode down;
	public KeyCode attack;
	public KeyCode jump;
	public KeyCode debugKill;
	public float magSpeedX;
	public float magSpeedY;
	public float magSpeedZ;
	public int joystickNumber;
	
	
	void Start()
	{
		characterController = GetComponent<CharacterController>();
	}
	
	
	void Update()
	{
		if (Input.GetKey(left))
		{
			magSpeedX = -1;
		}
		if (Input.GetKey(right))
		{
			magSpeedX = 1;
		}
		if (Input.GetKey(up))
		{
			magSpeedZ = -1;
		}
		if (Input.GetKey(down))
		{
			magSpeedZ = 1;
		}

		movementVector.x = magSpeedX * movementSpeed * Time.deltaTime;
		movementVector.z = magSpeedX * movementSpeed * Time.deltaTime;
		
		if(characterController.isGrounded)
		{
			movementVector.y = 0;
			
			if(Input.GetKey(jump))
			{
				movementVector.y = jumpPower; 
			}
		}
		
		movementVector.y -= gravity * Time.deltaTime;
		characterController.Move(movementVector * Time.deltaTime);
	}
}