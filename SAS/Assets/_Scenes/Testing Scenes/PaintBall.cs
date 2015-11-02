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
		this.GetComponent<Rigidbody> ().AddForce (new Vector3 (Random.Range (-500f, 500f), 100f, Random.Range (-500f, 500f)));
		color = gameObject.GetComponent<Renderer>().material.color;
	}

	void OnCollisionEnter(Collision col) {
		ContactPoint hit = col.contacts [0];
		GameObject projector = (GameObject) Instantiate (splatProjector, transform.position, Quaternion.identity);
		projector.SetActive (true);
		projector.transform.LookAt (hit.point);
		projector.transform.localEulerAngles = new Vector3 (45, projector.transform.localEulerAngles.y, 90);
		Debug.Log ("Normal:" + hit.normal);
		projector.transform.position = hit.point + (hit.normal * ballSize*2.5f);
		projector.GetComponent<Projector> ().material.color = color;
		Destroy (gameObject);
	}
}
