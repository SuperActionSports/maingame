using UnityEngine;
using System.Collections;
using InControl;

public class TennisController : MonoBehaviour {

	public Color c1;
	private bool colorChangeToUniform;
	private Renderer rend;
	private float colorLerpT;
    private bool facingLeft;
	
	//Keyboard Keybinding Stuff
	public KeyCode left;
	public KeyCode right;
	public KeyCode up;
	public KeyCode down;
	public KeyCode swing;
    public KeyCode attack;
    public KeyCode debugKill;

	private GameObject[] respawnPointsTeamA;
	private GameObject[] respawnPointsTeamB;
	
	public InputDevice device {get; set;}

	public bool alive;
	bool isSwinging;
	bool hasHitBall;
	public bool isAttacking;
	
	private Rigidbody rb;
	public RaycastHit groundHit;
	public float magSpeedX;
	public float magSpeedZ;
	public Vector3 speed;
	public bool putting;

    public GameObject[] respawnPoints;
    public GameObject equipment;
    private CapsuleCollider equipmentCollider;
    public float impactMod;
	private float startTime;

    public OverheadCameraController cam;
	private PaintSplatter paint;
	private AudioSource sound;
    private Animator anim;

	[Range(1,20)]
	public float speedMagnitude;
	// Use this for initialization
	void Start () {
		putting = false;
		sound =  GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<OverheadCameraController>();
		rend = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody>();
        anim = GetComponent <Animator>();
        equipmentCollider = equipment.GetComponent<CapsuleCollider>();
		//rend.material.color = c;
		speedMagnitude = 10f;
		colorChangeToUniform = false;
		colorLerpT = 0;
		alive = true;
        anim.SetBool("Alive", true);
		ResetRigidBodyConstraints();
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
    }
    
	
	// Update is called once per frame
	void Update () {

        if (alive)
        {
            magSpeedX = 0;
            magSpeedZ = 0;

			startTime = Time.time;

			// Move Character
            float xVel = GetXVelocity();
			float zVel = GetZVelocity();
			Vector3 newPosition = new Vector3(xVel,0,zVel);
			if (!putting) { transform.position = transform.position + newPosition; }
			// If input has been given change to face new input direction
			if (newPosition != new Vector3(0,0,0)) { transform.rotation = Quaternion.LookRotation(-newPosition); }
			CheckServe();
			GetSwinging();
            GetAttacking();
		}	

		UpdateColor();
		GetRespawn();
	}

	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag ("Ball")) 
		{
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
		LookAtNet();

		Rigidbody ballRB = other.GetComponent<Rigidbody>();

		ballRB.velocity = new Vector3(0,0,0);
		Vector3 endPosition = new Vector3(other.transform.position.x, transform.position.y, transform.position.z);
		transform.position = Vector3.Lerp(transform.position, endPosition, Time.deltaTime);
		
		float vertAngle = BallHeightToAngle(other.transform.position.y);
		float horizAngle = PlayerXPositionToAngle(transform.position.x, transform.position.z);
		float force = PlayerDepthToForce(transform.position.z, vertAngle);
		
		Debug.Log ("Vert Angle: " + vertAngle);
		Debug.Log ("Horiz Angle: " + horizAngle);
		Debug.Log ("Force: " + force);
		
		Quaternion horizDirection = Quaternion.AngleAxis(horizAngle, Vector3.up);
		
		Quaternion vertDirection = Quaternion.AngleAxis(vertAngle, Vector3.right);
		ballRB.AddForce(vertDirection * horizDirection * -transform.forward * force);

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

	private void ResetRigidBodyConstraints() 
	{
		rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
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

	private void CheckServe()
	{

	}
	
	private void MakeDead()
	{
		alive = false;
		//Need the normal of the local x axis of bat
		paint.Paint (transform.position, paint.color);
        rb.constraints = RigidbodyConstraints.None;
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
	
	private void CheckAnimStateForAttacking()
	{

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

    public void Respawn()
    {
        alive = true;
        ResetRigidBodyConstraints();
        rb.velocity = new Vector3(0, 0, 0);
        anim.SetBool("Alive", true);
        //Debug.Log("Length: " + respawnPoints.Length);
        transform.position = respawnPointsTeamA[Mathf.FloorToInt(Random.Range(0, respawnPointsTeamA.Length))].transform.position;
        //colorChangeToUniform = true;
    }

	private void GetSwinging()
	{
		if (Input.GetKeyDown (swing)) 
		{
			Swing();
		}
	}

	private void Swing()
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
	
	private void GetAttacking()
	{
		if (Input.GetKeyDown (attack) || (device != null && (device.LeftTrigger || device.RightTrigger)))
		{
			Attack ();
		}
		else if (Input.GetKeyDown (attack))
		{
			Attack();
		}
	}

	private void Attack()
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
    
    private float GetXVelocity()
    {
    	return device == null ? GetKeyboardXInput(): GetControllerXInput();
    }
	
	private float GetZVelocity()
	{
		return device == null ? GetKeyboardZInput(): GetControllerZInput();
	}
	
	
	private float GetKeyboardZInput()
	{
		if (Input.GetKey(up))
		{
			magSpeedZ = 1;
		}
		if (Input.GetKey(down))
		{
			magSpeedZ = -1;
		}
		return speedMagnitude * magSpeedZ * Time.deltaTime;
	}
	
	private float GetControllerZInput()
	{
		return speedMagnitude * device.Direction.Y * Time.deltaTime;
	}
	
	private float GetKeyboardXInput()
	{
		if (Input.GetKey(left))
		{
			magSpeedX = -1;
		}
		if (Input.GetKey(right))
		{
			magSpeedX = 1;
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
        if (colorChangeToUniform && alive)
        {
            rend.material.color = Color.Lerp(new Color(0, 0, 0, 0), c1, colorLerpT);
            if (colorLerpT >= 1)
            {
                    colorChangeToUniform = false;
                    colorLerpT = 0;
            }
        }
        else
        {
            rend.material.color = Color.Lerp(c1, new Color(0, 0, 0, 0), colorLerpT);
            if (colorLerpT >= 1)
            {
                if (alive)
                {
                    colorChangeToUniform = true;
                    colorLerpT = 0;
                }
            }
        }
    }
}
