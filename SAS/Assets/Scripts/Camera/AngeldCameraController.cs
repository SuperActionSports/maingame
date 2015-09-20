using UnityEngine;
using System.Collections;

public class AngeldCameraController : MonoBehaviour {

	public GameObject[] players;
	public Vector3[] positions;
	public float minCameraPos;
	public float maxCameraPos;
	public float yoffset;
	public float zoffset;
	[Range(1,100)]
	public float debugLerp;
	Camera cam;

	public float length;
	public float magnitude;
	public bool shake;
	public float maxX;
	public float maxZ;
	
	void Start () {
		players = GameObject.FindGameObjectsWithTag("Player");
		positions = new Vector3[players.Length];
		//offset = -18f;
		debugLerp = 10;
		cam = GetComponent<Camera>();
		shake = false;
	}
	
	void Update() {
		if (Input.GetKey (KeyCode.Space)) {
			shake = true;
		}
	}
	// Update is called once per frame
	void LateUpdate () {
		float x = 0;
		float z = 0;
		float maxDistX = 0;
		float maxDistZ = 0;
		
		// Collect current position of each player
		for (int i = 0; i < players.Length; i++) {
			GameObject t = players [i];
			positions [i] = t.transform.position;
		}

		// Loop through every recorded position to find the average point among them on the X,Z plane
		for (int i = 0; i < positions.Length; i++) {

			// Reset position's X if beyond maximum extremes
			Vector3 pos = positions [i];
			if (pos.x > maxX) {
				pos.x = maxX;
			}
			else
				if (pos.x < -maxX) {
					pos.x = -maxX;
				}
			// Reset position's Z if beyond maximum extremes
			if (pos.z > maxZ) {
				pos.z = maxZ;
			}
			else if (pos.z < -maxZ) {
				pos.z = -maxZ;
			}

			// Add position's X and Z to average
			x += pos.x;
			z += pos.z;
			foreach (Vector3 pos2 in positions) {
				float xDist = Mathf.Abs (pos.x - pos2.x);
				float zDist = Mathf.Abs (pos.z - pos2.z);
				if (maxDistX < xDist) {
					maxDistX = xDist;
				}
				if (maxDistZ < zDist) {
					maxDistZ = zDist;
				}
			}
		}

		//Because this is a horizontal screened game, the camera needs to zoom out more when the y distance is greater than the x distance
		if (Mathf.Abs(maxDistX) > 2*maxDistZ) {
			if (maxDistX > 8f) { 
				yoffset = -maxDistX;
				zoffset = -maxDistX;
				}
			else {
				yoffset = -8f;
				zoffset = -8f;
			}
		}
		else {
			yoffset = -2*maxDistZ;
			zoffset = -2*maxDistZ;
		}

		x /= players.Length;
		z /= players.Length;
		yoffset -= 2;

		transform.position = Vector3.Lerp(transform.position,new Vector3(x,-yoffset,(yoffset*(transform.eulerAngles.x/90))+z),Time.deltaTime * debugLerp);
		if (transform.position.y > maxCameraPos) {
			transform.position = new Vector3(transform.position.x, maxCameraPos, transform.position.z);
		}
		if (transform.position.y < minCameraPos) {
			transform.position = new Vector3(transform.position.x, minCameraPos, transform.position.z);
		}
		if (transform.position.z > -(minCameraPos/2)) {
			transform.position = new Vector3(transform.position.x, transform.position.y, -minCameraPos/2);
		}
		if (transform.position.z < -maxCameraPos/2) {
			transform.position = new Vector3(transform.position.x, transform.position.y, -maxCameraPos/2);
		}
	}
}
