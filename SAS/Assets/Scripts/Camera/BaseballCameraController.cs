using UnityEngine;
using System.Collections;

public class BaseballCameraController : MonoBehaviour {
	
	private GameObject[] players;
	private Vector3[] positions;
	public float debugLerp;
	public float xOffset;
	public float yOffset;
	public float zOffset;
    public float zDebugMod;

	//Camera Shake Variables
	Camera cam;
	public bool shake;
	public float shakeLength;
	public Vector2 shakeMagnitude;
    public float shakeIntensity;

    public float z;

    private Vector2 oldPosition;

	void Start () {
		cam = GetComponent<Camera>();
		positions = new Vector3[6];
		debugLerp = 10;
		shake = false;
        shakeLength = 0.1f;
        shakeMagnitude = new Vector2(3f,3f);
        shakeIntensity = 50f;
        zOffset = 20f;
        zDebugMod = 30;
	}
	
	void Update() {
		if (Input.GetKey (KeyCode.Space)) {
			shake = true;
		}
		players = GameObject.FindGameObjectsWithTag("Player");
	}

	// Update is called once per frame
	void LateUpdate () {
		float y = 0;
		float x = 0;
		 z = 0;
		float maxDist = 0;
		
		// Collect current position of each player
		for (int i = 0; i < players.Length; i++) {
            BaseballPlayerController p = players[i].GetComponent<BaseballPlayerController>();
            if (p.alive)
            {
                GameObject t = players[i];
                positions[i] = t.transform.position;
            }
		}
		GameObject ball = GameObject.FindGameObjectWithTag("ball");
		if (ball != null) {
			positions [5] = ball.transform.position;
		} 
		else {
			positions [5] = new Vector3(0, 0, 0) ;
		}
			
			// Loop through every recorded position to find the average point among them on the X,Z plane
		for (int i = 0; i < positions.Length; i++) {
			Vector3 pos = positions [i];
			
			// Add position's X and Z to average
			x += pos.x;
			y += pos.y;
		}
		
		// Divide average position by player positions
		x /= players.Length;
		y /= players.Length;
		
		// Loop through every recorded position to find the furthest point from the average point on th X,Y plane
		for (int i = 0; i < positions.Length; i++) {
			Vector3 pos = positions [i];
			float dist = Mathf.Sqrt(Mathf.Pow(pos.x - x, 2)+Mathf.Pow((2*(pos.y - y)), 2));
			if (dist > maxDist) { maxDist = dist; }
		}

        // Use (x,y) position of camera and furthest player points to set- z distance.
        float zOffsetViaMath = (Mathf.Tan(Mathf.PI / 3) * (maxDist));
        if (zOffsetViaMath > zOffset)
        {
            z = -zOffsetViaMath;
        }
        else
        {
            z = -zOffset;
        }
		transform.position = Vector3.Lerp(transform.position,new Vector3(x+xOffset,y+yOffset,z),Time.deltaTime * debugLerp);

		// Camera shake code
		if (shake) {
			shake = false;
			PlayShake();
		}
	}

	public void PlayShake() {
        oldPosition = new Vector2(transform.position.x, transform.position.y);
		StopAllCoroutines();
		StartCoroutine("Shake");
	}

    public void PlayShake(Vector3 impact)
    {
        oldPosition = new Vector2(transform.position.x, transform.position.y);
        if (impact.x < 0 && shakeMagnitude.x > 0)
        {
            shakeMagnitude.x *= -1;
        }
        if (impact.x > 0 && shakeMagnitude.x < 0)
        {
            shakeMagnitude.x *= -1;
        }
        StopAllCoroutines();
        StartCoroutine("Shake");
    }

    IEnumerator Shake() {
		
		float duration = 0f;
		Vector2 xy = new Vector3(cam.transform.position.x,cam.transform.position.y);
		Vector3 camOrigin = Camera.main.transform.position;
		//Debug.Log("In Shake()");
		while (duration < shakeLength) {
			//Debug.Log("In Coroutine");
			duration += Time.deltaTime;
                  
            xy = new Vector2(Mathf.PerlinNoise(Time.time*shakeIntensity,0) * shakeMagnitude.x,Mathf.PerlinNoise(0,Time.time*shakeIntensity)*shakeMagnitude.y);
            xy += oldPosition;
            cam.transform.position = new Vector3(xy.x, xy.y, cam.transform.position.z);
			//Debug.Log("New Pos: " + xy.x + ", " + xy.y);
			//Debug.Log("Duration: " + duration + " -duration: " + -duration + " Perlin Out: " + Mathf.PerlinNoise(0,duration));
			yield return null;
		}
		
		cam.transform.position = Vector3.Lerp(cam.transform.position,camOrigin,Time.deltaTime * debugLerp);
		
	}
}
