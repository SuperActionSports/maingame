using UnityEngine;
using System.Collections;
using InControl;

public class HockeyTempPlayerController : MonoBehaviour {

	public Color c1;
	private bool colorChangeToUniform;
	private Renderer rend;
	private float colorLerpT;

	//Keyboard Keybinding Stuff
	public KeyCode left;
	public KeyCode right;
	public KeyCode up;
	public KeyCode down;
    public KeyCode debugKill;

	public bool alive;
	
	private Rigidbody rb;
	public float magSpeedX;
	public float magSpeedZ;
	public float momentumX;
	public float momentumZ;
	[Range(1,2000)]
	public float maxSpeed;
	[Range(0.7f,1.0f)]
	public float friction;
	public float floatAbove;
    public GameObject[] respawnPoints;

	[Range(1,2000)]
	public float speedMagnitude;

	public InputDevice device {get; set;}
	private PaintSplatter paint;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		maxSpeed = 200f;
		speedMagnitude = 100f;
		alive = true;
        respawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
		rend = GetComponent<Renderer>();
		colorChangeToUniform = false;
		colorLerpT = 0;
		paint = GetComponent<PaintSplatter>();
//		paint.color = c1;
        if (respawnPoints.Length == 0)
        {
            Debug.Log("There aren't any respawn points, you catastrophic dingus.");
        }
		transform.position = new Vector3(transform.position.x, 6.25f, transform.position.z);

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
			momentumX = (momentumX + xVel)*friction;
			momentumZ = (momentumZ + zVel)*friction;
			if(momentumX > maxSpeed) {
				momentumX = maxSpeed;
			}
			if (momentumX < (-1)*maxSpeed) {
				momentumX = (-1)*maxSpeed;
			}
			if(momentumX < 0.1f && momentumX > -0.1f) {
				momentumX = 0;
			}
			if(momentumZ > maxSpeed) {
				momentumZ = maxSpeed;
			}
			if (momentumZ < (-1)*maxSpeed) {
				momentumZ = (-1)*maxSpeed;
			}
			if(momentumZ < 0.1f && momentumZ > -0.1f) {
				momentumZ = 0;
			}
			Vector3 newPosition = new Vector3(momentumX,0,momentumZ);
			if (newPosition != new Vector3(0,0,0)) { transform.rotation = Quaternion.LookRotation(-newPosition); }
			transform.position = new Vector3(transform.position.x, floatAbove, transform.position.z);
			rb.velocity = newPosition;
		}	
		GetRespawn();
//		foreach (Transform child in transform)
//		{
//			child.transform.position = transform.parent.transform.position;
//		}
	}
	
	private void GetRespawn()
	{
		if (Input.GetKeyDown(debugKill))
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
        rb.constraints = RigidbodyConstraints.None;
    }
	
	public void Kill()
	{
		rb.AddForce(40, 25, 0, ForceMode.VelocityChange);
        MakeDead();
    }

    public void Kill(Vector3 direction)
    {
        MakeDead();    
    }

    public void Respawn()
    {
        alive = true;
        rb.velocity = new Vector3(0, 0, 0);
        transform.position = respawnPoints[Mathf.FloorToInt(Random.Range(0, respawnPoints.Length))].transform.position;
    }

	private float GetXVelocity() {
		return device == null ? GetKeyboardXInput(): GetControllerXInput();
	}
	
	private float GetZVelocity() {
		return device == null ? GetKeyboardZInput(): GetControllerZInput();
	}
	private float GetControllerXInput() {
		return speedMagnitude * device.Direction.X * Time.deltaTime;
	}
	private float GetControllerZInput() {
		return speedMagnitude * device.Direction.Y * Time.deltaTime;
	}
	private float GetKeyboardXInput() {
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
	private void UpdateColor() {
//		colorLerpT += Time.deltaTime;
//		if (colorChangeToUniform && alive)
//		{
//			rend.material.color = Color.Lerp(new Color(0, 0, 0, 0), c1, colorLerpT);
//			if (colorLerpT >= 1)
//			{
//				colorChangeToUniform = false;
//				colorLerpT = 0;
//			}
//		}
//		else
//		{
//			rend.material.color = Color.Lerp(c1, new Color(0, 0, 0, 0), colorLerpT);
//			if (colorLerpT >= 1)
//			{
//				if (alive)
//				{
//					colorChangeToUniform = true;
//					colorLerpT = 0;
//				}
//			}
//		}
		rend.material.color = c1;
	}
}
