using UnityEngine;
using System.Collections;

public class PaintSplatterProjector : MonoBehaviour {


	public GameObject ball;
	public Color c;


	// Use this for initialization
	public void Splatter (Vector3 position, Vector3 direction) {

		int numberOfBalls = Random.Range (1, 4);

		for (int i = 0; i < numberOfBalls; i++) {
			GameObject obj = (GameObject) Instantiate (ball, position, Quaternion.identity);
			obj.SetActive (true);
			obj.GetComponent<Rigidbody>().AddForce (direction*100f);
			obj.GetComponent<Renderer>().material.color = c;
		}
	
	}
}
