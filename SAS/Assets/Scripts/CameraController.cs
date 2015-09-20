using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	public GameObject[] players;
	public float zoffset;
	[Range(1,100)]
	public float debugLerp;
	Camera cam;
	public float minZOffset;
	
	public float length;
	public float magnitude;
	public bool shake;
	
	void Start () {
		players = GameObject.FindGameObjectsWithTag("Player");
		//offset = -18f;
		debugLerp = 10;
		cam = GetComponent<Camera>();
		shake = false;
		//minZOffset = 18;
	}
	
	void Update() {
		if (Input.GetKey (KeyCode.Space)) {
			shake = true;
		}
	}
	// Update is called once per frame
	void LateUpdate () {
		float x = 0;
		float y = 0;
		float maxDistX = 0;
		float maxDistY = 0;
		
		//Get the positions of all the players and determine the average of their x and y coordinates
		//Then find the maximum distance in x and y
		foreach(GameObject t in players) {
			x += t.transform.position.x;
			y += t.transform.position.y;
			foreach (GameObject d in players) {
				float xDist = Mathf.Abs(t.transform.position.x - d.transform.position.x);
				float yDist = Mathf.Abs(t.transform.position.y - d.transform.position.y);
				if (maxDistX < xDist) {
					maxDistX = xDist;
				}
				if (maxDistY < yDist) {
					maxDistY = yDist;
				}
			}			
		}

		x /= players.Length;
		y /= players.Length;
<<<<<<< HEAD
		transform.position = Vector3.Lerp(transform.position,new Vector3(x,y,zoffset),Time.deltaTime * debugLerp);

=======
		//y += 5;
		transform.position = Vector3.Lerp(transform.position,new Vector3(x,y+2.5f,zoffset),Time.deltaTime * debugLerp);
		//transform.position = new Vector3(0f,5.5f,-26f);
>>>>>>> PlayerMovement
		if (shake) {
			shake = false;
			length = 0.15f;
			magnitude = 3f;
			PlayShake();
		}
	}
	
	public void PlayShake()
	{
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
