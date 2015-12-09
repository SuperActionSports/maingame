using UnityEngine;
using System.Collections;
using InControl;

public class TennisControllerGans : MonoBehaviour, IPlayerController {

	public Color color;
	private Renderer rend;
	
	public GameObject tennisBall;
	
	public InputDevice device {get; set;}
	public bool ActiveDevice;

	public bool alive;
	bool isSwinging;
	bool hasHitBall;
	public bool isAttacking;

    public Vector3 respawnPoint;
    public GameObject equipment;
    private CapsuleCollider equipmentCollider;
    public float impactMod;
	private float startTime;

    public TennisCameraController cam;
	private PaintSplatterProjector paint;
	private AudioSource sound;
    private Animator anim;
    private AudioSource[] playerSounds; // [0] racquet hit, [1] player hit
    
    public float hitForce;
    
    private Rigidbody rb;
    
    private TennisInputHandlerGans input;

	public TennisWizard wizard;
	public TennisStatsCard stats;
	public bool OriginSideNorth;
	public float respawnTime;
	private float timeOfDeath;
	public bool movementAllowed;
	public float forceMod = 0.0001f;
	public void MovementAllowed(bool allowed)
	{
		movementAllowed = allowed;
	}

	// Use this for initialization
	void Start () {
		
		paint = GetComponent<PaintSplatterProjector> ();
		sound =  GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<TennisCameraController>();
		rend = GetComponent<Renderer>();
        anim = GetComponent <Animator>();
        equipmentCollider = equipment.GetComponent<CapsuleCollider>();
		equipmentCollider.enabled = false;
        input = GetComponent<TennisInputHandlerGans>();
        input.control = this;
        rb = GetComponent<Rigidbody>();
		rend.material.color = color;
		alive = true;
        anim.SetBool("Alive", true);
		impactMod = 1f;

		hitForce = 25;
//		stats.ResetStats ();
		SetOriginSide ();
		InitializeStatCard();
		respawnTime = 3;
		timeOfDeath = Mathf.Infinity;
		ActiveDevice = device != null;
		paint.Initialize (color);
		playerSounds = GetComponents<AudioSource>();
    }
    
	public void InitializeStatCard()
	{
		stats = new TennisStatsCard ();
		stats.HardResetStats ();
	}
    
	// Update is called once per frame
	void Update () {

		if (alive && movementAllowed)
        {
        	input.CheckInput();
			UpdateColor();
		}
		else if (Time.time >= timeOfDeath + respawnTime)
		{
			Respawn();
		}
		if (transform.position.y < -100f) { MakeDead (); Respawn (); }
	}
	
	public float TotalScore()
	{
		return stats.TotalScore();
	}

	void SetOriginSide() {
		if (input.transform.position.z > 0) {
			OriginSideNorth = true;
		} else {
			OriginSideNorth = false;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag ("Ball")) 
		{
			Debug.Log("Ball!");
			if(isSwinging)
			{
				playerSounds[0].Play ();
				hasHitBall = BallCollision(other);
				stats.AddContact();
				if(hasHitBall)
				{
					isSwinging = false;
				}
			}
		}
	}

	private bool BallCollision(Collider other)
	{
		//LookAtNet();
		Debug.Log("Ball has been hit");
		Rigidbody ballRB = other.GetComponent<Rigidbody>();

		ballRB.velocity = new Vector3(0,0,0);
		Vector2 swingAngle = input.GetStickForSwing(); 
		if (swingAngle.magnitude < 0.4f) 
		{
			swingAngle = new Vector2(Mathf.Sin (transform.eulerAngles.y),Mathf.Cos(transform.eulerAngles.y));
		}
		float xForce = swingAngle.x * hitForce;
		float zForce = swingAngle.y * hitForce;
		ballRB.AddForce(xForce,10,zForce,ForceMode.VelocityChange);
		
		other.GetComponent<BallMovement>().ResetCount();
		other.GetComponent<BallMovement>().Hit(this.gameObject);
		
		
		
		return true;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Ball") {
			hasHitBall = false;
		}
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - 1.1f, transform.position.z));
	}
	
	public void MakeDead()
	{
		wizard.PlayerKilled();
		playerSounds[1].Play ();
		alive = false;
        GetComponent<Rigidbody>().constraints =  RigidbodyConstraints.None;
        alive = false;
        anim.SetBool("Alive", false);
		timeOfDeath = Time.time;
        cam.PlayShake();
		stats.AddDeath ();
		stats.EndLifeTime ();
    }
    
    public Vector3 facingDirection()
    {
    	return transform.eulerAngles;
    }
	
	public void Kill()
	{
		//Magic Number
		rb.AddForce(40, 25, 0, ForceMode.VelocityChange);
        MakeDead();
    }

    public void Kill(Vector3 direction)
	{
		Vector3 rbForce = direction;
		Debug.Log("Rbforce: " + rbForce);
		//rbForce.x *=-1;
		//rbForce.z *= -1;
		rb.AddForce (rbForce, ForceMode.VelocityChange);
		paint.Splatter (transform.position, rbForce);
		//rb.AddForce(Vector3.Cross(new Vector3(impactMod,impactMod,impactMod), direction), ForceMode.VelocityChange);
        MakeDead();    
	}
	
	public void WhatIsWindUp()
	{
		Debug.Log(input.GetSwingForce());
	}

	public void Serve()
	{
		Instantiate(tennisBall, transform.position + new Vector3(0, 2f, 0),Quaternion.identity);
	}
	
	public void Respawn()
	{
		GetComponent<Rigidbody>().constraints =  RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		alive = true;
		transform.rotation = Quaternion.identity;
		rb.velocity = new Vector3(0, 0, 0);
		anim.SetBool("control.alive", true);
		transform.position = respawnPoint;
		stats.StartLifeTime ();
	}

	public void Swing(float swingForce)
	{
		hitForce = Mathf.Clamp(swingForce, 20,40);
		anim.SetTrigger ("SwingRacquet");
		anim.SetFloat("WindingUp",0);
		isSwinging = true;
		stats.AddSwing ();
	}
	
	public void WindUp()
	{
		anim.SetFloat("WindingUp",input.GetSwingForce());
	}

	public void StartSwing()
	{
		equipmentCollider.enabled = true; 
	}

	public void EndSwing()
	{
		equipmentCollider.enabled = false;
		anim.SetFloat("WindingUp",0);
		isSwinging = false;
	}
	
	public void Attack()
    {
		anim.SetTrigger("AttackRacquet");
		isAttacking = true;
		stats.AddAttemptedAttack ();
    }

	public void StartAttack()
	{
		equipmentCollider.enabled = true;
	}

	public void EndAttack()
	{
		equipmentCollider.enabled = false;
		isAttacking = false;
	}

	public bool Alive()
	{
		return alive;
	}

	private void UpdateColor()
	{
		rend.material.color = color;
	}
	
	public void ScorePoints(int value)
	{
		stats.Score(value);
	}
}
