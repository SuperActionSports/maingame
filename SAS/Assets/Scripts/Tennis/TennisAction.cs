using UnityEngine;
using System.Collections;

public class TennisAction : MonoBehaviour {

	public GameObject tennisBall;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = tennisBall.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		Serve ();
	}

	private void Serve()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			Instantiate(tennisBall, transform.position + new Vector3(0, 2f, 0), transform.rotation);
		}
	}
}
