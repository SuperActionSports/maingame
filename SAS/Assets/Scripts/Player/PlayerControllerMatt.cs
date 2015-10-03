﻿using UnityEngine;
using System.Collections;

public class PlayerControllerMatt : MonoBehaviour {

	public Color color;
	private Renderer rend;
    public Material playerMaterial;
    
    public bool armed;
	public bool alive;	
	public float impactMod;
	
	private Rigidbody rb;

    public GameObject[] respawnPoints;
    private GameObject equipment;
	private GameObject shield;
	
    private CapsuleCollider equipmentCollider;
	private EquipmentThrow equipmentThrow;
	private RapierScript rapierScript;
	private ConfettiScript deathScript;
    public SideScrollingCameraController cam;
	private PaintSplatter paint;
	private AudioSource sound;
    private Animator anim;
	private PlayerInputHandlerMatt input;

	// Use this for initialization
	void Start () {
		sound =  GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<SideScrollingCameraController>();
		rend = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody>();
        anim = GetComponent <Animator>();
        input = GetComponent<PlayerInputHandlerMatt>();
		input.rb = rb;
		input.control = this;
		input.player = gameObject;
 		     
		equipment = getEquipment();
        equipmentCollider = equipment.GetComponent<CapsuleCollider>();
		equipmentThrow = equipment.GetComponent<EquipmentThrow>();
		rapierScript = equipment.GetComponent<RapierScript>();
		deathScript = transform.GetComponentInChildren<ConfettiScript>();
		shield = transform.FindChild("Shield").gameObject;
		rapierScript.c = color;
        armed = true;
		rapierScript.ResetColor();
		
		//SetColorForChildren();
		alive = true;
        anim.SetBool("Alive", true);
		ResetRigidBodyConstraints();
		ResetRotation(); 
		impactMod = 7.5f;
		
        respawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
   
        if (respawnPoints.Length == 0)
        {
            Debug.Log("There aren't any respawn points, you catastrophic dingus.");
       }
        
//		paint = GetComponent<PaintSplatter>();
//		paint.color = color;
//		
		UpdateColor();
    }
    
	// Update is called once per frame
	void Update () {
        if (alive)
        {
            input.HandleInput();            
		}	
	}
	
	public void Attack()
	{
		anim.SetTrigger("Attack");
		rapierScript.Attack();
	}
			
	private void MakeDead()
	{
		Debug.Log("Make Dead");
		deathScript.Party();
		alive = false;
		//Need the normal of the local x axis of bat
        //paint.Paint(transform.position,paint.color);
        rb.constraints = RigidbodyConstraints.None;
        alive = false;
        anim.SetBool("Alive", false);
        //cam.PlayShake(transform.position);
        gameObject.SetActive(false);
        equipmentThrow.Drop();
    }
    
    public void Counter()
    {
    	anim.SetTrigger("Counter");
    }

	public void AttackStart()
	{
		rapierScript.Attack();
	}
	
	public void AttackEnd()
	{
		rapierScript.StopAttack();
	}
		
	public void ShieldOn()
	{
		shield.GetComponent<ShieldScript>().Activate();	
	}
	
	public void ShieldOff()
	{
		shield.GetComponent<ShieldScript>().Deactivate();
	}
	
	public void Kill()
	{	
		Debug.Log("Kill");
		//Magic Number
		rb.AddForce(40, 25, 0, ForceMode.VelocityChange);
        MakeDead();
    }

    public void Kill(Vector3 direction)
    {
		//Magic Number
	//	sound.Play ();
		Debug.Log("Kill with direction");
		rb.AddForce(Vector3.Cross(new Vector3(impactMod,impactMod,impactMod), direction), ForceMode.VelocityChange);
        MakeDead();    
    }

    public void Respawn()
    {
        alive = true;
        ResetRigidBodyConstraints();
        rb.velocity = new Vector3(0, 0, 0);
        anim.SetBool("Alive", true);
//        transform.position = respawnPoints[Mathf.FloorToInt(Random.Range(0, respawnPoints.Length))].transform.position;
    }
    
	public void PickUp (GameObject rapier)
	{
		rapier.transform.parent = this.transform.FindChild("RapierHand");
		rapier.transform.localPosition = new Vector3(0.5f,0,0);
		rapier.transform.localRotation = new Quaternion(270,270,0,0);
		equipment = rapier.gameObject;
		equipmentThrow = rapier.GetComponent<EquipmentThrow>();
		equipmentThrow.PickUp();
		armed = true;
	}
	
	public void ThrowEquipment()
	{
		if (equipmentThrow == null)
		{
			Debug.Log("Equipment " + equipment + " has no EquipmentThrow script.");
		}
		else
		{
			anim.SetTrigger("Throw");
			armed = false;
			equipmentThrow.directionModifier = input.facingRight ? 1 : -1;
			equipmentThrow.c = color;
			equipmentThrow.Throw();
			equipment = null;
			equipmentThrow = null;
			rapierScript.Attack();		
		}
	} 
	
	private void ResetRigidBodyConstraints() 
	{
		rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
		transform.rotation = Quaternion.identity;
	}
	
	private void ResetRotation()
	{
		if (transform.position.x > 0) 
		{
			transform.eulerAngles = new Vector3(0,180,0);
			input.facingRight = false;			
		}
		else{
			transform.eulerAngles = new Vector3(0,0,0);
			input.facingRight = true;
		}
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - 1.1f, transform.position.z));
	}
	
	private void UpdateColor()
	{
		rend.material.color = color;
	}
	
	private GameObject getEquipment()
	{
		return transform.FindChild("RapierHand/Rapier").gameObject;
	}
}