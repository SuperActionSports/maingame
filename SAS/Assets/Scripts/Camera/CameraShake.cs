using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
	
	//Camera Shake Variables
	Camera cam;
	public bool shake;
	public float length;
	public float magnitude;
	public float debugLerp;
	
	void Start () {
		cam = GetComponent<Camera>();
		shake = false;
	}
	
	void Update() {
		if (Input.GetKey (KeyCode.Space)) {
			shake = true;
		}
	}

	void LateUpdate () {
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
			//Debug.Log("Duration: " + duration + " -duration: " + -duration + " Perlin Out: " + Mathf.PerlinNoise(0,duration));
			yield return null;
		}
		
		cam.transform.position = Vector3.Lerp(cam.transform.position,camOrigin,Time.deltaTime * debugLerp);
		
	}
}
