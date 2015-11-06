using UnityEngine;
using System.Collections;

public class FencingPlayerController : MonoBehaviour, IPlayerController {

	public Color color;
	SphereCollider shield;
	public bool Armed;
	public GameObject equipmentHand;
	GameObject Equipment;
	FencingEquipment equipScript;
	Animator anim;
	private bool alive;
	public bool Alive()
	{
		return alive;	
	}
	GameObject[] respawnPoints;
	Rigidbody rb;
	public FencingWizard wizard;
	FencingCameraController cam;
	private FencingStatsCard stats;
	public FencingStatsCard Stats {
		get
		{
			return stats;
		}
		set
		{
			stats = value;
		}
	}
	public PlayerInputHandlerMatt input;
	public ConfettiScript deathScript;
	private bool movementAllowed;
	public void MovementAllowed(bool permission)
	{
		movementAllowed = permission;
	}
	Renderer rend;
	
	// Use this for initialization
	void Start () {
		stats = new FencingStatsCard ();
		stats.ResetStats ();
		//sound =  GetComponent<AudioSource>();
		cam = Camera.main.GetComponent<FencingCameraController>();
		rend = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody>();
		anim = GetComponent <Animator>();
		equipScript = GetComponentInChildren<FencingEquipment>();
		deathScript = transform.GetComponentInChildren<ConfettiScript>();
		shield = GetComponent<SphereCollider>();
		shield.enabled = false;
		
		input = GetComponent<PlayerInputHandlerMatt>();
		input.rb = rb;
		input.control = this;
		input.player = gameObject;
		
		alive = true;
		Armed = true;
		rend.material.color = color;
		equipScript.owner = this.gameObject;
		equipScript.ResetColor(color);
		movementAllowed = true;
		
		ResetRigidBodyConstraints();
	}
	
	void Update () {
		if (alive && movementAllowed)
		{
			input.HandleInput();            
		}	
	}
	
	public void SetRun(float v)
	{
		anim.SetFloat("Run",v);
	}
	
	/// <summary>
	/// Starts the Attack animation. Other attack methods are exclusively called by the animator.
	/// </summary>
	public void Attack()
	{
		if (equipScript != null) 
			{
				stats.AddStabAttempts ();
			}
	}
		public void AttackStart()
	{
		if (equipScript != null) equipScript.StartAttack();
	}
	
	public void AttackEnd()
	{
		if (equipScript != null) equipScript.EndAttack();
	}
	public void ThrowRapier()
	{
		anim.SetTrigger("Throw");
		stats.AddThrowAttempts ();
	}
	
	public void ThrowEquipment()
	{
		equipScript.Throw(anim.GetFloat("Run")>9);
		Disarm();
	}
	
	public void Disarm()
	{
		equipScript = null;
		Armed = false;
	}
	
	public bool GetHit()
	{
		if (!shield.enabled)
		{
			MakeDead();
			return true;
		}
		return false;
	}
	
	private void MakeDead()
	{
		deathScript.Party(color);
        rb.constraints = RigidbodyConstraints.None;
        alive = false;
        cam.PlayShake(transform.position);
		if (equipScript != null) { equipScript.Deflect();}
        gameObject.SetActive(false);
        wizard.UpdatePlayerCount();
        Destroy(gameObject,3);
		stats.EndLifeTime ();
		stats.AddDeath ();

    }
    
    public void AddKill()
    {
    	stats.AddKill();
    }
	
	public void Counter()
	{
		if (Armed) { anim.SetBool("Counter", true); }
	}
	
	public void StartCounter()
	{
		shield.enabled = true;
	}	
	
	public void EndCounter()
	{
		shield.enabled = false;
		anim.SetBool("Counter", false);
	}
	
	public GameObject PickUp(GameObject equip)
	{	
		if (equipScript == null)
		{
			equipScript = equip.GetComponent<FencingEquipment>();
			equip.transform.parent = equipmentHand.transform;
			Armed = true;
			return this.gameObject;
		}
		return null;
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
}
