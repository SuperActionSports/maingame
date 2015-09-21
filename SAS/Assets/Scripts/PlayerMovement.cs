using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	
	private Vector3 movementVector;
	private CharacterController characterController;
	private float movementSpeed = 8;
	private float jumpPower = 15;
	private float gravity = 40;
	public int joystickNumber;
	
	
	void Start()
	{
		characterController = GetComponent<CharacterController>();
	}
	
	
	void Update()
	{
		string joystickString = joystickNumber.ToString();
		
		movementVector.x = Input.GetAxis("LeftJoystickX_P" + joystickString) * movementSpeed;
		movementVector.z = -1*Input.GetAxis("LeftJoystickY_P" + joystickString) * movementSpeed;
		
		if(characterController.isGrounded)
		{
			movementVector.y = 0;
			
			if(Input.GetButtonDown("A_P" + joystickString))
			{
				movementVector.y = jumpPower; 
			}
		}
		
		movementVector.y -= gravity * Time.deltaTime;
		characterController.Move(movementVector * Time.deltaTime);
	}
}