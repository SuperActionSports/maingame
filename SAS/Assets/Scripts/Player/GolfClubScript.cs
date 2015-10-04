using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GolfClubScript : MonoBehaviour {

	private int count;
	
	// Use this for initialization
	void Start () {
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Golf club hit detected");
		Debug.Log("I have hit " + other.gameObject.tag);
		if (other.CompareTag("Player"))
		{
			GolfPlayerController victim = other.GetComponent<GolfPlayerController>();
			if (victim.alive)
			{
				Debug.Log("Hit detected, sending "+ (transform.right*-1));
				victim.Kill(new Vector3 (transform.position.x * -1, transform.position.y,transform.position.z));
				//This causes no movement at the center of the field
				count++;
				//SetScoreText();
			}
		}
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		Vector3 sweet = new Vector3(transform.position.x, transform.localPosition.y, transform.position.z);
		Gizmos.DrawLine(sweet, transform.forward * 1.5f);
	}
}
