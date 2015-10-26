using UnityEngine;
using System.Collections;
using InControl;

public class PuckMovement : MonoBehaviour {
	
	public KeyCode debugKill;
	public Color c1;
	private CapsuleCollider puckCollider;
	private Rigidbody rb;
	public bool inPlay;
	public GameObject respawnPoint;
	public InputDevice device {get; set;}
	public FencingWizard wizard;
	private float timeOfGoal;
	public float respawnDelay;
	private bool willRespawn;
	
	// Use this for initialization
	void Start () {
		inPlay = true;
		
		rb = GetComponent<Rigidbody> ();
		if (respawnPoint == null)
		{
			Debug.Log("There aren't any respawn points, you catastrophic dingus.");
		}
		willRespawn = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (willRespawn && timeOfGoal + respawnDelay < Time.time)
		{
			Respawn();
			willRespawn = false;
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (inPlay == true) {
			if (col.gameObject.CompareTag("Goal")) {
				Debug.Log ("Goal Scored");
				inPlay = false;
				timeOfGoal = Time.time;
				willRespawn = true;
				//Respawn();
			}
		}
	}

	private void GetRespawn()
	{
		if (Input.GetKeyDown(debugKill) || (device!= null && device.Command.WasPressed))
		{
				Respawn();
		} 
	}

	public void Respawn()
	{
		inPlay = true;
		rb.velocity = new Vector3(0, 0, 0);
		transform.position = respawnPoint.transform.position;
	}

	private void ResetRigidBodyConstraints() 
	{
		rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
		transform.rotation = Quaternion.identity;
	}
}
