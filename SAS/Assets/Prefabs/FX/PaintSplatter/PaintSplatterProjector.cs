using UnityEngine;
using System.Collections;

public class PaintSplatterProjector : MonoBehaviour {

	private Material projectorMat;
	public GameObject ball;
	public Color c;
	
	// Use this for initialization
	public void Initialize (Color color) {
		projectorMat = (Material) Instantiate (ball.GetComponent<PaintBall>().splatProjector.GetComponent<Projector> ().material);
		projectorMat.color = color;
		c = color;
	}
	
	public void Splatter (Vector3 position, Vector3 direction) {

		int numberOfBalls = 1; // Random.Range (1, 4);

		for (int i = 0; i < numberOfBalls; i++) {
			GameObject obj = (GameObject) Instantiate (ball, position, Quaternion.identity);
			obj.SetActive (true);
			obj.GetComponent<Rigidbody>().AddForce (direction*100f);
			obj.GetComponent<Renderer>().material = projectorMat;
			obj.GetComponent<Renderer>().enabled = false;
		}
	
	}

}
