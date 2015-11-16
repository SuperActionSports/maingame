using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {

	Vector3 position;
	//Rigidbody rb;
	GameObject lastHitBy;
	int count;
	bool hasHitTurf;
	int value;
	public Vector3 vel;
	public bool hit;
	public GameObject wizard;

	private Renderer tr;
	// Use this for initialization
	void Start () 
	{
		ResetValue();
		tr = GetComponent<TrailRenderer>().GetComponent<Renderer>();
		//rb = GetComponent<Rigidbody> ();
		//Quaternion angle = Quaternion.AngleAxis(30.0f, Vector3.right);
		//rb.AddForce(angle * transform.forward * 1000);
		hasHitTurf = false;
		count = 0;
		hit = false;
	}

	void Update()
	{
		
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Turf") {
			count++;
			CheckForDoubleBounce();
		} else if (collision.gameObject.tag == "Wall") 
		{
		//	hasHitTurf = false;
		}
		else if (collision.gameObject.CompareTag("BrickWall"))
		{
			if (!hasHitTurf)
			{
				wizard.GetComponent<TennisWizard>().BallHitWall();
				IncreaseValue();
			}
		}
		else
		{
			//hasHitTurf = false;
		}
	}

	void CheckForDoubleBounce()
	{
		if (hasHitTurf) 
		{
			Debug.Log ("Double bounce");
			if (lastHitBy != null) lastHitBy.GetComponent<TennisController>().ScorePoints(value);
		} 
		else 
		{
			hasHitTurf = true;
		}
	}
	
	public void ResetValue()
	{
		value = 1;
	}
	
	public void IncreaseValue()
	{
		value = Mathf.Clamp(value + 1,1,10);
	}
	
	public void ResetCount()
	{
		count = 0;
	}
	
	public void Hit(GameObject hittee)
	{
		lastHitBy = hittee;
		tr.material.color = hittee.GetComponent<TennisController>().c1;
		GetComponent<TrailRenderer>().material.color = hittee.GetComponent<TennisController>().c1;
		
	}
}
