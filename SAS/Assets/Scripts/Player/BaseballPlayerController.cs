using UnityEngine;
using System.Collections;
using InControl;

public class BaseballPlayerController : MonoBehaviour {

	public Color c1;
	public int playerNumber;
	public int runs;

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
	
	public InputDevice device {get; set;}

	public bool alive;
	
	private Rigidbody rb;
	public RaycastHit groundHit;
	public float magSpeedX;
	public float magSpeedY;
    public float debugYMod;
	public Vector3 speed;
    private bool doubleJumpAllowed;

    public GameObject[] respawnPoints;
    public GameObject equipment;
    private CapsuleCollider equipmentCollider;
    public float impactMod;


    public BaseballCameraController cam;
	private PaintSplatter paint;
	private AudioSource sound;
    private Animator anim;

	[Range(1,20)]
	public float speedMagnitude;
	// Use this for initialization
	void Start () {
	 	sound =  GetComponent<AudioSource>();
        cam = Camera.main.GetComponent<BaseballCameraController>();
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
        doubleJumpAllowed = true;
		impactMod = 7.5f;
        respawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
     
        if (respawnPoints.Length == 0)
        {
            Debug.Log("There aren't any respawn points, you catastrophic dingus.");
        }
        
		paint = GetComponent<PaintSplatter>();
		paint.color = c1;
    }
    
	
	// Update is called once per frame
	void Update () {
        if (alive)
        {
            magSpeedX = 0;
            magSpeedY = 0;
            
            float xVel = GetXVelocity();
			GetYVelocity();
			transform.position = transform.position + new Vector3(xVel,0,0);
			if (xVel < 0)
			{
                transform.rotation = new Quaternion(transform.rotation.x, 180f, transform.rotation.z, transform.rotation.w);
            }
			else if (xVel > 0 && transform.rotation.y > 0)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 0f, transform.rotation.z, transform.rotation.w);
            }
            GetAttacking();
			//CheckAnimStateForAttacking();
		}	
		
		UpdateColor();
		
		GetRespawn();
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
        paint.Paint(transform.position,paint.color);
        rb.constraints = RigidbodyConstraints.None;
        alive = false;
        anim.SetBool("Alive", false);
        cam.PlayShake(transform.position);
        /* foreach (Transform child in transform)
         {
             Vector3 t = child.transform.TransformPoint(child.transform.position);
             child.parent = null;
             child.transform.position = t;

         }*/
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
		sound.Play ();
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
        transform.position = respawnPoints[Mathf.FloorToInt(Random.Range(0, respawnPoints.Length))].transform.position;
        colorChangeToUniform = true;
    }
	
	private void GetAttacking()
	{
		if (Input.GetKey(attack) || (device != null && (device.LeftTrigger.WasPressed || device.RightTrigger.WasPressed)))
		{
			Attack ();
		}
		else if (Input.GetKey(attack))
		{
			Attack();
		}
	}
	
	private void Attack()
    {
            anim.SetTrigger("Attack");
    }

	public void StartAttack()
	{
		equipmentCollider.enabled = true;
	}

	public void StopAttack()
	{
		equipmentCollider.enabled = false;
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
			if (doubleJumpAllowed)
			{
				rb.velocity = new Vector3(rb.velocity.x, speedMagnitude * 4f, rb.velocity.z);
				doubleJumpAllowed = false;
			}
			else if (Physics.Raycast(transform.position, Vector3.down, out groundHit, 1.1f))
			{
				if (groundHit.collider.CompareTag("field"))
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
				if (groundHit.collider.CompareTag("field"))
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
			magSpeedX = -2;
		}
		if (Input.GetKey(right))
		{
			magSpeedX = 2;
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
