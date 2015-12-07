using UnityEngine;
using System.Collections;

public class FencingPlayerController : MonoBehaviour, IPlayerController {

	public Color color;
	SphereCollider shield;
	public bool Armed;
	public GameObject equipmentHand;
	GameObject Equipment;
	public FencingEquipment equipScript;
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
	public FencingInputHandler input;
	public ConfettiScript deathScript;
	private bool movementAllowed;
	public void MovementAllowed(bool permission)
	{
		movementAllowed = permission;
	}
	Renderer rend;
	public Vector3 respawnPoint;
	public GameObject rapier;
	public GameObject deathEffect;
	public GameObject projectorPrefab;
	public Transform projectorPosition;
	
	// Use this for initialization
	void Start () {
		//sound =  GetComponent<AudioSource>();
		cam = Camera.main.GetComponent<FencingCameraController>();
		rend = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody>();
		anim = GetComponent <Animator>();
		equipScript = GetComponentInChildren<FencingEquipment>();
		//deathScript = transform.GetComponentInChildren<ConfettiScript>();
		shield = GetComponent<SphereCollider>();
		shield.enabled = false;
		
		input = GetComponent<FencingInputHandler>();
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
	
	public void InitializeStatCard()
	{
		stats = new FencingStatsCard ();
		//stats.HardResetStats ();
	}
	
	public float TotalScore()
	{
		return stats.TotalScore();
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
		anim.SetTrigger("Attack");
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
		equipScript.Throw(anim.GetFloat("Run")>9, input.facingRight ? 1 : -1);
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
		GameObject d = Instantiate(deathEffect,transform.position,Quaternion.identity) as GameObject;
		d.GetComponent<ConfettiScript>().PartyToDeath(color,3);
		CreateDeathSplat();
        rb.constraints = RigidbodyConstraints.None;
        alive = false;
        cam.PlayShake(transform.position);
        cam.RecountPlayers();
		if (equipScript != null) { equipScript.Deflect();}
        transform.position = new Vector3 (0,0,3);
        rb.constraints = RigidbodyConstraints.FreezeAll;
        wizard.UpdatePlayerCount();
		stats.EndLifeTime ();
		stats.AddDeath ();
	}
	
	private void CreateDeathSplat()
	{
		GameObject p = Instantiate(projectorPrefab,projectorPosition.position,projectorPosition.rotation) as GameObject;
		Vector3 fixedProjectorPosition = new Vector3(transform.position.x,transform.position.y,-4.5f);
		p.GetComponent<FencingPaintSplatter>().Activate(fixedProjectorPosition,color);
	}
	
	public void Respawn() 
	{
		alive = true;
		transform.position = respawnPoint;
		GameObject r = Instantiate(rapier,Vector3.zero,Quaternion.identity) as GameObject;
		r.transform.parent = equipmentHand.transform;
		r.transform.localPosition = new Vector3(0.5f,0,0);
		r.transform.eulerAngles = new Vector3(0,0,270);
		r.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
		r.GetComponent<FencingEquipment>().ResetColor(color);
		equipScript = r.GetComponent<FencingEquipment>();
		equipScript.owner = this.gameObject;
		ResetRigidBodyConstraints();
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
		if (equipmentHand.transform.childCount > 1)
		{
			Debug.Log("Something is weird. Equipscript is " + equipScript);
		}
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
