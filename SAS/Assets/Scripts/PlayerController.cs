using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Color c1;
	private Color c2;
	private bool complete;
	private Renderer rend;
	private float colorLerpT;
	
	public KeyCode left;
	public KeyCode right;
	public KeyCode up;
	public KeyCode down;
	
	public bool alive;
	
	private Rigidbody rb;
	private RaycastHit groundHit;
	
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
	}
	
	// Update is called once per frame
	void Update () {
	if (alive) 
	{
			Physics.Raycast(transform.position, Vector3.down,out groundHit, 1.1f);
	Vector3 speed = new Vector3(0,0,0);
		if (Input.GetKey(left)) {
			speed.x -= speedMagnitude;
		}
		if (Input.GetKey(right)) {
			speed.x += speedMagnitude;
		}
		if (Input.GetKey(up) && groundHit.collider.CompareTag("Stage")) {
			speed.y += speedMagnitude;
		}
		if (Input.GetKey(down)) {
			speed.y -= speedMagnitude;
		}
		//transform.position += speed*Time.deltaTime;
		rb.AddForce(new Vector3(speed.x, 0,0), ForceMode.Force);
		rb.AddForce (new Vector3(0,speed.y, 0), ForceMode.VelocityChange);
		
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
			if (groundHit.collider.CompareTag("Stage"))
			{
				//transform.position = new Vector3(transform.position.x, transform.position.y +1, transform.position.z);
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
	
}
