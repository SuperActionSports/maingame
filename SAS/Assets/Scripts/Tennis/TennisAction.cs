using UnityEngine;
using System.Collections;

public class TennisAction : MonoBehaviour {

	public GameObject tennisBall;
	Rigidbody rb;
	float playerRotation;

	// Use this for initialization
	void Start () {
		rb = tennisBall.GetComponent<Rigidbody> ();
		playerRotation = transform.rotation.y;
	}
	
	// Update is called once per frame
	void Update () {
		Serve ();
	}

	private void Serve()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			if(transform.position.z < 0)
			{
				transform.eulerAngles = new Vector3(0, 180, 0);
			}
			else if (transform.position.z > 0)
			{
				transform.eulerAngles = new Vector3(0, 0, 0);
			}
			Instantiate(tennisBall, transform.position + new Vector3(0, 2f, 0), transform.rotation);
		}
	}
}
