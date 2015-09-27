﻿using UnityEngine;
using System.Collections;
using InControl;

public class PlayerControllerMatt : MonoBehaviour {

	public Color color;
	private Renderer rend;
	private float colorLerpT;
    private bool facingRight;
    public Material playerMaterial;
    public bool armed;
	
	//Keyboard Keybinding Stuff
	public KeyCode left;
	public KeyCode right;
	public KeyCode up;
	public KeyCode down;
    public KeyCode attack;
    public KeyCode debugKill;
    public KeyCode jab;
	public KeyCode throwEquip;
	public KeyCode counter;
	
	public InputDevice device {get; set;}

	public bool alive;
	
	private Rigidbody rb;
	public RaycastHit groundHit;
	public float magSpeedX;
	public float magSpeedY;
    public float debugYMod;
	public Vector3 speed;
    private bool doubleJumpAllowed;
    public GameObject centerStageObject;
    private Vector3 centerStage;
    private Quaternion startingRotation;

    public GameObject[] respawnPoints;
    private GameObject equipment;
    private CapsuleCollider equipmentCollider;
	private EquipmentThrow equipmentThrow;
    public float impactMod;


    public SideScrollingCameraController cam;
	private PaintSplatter paint;
	private AudioSource sound;
    private Animator anim;
    
    private float timeSinceKeyPress;
    public float repeatKeyDelay;

