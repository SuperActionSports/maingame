using UnityEngine;
using System.Collections;

public class PlayerControllerMatt : MonoBehaviour, IPlayerController {

	public Color color;
	private Renderer rend;
    public Material playerMaterial;
    
    public bool armed;
	public bool alive;
//	public bool Alive
//	{
//		get{
//		 return alive;
//		 }
//	}
	public bool Alive()
	{
		return alive;	
	}	
	public float impactMod;
	public bool previousFacingRight;
	
	private Rigidbody rb;

    public GameObject[] respawnPoints;
    private GameObject equipment;
	private GameObject shield;
	
    private CapsuleCollider equipmentCollider;
	private EquipmentThrow equipmentThrow;
	private RapierScript rapierScript;
	private ConfettiScript deathScript;
    public FencingCameraController cam;
	private PaintSplatter paint;
	private AudioSource sound;
    private Animator anim;
	public PlayerInputHandlerMatt input; // Input manager, instructed by Layla
	public FencingWizard wizard; // Game Wizard, handled by game
	public int wizardNumber;
	public bool movementAllowed;

	public FencingStatsCard stats;

	// Use this for initialization
	void Start () {
		stats = new FencingStatsCard ();
		stats.ResetStats ();
		sound =  GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<FencingCameraController>();
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
		//ResetRotation(); 
		impactMod = 7.5f;
		
        respawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
   
        if (respawnPoints.Length == 0)
        {
            Debug.Log("There aren't any respawn points, you catastrophic dingus.");
       }
        
//		paint = GetComponent<PaintSplatter>();
//		paint.color = color;
//		
		//wizard = GameObject.Find("FencingGameWizard").GetComponent<FencingGameManager>();
		UpdateColor();
		
		//rapierScript.parent = this.gameObject;
		rapierScript.ResetOwnership(this);
		movementAllowed = true;
    }
    
	// Update is called once per frame
	void Update () {
        if (alive && movementAllowed)
        {
            input.HandleInput();            
		}	
	}
	
	public void MovementAllowed(bool allowed)
	{
		movementAllowed = allowed;
		Debug.Log("From Contrller. Movement is allowed? " + movementAllowed);
	}
	
	public void setRun(float vel)
	{
		anim.SetFloat("Run",vel);
	}
	
	public void SetRotation(bool currentFacingRight)
	{
		if (currentFacingRight)
		{
			anim.rootRotation = new Quaternion(transform.rotation.x, 180f, transform.rotation.z, transform.rotation.w);
		}
		else
		{
			anim.rootRotation = new Quaternion(transform.rotation.x, 0f, transform.rotation.z, transform.rotation.w);
		}
	}
	
	public void Attack()
	{
		anim.SetTrigger("Attack");
		stats.AddStabAttempts ();
	}
			
	private void MakeDead()
	{
		deathScript.Party(color);
        rb.constraints = RigidbodyConstraints.None;
        alive = false;
        anim.SetBool("Alive", false);
        cam.PlayShake(transform.position);
		if (rapierScript != null) { rapierScript.Parry();}
        gameObject.SetActive(false);
        Debug.Log("Finna update player count.");
        wizard.UpdatePlayerCount();
        Debug.Log("Yeah.");
        Destroy(gameObject,3);
		stats.EndLifeTime ();
		stats.AddDeath ();

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
		stats.AddBlockAttempts ();
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
		stats.StartLifeTime ();

    }
    
	public void PickUp (GameObject rapier)
	{
		rapier.transform.parent = this.transform.FindChild("RapierHand");
		rapier.transform.localPosition = new Vector3(0.5f,0,0);
		rapier.transform.localRotation = new Quaternion(270,270,0,0);
		equipment = rapier.gameObject;
		equipmentThrow = rapier.GetComponent<EquipmentThrow>();
		equipmentThrow.PickUp(this);
		rapierScript = rapier.GetComponent<RapierScript> ();
		rapierScript.c = color;
		rapierScript.ResetOwnership(this);
		armed = true;
	}
	
	public void ThrowRapier()
	{
		anim.SetTrigger("Throw");
		stats.AddThrowAttempts ();
	}
	
	public void ThrowEquipment()
	{
		if (equipmentThrow == null)
		{
			Debug.Log("Equipment " + equipment + " has no EquipmentThrow script.");
		}
		else
		{
			armed = false;
			rapierScript.Attack();	
			equipmentThrow.directionModifier = input.facingRight ? 1 : -1;
			equipmentThrow.Throw(anim.GetFloat("Run") > 17);
			equipment = null;
			equipmentThrow = null;	
			rapierScript = null;
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
