using UnityEngine;
using System.Collections;

public class PaintBall : MonoBehaviour {

	private float ballSize;
	public GameObject splatProjector;
	public Color color;

	// Use this for initialization
	void Start () {
		ballSize = Random.Range (0.3f, 0.8f);
		transform.localScale = new Vector3 (ballSize, ballSize, ballSize);
		GetComponent<Rigidbody> ().AddForce (0, 100f, 0);
		color = GetComponent<Renderer>().material.color;
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.CompareTag("field")) {
			ContactPoint hit = col.contacts [0];
			GameObject projector = (GameObject) Instantiate (splatProjector, transform.position, Quaternion.identity);
			projector.SetActive (true);
			projector.transform.LookAt (hit.point);
			projector.transform.localEulerAngles = new Vector3 (45, projector.transform.localEulerAngles.y, 90);
			Debug.Log ("Normal:" + hit.normal);
			projector.transform.position = hit.point + (hit.normal * ballSize*2.5f);
			projector.GetComponent<Projector>().material = GetComponent<Renderer>().material;
			Destroy (gameObject);
		}
	}
}