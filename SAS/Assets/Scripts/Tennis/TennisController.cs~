using UnityEngine;
using System.Collections;
using InControl;

public class TennisController : MonoBehaviour {

	public Color c1;
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
    
    private TennisInputHandler input;
	
	// Use this for initialization
	void Start () {
		sound =  GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<OverheadCameraController>();
		rend = GetComponent<Renderer>();
        anim = GetComponent <Animator>();
        equipmentCollider = equipment.GetComponent<CapsuleCollider>();
        input = GetComponent<TennisInputHandler>();
        input.control = this;
        rb = GetComponent<Rigidbody>();
		rend.material.color = c1;
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
		paint.color = c1;
		hitForce = 10;
    }
    
	
	// Update is called once per frame
	void Update () {

        if (alive)
        {
        	input.CheckInput();
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
		/*Vector3 endPosition = new Vector3(other.transform.position.x, transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp(transform.position, endPosition, Time.deltaTime);
		
		float vertAngle = BallHeightToAngle(other.transform.position.y);
		float horizAngle = PlayerXPositionToAngle(transform.position.x, transform.position.z);
		float force = PlayerDepthToForce(transform.position.z, vertAngle);
		
		Debug.Log ("Vert Angle: " + vertAngle);
		Debug.Log ("Horiz Angle: " + horizAngle);
		Debug.Log ("Force: " + force);
		
		Quaternion horizDirection = Quaternion.AngleAxis(horizAngle, Vector3.up);
		
		Quaternion vertDirection = Quaternion.AngleAxis(vertAngle, Vector3.right);
		*/
		//Vector3 force = transform.forward*10;
		//force.y *= 2;
		//force = new Vector3(0,30,60);
		Vector2 swingForce = input.GetStickForSwing();
		float xForce = swingForce.x * hitForce;
		float yForce = swingForce.y * hitForce;
		ballRB.AddForce(xForce,10,yForce,ForceMode.VelocityChange);
		
		other.GetComponent<BallMovement>().ResetCount();
		other.GetComponent<BallMovement>().Hit(c1);

		return true;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Ball") {
			hasHitBall = false;
		}
	}

	private void LookAtNet()
	{
		if (transform.position.z > 0) 
		{
			transform.eulerAngles = new Vector3(0, 0, 0);
		}
		else if (transform.position.z < 0)
		{
			transform.eulerAngles = new Vector3(0, 180, 0);
		}
	}

	private float BallHeightToAngle(float height)
	{
		// The following are just magic numbers that I've been recently testing
		float angle = 35f;
		if (height < 0.25) {
			angle = Random.Range (65, 75);
		} else if (height < 0.5) {
			angle = Random.Range (55, 65);
		} else if (height >= 0.5 && height < 1f) {
			angle = Random.Range (45, 55);
		} else {
			angle = Random.Range (35, 45);
		}
		return angle;
	}

	private float PlayerXPositionToAngle(float positionX, float positionZ)
	{
		float angle = 0;
		if (positionZ > 0) {
			if (positionX > 1) {
				angle = Random.Range (0, 20);
			} else if (positionX < 1) {
				angle = Random.Range (-20, 0);
			} else {
				angle = Random.Range (-20, 20);
			}
		} else if (positionZ < 0) {
			if(positionX > 1) {
				angle = Random.Range (-20, 0);
			} else if (positionX < 1) {
				angle = Random.Range (0, 20);
			} else {
				angle = Random.Range (-20, 20);
			}
		}
		return angle;
	}

	private float PlayerDepthToForce(float position, float angle)
	{
		// Same as above, magic numbers.
		float force = 1000;
		if (angle >= 65) {
			force = 1300;
		} else {
			if (Mathf.Abs (position) < 5f) {
				force = Random.Range (800, 1000);
			} else if (position >= 5f && position < 8f) {
				force = Random.Range (1000, 1100);
			} else {
				force = Random.Range (1100, 1300);
			}
		}
		return force;
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
	}

	public void Swing()
	{
		anim.SetTrigger ("SwingRacquet");
		isSwinging = true;
	}

	private void StartSwing()
	{
		equipmentCollider.enabled = true; 
	}

	private void EndSwing()
	{
		equipmentCollider.enabled = false;
		isSwinging = false;
	}
	
	public void Attack()
    {
		anim.SetTrigger("AttackRacquet");
		isAttacking = true;
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
}