	[Range(1,20)]
	public float speedMagnitude;
	// Use this for initialization
	void Start () {
		sound =  GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<SideScrollingCameraController>();
		rend = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody>();
        anim = GetComponent <Animator>();
		equipment = getEquipment();
        equipmentCollider = equipment.GetComponent<CapsuleCollider>();
		equipmentThrow = equipment.GetComponent<EquipmentThrow>();
        armed = true;
		speedMagnitude = 10f;
		
		colorLerpT = 0;
		//SetColorForChildren();
		alive = true;
        anim.SetBool("Alive", true);
        centerStage = centerStageObject.transform.position;
		if (transform.position.x > 0) 
		{
			transform.rotation = new Quaternion(0,180,0,0);
			facingRight = false;
			anim.SetBool("FacingRight", false);
		}
		else{
			 transform.rotation = new Quaternion(0,0,0,0);
			 facingRight = true;
			anim.SetBool("FacingRight", true);
		}
		
        startingRotation = transform.rotation;
        
        
		ResetRigidBodyConstraints();
		
        doubleJumpAllowed = true;
		impactMod = 7.5f;
		
        respawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
     
        if (respawnPoints.Length == 0)
        {
            Debug.Log("There aren't any respawn points, you catastrophic dingus.");
        }
        
//		paint = GetComponent<PaintSplatter>();
//		paint.color = color;
		
		repeatKeyDelay = 0.093f;
		timeSinceKeyPress = repeatKeyDelay;
    }
    
	
	// Update is called once per frame
	void Update () {
        if (alive)
        {
            magSpeedX = 0;
            magSpeedY = 0;
            
            float xVel = GetXVelocity();
			GetYVelocity();
			transform.position += new Vector3(xVel,0,0);
			if (xVel < 0 && transform.rotation.y < 0)
			{
                transform.rotation = new Quaternion(transform.rotation.x, 180f, transform.rotation.z, transform.rotation.w);
                anim.SetBool("FacingRight", false);
                //facingRight = false;
                startingRotation = transform.rotation;
            }
			else if (xVel > 0 && transform.rotation.y > 0)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 0f, transform.rotation.z, transform.rotation.w);
				anim.SetBool("FacingRight", true);
				//facingRight = true;
				startingRotation = transform.rotation;
            }
            else
            {
            	//transform.rotation = startingRotation;
            }
            GetAttacking();
			CheckAnimStateForAttacking();
		}	
		
		UpdateColor();
		
		GetRespawn();
	}
	
	public void PickUp (GameObject rapier)
	{
		Debug.Log(transform.FindChild("RapierHand").childCount);
		rapier.transform.parent = this.transform.FindChild("RapierHand");
		rapier.transform.localPosition = new Vector3(0.5f,0,0);
		rapier.transform.localRotation = new Quaternion(270,270,0,0);
		equipment = rapier.gameObject;
		equipmentThrow.PickUp();
		armed = true;
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
        //paint.Paint(transform.position,paint.color);
        rb.constraints = RigidbodyConstraints.None;
        alive = false;
        anim.SetBool("Alive", false);
        //cam.PlayShake(transform.position);
        gameObject.SetActive(false);
        //equipment.transform.parent = null;
        equipmentThrow.Drop();
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
	//	sound.Play ();
		rb.AddForce(Vector3.Cross(new Vector3(impactMod,impactMod,impactMod), direction), ForceMode.VelocityChange);
        MakeDead();    
    }

    public void Respawn()
    {
        alive = true;
        ResetRigidBodyConstraints();
        rb.velocity = new Vector3(0, 0, 0);
        anim.SetBool("Alive", true);
        transform.position = respawnPoints[Mathf.FloorToInt(Random.Range(0, respawnPoints.Length))].transform.position;
    }
	
	private void GetAttacking()
	{
		if (Input.GetKeyDown(attack) || (device != null && (device.LeftTrigger || device.RightTrigger)))
		{
			Attack ();
		}
		else if (Input.GetKeyDown(throwEquip))
		{
			anim.SetTrigger("Throw");
			armed = false;
		}
		else if (Input.GetKey(attack))
		{
			Attack();
		}
	}
	
	private void Attack()
    {
    	timeSinceKeyPress += Time.deltaTime;
        if (equipmentCollider.enabled)
        {
            //anim.SetTrigger("ComboAttack");
        }
        else if (timeSinceKeyPress > repeatKeyDelay)
        {
            anim.SetTrigger("Attack");
            timeSinceKeyPress = 0;
        }

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
			anim.SetTrigger("Jump");
			if (doubleJumpAllowed)
			{
				rb.velocity = new Vector3(rb.velocity.x, speedMagnitude * 4f, rb.velocity.z);
				doubleJumpAllowed = false;
			}
			else if (Physics.Raycast(transform.position, Vector3.down, out groundHit, 1.1f))
			{
				if (groundHit.collider.CompareTag("Stage"))
				{
					doubleJumpAllowed = true;
					rb.velocity = new Vector3(rb.velocity.x, speedMagnitude * 4f, rb.velocity.z);
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
			if (doubleJumpAllowed)
			{
				rb.velocity = new Vector3(rb.velocity.x, speedMagnitude * 4f, rb.velocity.z);
				doubleJumpAllowed = false;
			}
			else if (Physics.Raycast(transform.position, Vector3.down, out groundHit, 1.1f))
			{
				if (groundHit.collider.CompareTag("Stage"))
				{
					doubleJumpAllowed = true;
					rb.velocity = new Vector3(rb.velocity.x, speedMagnitude * 4f, rb.velocity.z);
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
			facingRight = false;
		}
		if (Input.GetKey(right))
		{
			magSpeedX = 1;
			facingRight = true;
		}
		if (magSpeedX > 0)
		{
			//anim.SetTrigger("Right Hop"); 
		}
		return speedMagnitude * magSpeedX * Time.deltaTime;
	}
	
	private float GetControllerXInput()
	{
		return speedMagnitude * device.Direction.X * Time.deltaTime;
	}

	private void UpdateColor()
	{
		colorLerpT += Time.deltaTime;
		if (alive && colorLerpT < 1)
		{
			rend.material.color = Color.Lerp(new Color(0, 0, 0, 0), color, colorLerpT);
		}
		else if (!alive)
		{
			rend.material.color = Color.Lerp(color, new Color(0, 0, 0, 0), colorLerpT);

		}
	}
	
	public void throwEquipment()
	{
		equipment = transform.FindChild("RapierHand/Rapier").gameObject;
		if (equipmentThrow == null)
		{
			Debug.Log("Equipment " + equipment + " has no EquipmentThrow script.");
		}
		equipmentThrow.directionModifier = facingRight ? 1 : -1;
		equipmentThrow.c = color;
		equipmentThrow.Throw();		
	} 
	
	private GameObject getEquipment()
	{
		return transform.FindChild("RapierHand/Rapier").gameObject;

	}
}