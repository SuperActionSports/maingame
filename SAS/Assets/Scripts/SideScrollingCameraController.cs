using UnityEngine;
using System.Collections;

public class SideScrollingCameraController : MonoBehaviour {
	
	public GameObject[] players;
	public Vector3[] positions;
	public float debugLerp;
	public float xOffset;
	public float yOffset;
	public float zOffset;

	//Camera Shake Variables
	Camera cam;
	public bool shake;
	public float length;
	public float magnitude;
	
	void Start () {
		players = GameObject.FindGameObjectsWithTag("Player");
		cam = GetComponent<Camera>();
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
			y += pos.z;
		}
		
		// Divide average position by player positions
		x /= players.Length;
		y /= players.Length;
		
		// Loop through every recorded position to find the furthest point from the average point on th X,Z plane
		for (int i = 0; i < positions.Length; i++) {
			Vector3 pos = positions [i];
			float dist = Mathf.Sqrt(Mathf.Pow(pos.x - x, 2)+Mathf.Pow((2*(pos.y - y)), 2));
			if (dist > maxDist) { maxDist = dist; }
		}
		
		// Use (x,y) position of camera and furthest player points to set- z distance.
		z = (Mathf.Tan (Mathf.PI / 3) * maxDist);
		transform.position = Vector3.Lerp(transform.position,new Vector3(x+xOffset,y+yOffset,-z-zOffset),Time.deltaTime * debugLerp);

		// Camera shake code
		if (shake) {
			shake = false;
			PlayShake();
		}
	}

	public void PlayShake() {
		StopAllCoroutines();
		StartCoroutine("Shake");
	}	

	IEnumerator Shake() {
		
		float duration = 0f;
		Vector2 xy = new Vector3(cam.transform.position.x,cam.transform.position.y);
		Vector3 camOrigin = Camera.main.transform.position;
		//Debug.Log("In Shake()");
		while (duration < length) {
			//Debug.Log("In Coroutine");
			duration += Time.deltaTime;
			
			//xy = new Vector2 (Mathf.PerlinNoise(duration,0)*magnitude,Mathf.PerlinNoise(0,duration)*magnitude);
			xy = new Vector2(Mathf.PerlinNoise(Random.Range(-magnitude,magnitude),Random.Range(-magnitude,magnitude)),Mathf.PerlinNoise(Random.Range(-magnitude,magnitude),Random.Range(-magnitude,magnitude)));
			cam.transform.position = new Vector3(xy.x, xy.y, cam.transform.position.z);
			//Debug.Log("New Pos: " + xy.x + ", " + xy.y);
			Debug.Log("Duration: " + duration + " -duration: " + -duration + " Perlin Out: " + Mathf.PerlinNoise(0,duration));
			yield return null;
		}
		
		cam.transform.position = Vector3.Lerp(cam.transform.position,camOrigin,Time.deltaTime * debugLerp);
		
	}
}
