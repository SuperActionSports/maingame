using UnityEngine;
using System.Collections;
using InControl;

public class GolfPlayerController : MonoBehaviour {

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
    public KeyCode attack;
    public KeyCode debugKill;

	// Golf
	public GolfBall ball;
	public float distanceToBall;
	public bool canHitBall;
	public bool putting;
	public bool swinging;
	public float swingStrength;

	public InputDevice device {get; set;}

	public bool alive;
	
	private Rigidbody rb;
	public RaycastHit groundHit;
	public float magSpeedX;
	public float magSpeedZ;
	public Vector3 speed;

    public GameObject[] respawnPoints;
    public GameObject equipment;
    private CapsuleCollider equipmentCollider;
    public float impactMod;


    public OverheadCameraController cam;
	private PaintSplatter paint;
	private AudioSource sound;
    private Animator anim;

	[Range(1,20)]
	public float speedMagnitude;
	// Use this for initialization
	void Start () {
		swinging = false;
		putting = false;
		sound =  GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<OverheadCameraController>();
		rend = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody>();
        anim = GetComponent <Animator>();
        equipmentCollider = equipment.GetComponent<CapsuleCollider>();
        equipmentCollider.enabled = false;
		rend.material.color = c1;
		speedMagnitude = 10f;
		colorChangeToUniform = false;
		colorLerpT = 0;
		alive = true;
        anim.SetBool("Alive", true);
		ResetRigidBodyConstraints();
		impactMod = 7.5f;
        respawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
     
        if (respawnPoints.Length == 0)
        {
            Debug.Log("There aren't any respawn points, you catastrophic dingus.");
        }
        
        transform.parent.rotation = transform.rotation;
		paint = GetComponent<PaintSplatter>();
		paint.color = c1;
    }
    
	
	// Update is called once per frame
	void Update () {
		
        if (alive)
        {
        	rb.velocity = new Vector3(0,0,0);
			// Update Ball Object Info to See if Close Enough to Putt
			ball = GolfBall.FindObjectOfType<GolfBall>();
			if (ball == null) {
				distanceToBall = 0;
				canHitBall = false;
			}
			else {
			 distanceToBall = Mathf.Sqrt (Mathf.Pow ((ball.transform.position.x - transform.position.x), 2)
			                                   + Mathf.Pow ((ball.transform.position.z - transform.position.z), 2));
			 canHitBall = (distanceToBall < 1.5);
			}

			// Move Character 
			magSpeedX = 0;
			magSpeedZ = 0;
            float xVel = GetXVelocity();
			float zVel = GetZVelocity();
			Vector3 newPosition = new Vector3(xVel,0,zVel);
			if (!putting) {
				transform.parent.transform.position = transform.parent.transform.position + newPosition; 	
				// If input has been given change to face new input direction
				if (newPosition != new Vector3(0,0,0)) { 
					transform.rotation = Quaternion.LookRotation(-newPosition); 
					transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y + 90, transform.eulerAngles.z);
				}
			}
			else {
				if (!swinging) { transform.parent.transform.RotateAround (ball.transform.position, Vector3.up, 10*xVel+-10*zVel); }
			}
			
			GetAttacking(putting, canHitBall);
			CheckAnimStateForAttacking();
		}	
		
		UpdateColor();
		GetRespawn();
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
	
	private void MakeDead()
	{
		alive = false;
		//Need the normal of the local x axis of bat
        paint.Paint(transform.position,paint.color);
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
		// Check Anim State for Putting
		if (putting) { 
			anim.SetBool ("Putting", true);
			if (swinging) { 
				if (anim.GetBool ("Swing") != true) { anim.SetBool ("BackSwing", true); }
			}
		} else {
			anim.SetBool ("Putting", false);
		}
	}
	
	public void Kill()
	{
		//Magic Number
		rb.AddForce(40, 25, 0, ForceMode.VelocityChange);
		if (putting) {
			ball.beingHit = false;
		}
		putting = false;
		swinging = false;
        MakeDead();
    }

    public void Kill(Vector3 direction)
    {
		rb.AddForce(40, 25, 0, ForceMode.VelocityChange);
		if (putting) {
			ball.beingHit = false;
		}
		putting = false;
		swinging = false;
		MakeDead();
    }

    public void Respawn()
    {
        alive = true;
        ResetRigidBodyConstraints();
        rb.velocity = new Vector3(0, 0, 0);
        anim.SetBool("Alive", true);
        //Debug.Log("Length: " + respawnPoints.Length);
        transform.position = respawnPoints[Mathf.FloorToInt(Random.Range(0, respawnPoints.Length))].transform.position;
        colorChangeToUniform = true;
    }
	
	private void GetAttacking(bool putting, bool CanHitBall)
	{
		if (putting) {
			if (swinging) {
				if (Input.GetKey (attack)) {
					BackSwing ();
				}
				if (Input.GetKeyUp (attack)) {
					Swing ();
				}
			}
			else {
				if (Input.GetKeyDown (attack)) {
					BackSwing ();
				}
			}
			return;
		} else {
			if (Input.GetKeyDown (attack) || (device != null && (device.LeftTrigger || device.RightTrigger))) {
				if (CanHitBall && !ball.beingHit) {
					StartPutting ();
					return;
				} else {
					Attack ();
					return;
				}
			}
		}
	}
	
	private void Attack()
    {
		anim.SetTrigger("SwingAttack");
    }
    
    private void StartAttacking()
    {
    	equipmentCollider.enabled = true;
    }

	private void StopAttack()
	{
		equipmentCollider.enabled = false;
	}

	private void StartPutting() {
		//transform.parent.transform.LookAt (ball.transform.position);
		transform.parent.transform.position = ball.transform.position;
		transform.localEulerAngles = new Vector3(0,0,0);
		transform.localPosition = new Vector3 (-2.51f, transform.localPosition.y, transform.localPosition.z);
		putting = true;
		(ball as GolfBall).beingHit = true;
	}

	private void BackSwing() {
		if (!swinging) {
			swinging = true;
			swingStrength = 5f;
		} else {
			swingStrength+=0.5f;
		}
	}

	private void Swing() {
		anim.SetBool ("BackSwing", false);
		anim.SetBool ("Swing", true);
		(ball as GolfBall).Putt (60f*swingStrength*transform.forward, c1);
		FinishSwing ();
	}

	private void FinishSwing() {
		swinging = false;
		putting = false;
		anim.SetBool ("BackSwing", false);
		anim.SetBool ("Swing", false);
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
