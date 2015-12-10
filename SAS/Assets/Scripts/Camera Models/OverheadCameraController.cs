using UnityEngine;
using System.Collections;

public class OverheadCameraController : MonoBehaviour {

	public GameObject[] players;
	public Vector3[] positions;
	public float debugLerp;
	public float minDistance;
	public bool shake;
	public bool FlyingIn = true;

	void Start () {
		players = GameObject.FindGameObjectsWithTag("Player");
		positions = new Vector3[players.Length];
		debugLerp = 10;
		shake = false;
	}
	
	void Update() {
		if (Input.GetKey (KeyCode.Space)) {
			shake = true;
		}
	}
	// Update is called once per frame
	void LateUpdate () {
	if (!FlyingIn)
		{
			float y = 0;
			float x = 0;
			float z = 0;
			float maxDist = 0;
			
			// Collect current position of each player
			for (int i = 0; i < players.Length; i++) {
				GameObject t = players [i];
				positions [i] = t.transform.position;
			}
	
			// Loop through every recorded position to find the average point among them on the X,Z plane
			for (int i = 0; i < positions.Length; i++) {
				Vector3 pos = positions [i];
	
				// Add position's X and Z to average
				x += pos.x;
				z += pos.z;
			}
	
			// Divide average position by player positions
			x /= players.Length;
			z /= players.Length;
	
			// Loop through every recorded position to find the furthest point from the average point on th X,Z plane
			for (int i = 0; i < positions.Length; i++) {
				Vector3 pos = positions [i];
				float dist = Mathf.Sqrt(Mathf.Pow(pos.x - x, 2)+Mathf.Pow(pos.z-z, 2));
				if (dist > maxDist) { maxDist = dist; }
			}
	
			// Use (x,y) position of camera and furthest player points to set y distance.
			y = (Mathf.Tan (Mathf.PI / 3) * maxDist);
			if (y < minDistance) {
				y = minDistance;
			}
			transform.position = Vector3.Lerp(transform.position,new Vector3(x,y,z),Time.deltaTime * debugLerp);
		}
	}
}
