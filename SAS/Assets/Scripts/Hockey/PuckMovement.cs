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
	private HockeyPlayerController lastHit = null;
	public float friction = 1f;
	public GameObject goalEffect;
	
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
		rb.velocity *= friction;
		transform.position = Vector3.Lerp(transform.position,new Vector3(0,0,0),Time.deltaTime/60);
	}

	void OnTriggerEnter (Collider col)
	{
		if (inPlay == true) {
			if (col.gameObject.CompareTag("Goal")) {
				Debug.Log ("Goal Scored, last hit: " + lastHit);
				lastHit.GoalScored();
				inPlay = false;
				timeOfGoal = Time.time;
				willRespawn = true;
				rb.velocity *= 0.8f;
				StartGoalEffect();
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
	
	public void HitPuck(HockeyPlayerController p)
	{
		lastHit = p;
	} 
	
	private void StartGoalEffect()
	{
		GameObject p = Instantiate(goalEffect,transform.position,Quaternion.identity) as GameObject;
		p.GetComponent<HockeyGoalEffect>().PartyToDeath(lastHit.color);
	}
}
