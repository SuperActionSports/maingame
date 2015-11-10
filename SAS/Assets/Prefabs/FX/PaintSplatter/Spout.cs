using UnityEngine;
using System.Collections;

public class Spout : MonoBehaviour {

	public GameObject ball;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			GameObject paint = (GameObject) Instantiate (ball, new Vector3(transform.position.x,3,transform.position.z), Quaternion.identity);
			paint.SetActive(true);
		}
	}
}
