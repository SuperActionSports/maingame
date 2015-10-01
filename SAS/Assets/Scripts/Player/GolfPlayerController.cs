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
	
	public InputDevice device {get; set;}

	public bool alive;
	
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
            magSpeedZ = 0;
            

			// Move Character
            float xVel = GetXVelocity();
			float zVel = GetZVelocity();
			Vector3 newPosition = new Vector3(xVel,0,zVel);
			if (!putting) { transform.position = transform.position + newPosition; }
			// If input has been given change to face new input direction
			if (newPosition != new Vector3(0,0,0)) { transform.rotation = Quaternion.LookRotation(-newPosition); }
            GetAttacking();
			CheckAnimStateForAttacking();
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
		if (Input.GetKey(attack) || (device != null && (device.LeftTrigger || device.RightTrigger)))
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
        if (equipmentCollider.enabled)
        {
            anim.SetTrigger("ComboAttack");
        }
        else
        {
            anim.SetTrigger("Attack");
        }

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
