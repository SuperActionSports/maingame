using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Color c1;
	private bool colorChangeToUniform;
	private Renderer rend;
	private float colorLerpT;
    private bool facingLeft;
	
	public KeyCode left;
	public KeyCode right;
	public KeyCode up;
	public KeyCode down;
    public KeyCode attack;
    public KeyCode debugKill;

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


    public SideScrollingCameraController cam;
	private PaintSplatter paint;

    private Animator anim;

	[Range(1,20)]
	public float speedMagnitude;
	// Use this for initialization
	void Start () {
        cam = Camera.main.GetComponent<SideScrollingCameraController>();
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
            if (Input.GetKey(left))
            {
                magSpeedX = -1;
            }
            if (Input.GetKey(right))
            {
                magSpeedX = 1;
            }
            if (Input.GetKeyDown(up))
            {
                if (doubleJumpAllowed)
                {
                    rb.velocity = new Vector3(rb.velocity.x, speedMagnitude * 4f, rb.velocity.z);
                    doubleJumpAllowed = false;
                }
                else if (Physics.Raycast(transform.position, Vector3.down, out groundHit, 1.1f))
                {
                    if (groundHit.collider.CompareTag("Stage"))
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

            float xSpeed = speedMagnitude * magSpeedX;
            speed = new Vector3(xSpeed, 0, 0);
            transform.position = transform.position + new Vector3(speed.x * Time.deltaTime, 0, 0);
            if (speed.x < 0)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 180f, transform.rotation.z, transform.rotation.w);
            }
            else if (speed.x > 0 && transform.rotation.y > 0)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 0f, transform.rotation.z, transform.rotation.w);
            }
        }	

        UpdateColor();

        if (Input.GetKeyDown(debugKill))
		{
            if (alive)
            {
                MakeDead();
            }
            else
                Respawn();
        }

        if (Input.GetKey(attack))
        {
            Attack();
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            equipmentCollider.enabled = true;
        }
        else if (equipmentCollider.enabled)
        {
            equipmentCollider.enabled = false;
        }
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
	
    private void MakeDead()
    {
        alive = !alive;
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

    public void Kill()
    {
        //Magic Number
        rb.AddForce(40, 25, 0, ForceMode.VelocityChange);
        MakeDead();
    }

    public void Kill(Vector3 direction)
    {
        //Magic Number
		rb.AddForce(Vector3.Cross(new Vector3(impactMod,impactMod,impactMod), direction), ForceMode.VelocityChange);
        MakeDead();    
    }

    public void Respawn()
    {
        alive = true;
        ResetRigidBodyConstraints();
        rb.velocity = new Vector3(0, 0, 0);
        anim.SetBool("Alive", true);
        Debug.Log("Length: " + respawnPoints.Length);
        transform.position = respawnPoints[Mathf.FloorToInt(Random.Range(0, respawnPoints.Length))].transform.position;
        colorChangeToUniform = true;
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
