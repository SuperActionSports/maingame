using UnityEngine;
using System.Collections;

public class FencingCameraController : MonoBehaviour {
	
	public GameObject[] players;
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
	private float maxDist;
	
	void Start () {
		RecountPlayers();
		cam = GetComponent<Camera>();
		positions = new Vector3[players.Length];
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
	}
	
	// Update is called once per frame
	void LateUpdate () {
		z = 0;
		maxDist = 0;	
		if (players.Length > 1)
		{
			Vector2 averagePosition = GetAveragePositions();
			float zOffsetViaMath = (Mathf.Tan(Mathf.PI / 3) * (maxDist));
			z = zOffset < zOffsetViaMath ? -zOffset : -zOffsetViaMath;
			transform.position = Vector3.Lerp(transform.position,new Vector3(averagePosition.x+xOffset,averagePosition.y+yOffset,z),Time.deltaTime * debugLerp);
		}
		
		// Camera shake code
		if (shake) {
			shake = false;
			PlayShake();
		}
	}
	
	public void RecountPlayers()
	{
		Debug.Log("Player count before: " + players.Length);
		players = GameObject.FindGameObjectsWithTag("Player");
		Debug.Log("Player count after: " + players.Length);
	}
	
	private Vector2 GetAveragePositions()
	{
		float x = 0;
		float y = 0;
		Vector3[] positions = new Vector3[players.Length];
		
		Debug.Log("About to get average: " + players.Length);
		for (int i = 0; i < players.Length; i++) {
			Debug.Log ("I think there are this many players: "+players.Length);
			Debug.Log("Positions length: " + positions.Length);
			PlayerControllerMatt p = players[i].GetComponent<PlayerControllerMatt>();
			if (p.alive)
			{
				Debug.Log("I'm dealing with " + i);
				positions [i] = p.transform.position;
				x += players[i].transform.position.x;
				y += players[i].transform.position.y;
			}
			Debug.Log("I'm not dealing with " + i + " because he dead.");
		}
		for (int i = 0; i < positions.Length; i++) {
			Vector3 pos = positions [i];
			float dist = Mathf.Sqrt(Mathf.Pow(pos.x - x, 2)+Mathf.Pow((2*(pos.y - y)), 2));
			if (dist > maxDist) 
			{ 
				maxDist = dist; 
			}
		}
		
		x /= players.Length;
		y /= players.Length;
		
		
		return new Vector2(x,y);
	}
	
	public void PlayShake() {
		oldPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		StopAllCoroutines();
		StartCoroutine("Shake");
	}
	
	public void PlayShake(Vector3 impact)
	{
		oldPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
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
			yield return null;
		}
		
		cam.transform.position = Vector3.Lerp(cam.transform.position,oldPosition,Time.deltaTime * debugLerp);
		
	}
	
	void OnGizmosDraw()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z +zOffset),1);;
	}
}
