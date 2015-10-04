using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {

	Vector3 position;
	Rigidbody rb;
	GameObject[] players;
	int count;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		players = GameObject.FindGameObjectsWithTag ("Player");
		Quaternion angle = Quaternion.AngleAxis(30.0f, Vector3.right);
		rb.AddForce(angle * -transform.forward * 2000);
		count = 0;
	}

	void Update()
	{
		foreach (GameObject player in players) 
		{
			//Physics.IgnoreCollision(transform.gameObject.GetComponent<Collider>(), player.GetComponent<Collider>());
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "BackWall") 
		{
			//Debug.Log ("Hit back wall");
			Destroy(transform.gameObject);
		}
		if (collision.gameObject.tag == "FrontWall") 
		{
			//Debug.Log ("Hit front wall");
			Destroy(transform.gameObject);
		}
		if (collision.gameObject.tag == "Turf") 
		{
			count++;
			if(count >= 10)
			{
				Destroy(transform.gameObject);
			}
		} else {
			//Debug.Log ("collision: " + collision.gameObject.tag);
		}
		if (collision.gameObject.tag == "Player") 
		{
			//Debug.Log ("Hit player");
			//Debug.Log("Hit " + collision.transform.GetChild(0).gameObject.name);
			//Quaternion angle = Quaternion.AngleAxis(30.0f, Vector3.right);
			//rb.AddForce(angle * transform.forward * 2000);
		}

	}
}
