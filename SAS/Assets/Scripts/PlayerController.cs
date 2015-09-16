using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Color c1;
	private Color c2;
	private bool complete;
	private Renderer rend;
	private float colorLerpT;
    private bool facingLeft;
	
	public KeyCode left;
	public KeyCode right;
	public KeyCode up;
	public KeyCode down;
	
	public bool alive;
	
	private Rigidbody rb;
	public RaycastHit groundHit;
	public float magSpeedX;
	public float magSpeedY;
    public float debugYMod;
	public Vector3 speed;
    private bool doubleJumpAllowed;
	
	[Range(1,20)]
	public float speedMagnitude;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		rb = GetComponent<Rigidbody>();
		//rend.material.color = c;
		speedMagnitude = 10f;
		complete = false;
		colorLerpT = 0;
		alive = true;
		ResetRigidBodyConstraints();
        doubleJumpAllowed = true;
    }
    
	
	// Update is called once per frame
	void Update () {
	if (alive) 
	{
            magSpeedX = 0;
            magSpeedY = 0;
            float doubleJumpGravityModifier = 1;
		if (Input.GetKey(left)) {
                magSpeedX = -1;
		}
		if (Input.GetKey(right)) {
                magSpeedX = 1;
		}
		if (Input.GetKeyDown(up))
        {
                if (doubleJumpAllowed)
                {
                    rb.velocity = new Vector3(rb.velocity.x,speedMagnitude*4f,rb.velocity.z);
                    doubleJumpAllowed = false;
                    doubleJumpGravityModifier = 0;
                }
				else if (Physics.Raycast(transform.position, Vector3.down, out groundHit, 1.1f))
                {
                	if (groundHit.collider.CompareTag("Stage")) {
                    	doubleJumpAllowed = true;
                    	rb.velocity = new Vector3(rb.velocity.x, speedMagnitude*4f, rb.velocity.z);
                    	doubleJumpGravityModifier = 1;
                    }
                }
        }
		
		if (Input.GetKey(down)) {
                rb.velocity -= new Vector3 (0, speedMagnitude * Time.deltaTime, 0);
		}
            //transform.position += speed*Time.deltaTime;
            float xSpeed = speedMagnitude * magSpeedX;
            //float ySpeed = 4 * (Physics.gravity.y * doubleJumpGravityModifier * Time.deltaTime);
            //ySpeed += speedMagnitude * magSpeedY * 3;
            speed = new Vector3(xSpeed, 0, 0);
            transform.position = transform.position + new Vector3(speed.x * Time.deltaTime, 0, 0);
            //rb.AddForce(new Vector3(0, speed.y, 0), ForceMode.VelocityChange);
        if (speed.x < 0)
            {
                transform.rotation = new Quaternion(transform.rotation.x, 180f, transform.rotation.z, transform.rotation.w);
            }
        else if (speed.x > 0 && transform.rotation.y > 0)
            {
                transform.rotation =  new Quaternion(transform.rotation.x, 0f, transform.rotation.z, transform.rotation.w);
            }
        
		
		// This can be cut whenever, it changes the colors of the players for easier identification
		colorLerpT += Time.deltaTime;
		if (complete) {
			rend.material.color = Color.Lerp(c2,c1,colorLerpT);
			if (colorLerpT >= 1) {
				complete = false;
				colorLerpT = 0;
					}
			}
			else {
			rend.material.color = Color.Lerp(c1,c2,colorLerpT);
			if (colorLerpT >= 1) {
				complete = true;
				colorLerpT = 0;
				}
			}
		}
		else { //Dead
			rb.constraints = RigidbodyConstraints.None;
		}
		
		if (Input.GetKey(KeyCode.X))
		{
			alive = !alive;
			if (alive)
				ResetRigidBodyConstraints();
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
	
}
