using UnityEngine;
using System.Collections;
using InControl;

public class TennisControllerGans : MonoBehaviour, IPlayerController {

	public Color color;
	private Renderer rend;
	
	private GameObject[] respawnPointsTeamA;
	private GameObject[] respawnPointsTeamB;
	public GameObject tennisBall;
	
	public InputDevice device {get; set;}

	public bool alive;
	bool isSwinging;
	bool hasHitBall;
	public bool isAttacking;

    public GameObject[] respawnPoints;
    public GameObject equipment;
    private CapsuleCollider equipmentCollider;
    public float impactMod;
	private float startTime;

    public OverheadCameraController cam;
	private PaintSplatter paint;
	private AudioSource sound;
    private Animator anim;
    
    public float hitForce;
    
    private Rigidbody rb;
    
    private TennisInputHandlerGans input;

	public TennisWizard wizard;
	public TennisStatsCard stats;

	// Use this for initialization
	void Start () {
		stats = new TennisStatsCard ();
		sound =  GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<OverheadCameraController>();
		rend = GetComponent<Renderer>();
        anim = GetComponent <Animator>();
        equipmentCollider = equipment.GetComponent<CapsuleCollider>();
        input = GetComponent<TennisInputHandlerGans>();
        input.control = this;
        rb = GetComponent<Rigidbody>();
		rend.material.color = color;
		alive = true;
        anim.SetBool("Alive", true);
		impactMod = 7.5f;
        respawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
		equipmentCollider.enabled = false;
        if (respawnPoints.Length == 0)
        {
            Debug.Log("There aren't any respawn points, you catastrophic dingus.");
        }
        
		respawnPointsTeamA = GameObject.FindGameObjectsWithTag ("RespawnPointTeamA");
		respawnPointsTeamB = GameObject.FindGameObjectsWithTag ("RespawnPointTeamB");

		paint = GetComponent<PaintSplatter>();
		paint.color = color;
		hitForce = 25;
		stats.ResetStats ();
    }
    
	
	// Update is called once per frame
	void Update () {

        if (alive)
        {
        	input.CheckInput();
			UpdateColor();
		}	
	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag ("Ball")) 
		{
			Debug.Log("Ball!");
			if(isSwinging)
			{
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

		Rigidbody ballRB = other.GetComponent<Rigidbody>();

		ballRB.velocity = new Vector3(0,0,0);
		Vector2 swingForce = input.GetStickForSwing();
		float xForce = swingForce.x * hitForce;
		float yForce = swingForce.y * hitForce;
		ballRB.AddForce(xForce,10,yForce,ForceMode.VelocityChange);
		
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
		alive = false;
		//Need the normal of the local x axis of bat
		paint.Paint (transform.position, paint.color);
        GetComponent<Rigidbody>().constraints =  RigidbodyConstraints.None;
        alive = false;
        anim.SetBool("Alive", false);
        //cam.PlayShake(transform.position);
        /* foreach (Transform child in transform)
         {
             Vector3 t = child.transform.TransformPoint(child.transform.position);
             child.parent = null;
             child.transform.position = t;

         }*/
		stats.AddDeath ();
		stats.EndLifeTime ();
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
		//sound.Play ();
		rb.AddForce(Vector3.Cross(new Vector3(impactMod,impactMod,impactMod), direction), ForceMode.VelocityChange);
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
		alive = true;
		rb.velocity = new Vector3(0, 0, 0);
		anim.SetBool("control.alive", true);
		//Debug.Log("Length: " + respawnPoints.Length);
		transform.position = respawnPointsTeamA[Mathf.FloorToInt(Random.Range(0, respawnPointsTeamA.Length))].transform.position;
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

	private void StartSwing()
	{
		equipmentCollider.enabled = true; 
	}

	private void EndSwing()
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

	private void StartAttack()
	{
		equipmentCollider.enabled = true;
	}

	private void EndAttack()
	{
		equipmentCollider.enabled = false;
		isAttacking = false;
	}

	public bool Alive()
	{
		return alive;
	}

	public void MovementAllowed(bool val)
	{
		//This is temporary -Gans
	}

	private void UpdateColor()
	{
		rend.material.color = color;
	}
}
