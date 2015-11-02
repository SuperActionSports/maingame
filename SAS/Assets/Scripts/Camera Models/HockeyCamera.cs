using UnityEngine;
using System.Collections;

public class HockeyCamera : MonoBehaviour {
	
	private Vector3 originalPosition;
	public GameObject[] players;
	public GameObject puck;
	public Vector3[] positions;
	public float minCameraPos;
	public float maxCameraPos;
	public float yoffset;
	public float maxYOffset;
	public float minYOffset;
	public float zoffset;
	[Range(1,100)]
	public float debugLerp;
	Camera cam;
	
	public float length;
	public float magnitude;
	public bool shake;
	public float maxX;
	public float maxZ;
	public float distanceModifier = 2;
	Vector3 collectivePos = new Vector3();
	public Vector2 xExtrema;
	public Vector2 zExtrema;
	void Start () {
		players = GameObject.FindGameObjectsWithTag("Player");
		positions = new Vector3[players.Length];
		//offset = -18f;
		debugLerp = 5;
		cam = GetComponent<Camera>();
		shake = false;
		originalPosition = transform.position;
		xExtrema = new Vector2(-4,4);
		zExtrema = new Vector2(5,-5);
		maxYOffset = 27f;
		minYOffset = 8.5f;
	}
	
	void Update() {
		if (Input.GetKey (KeyCode.Space)) {
			shake = true;
		}
	}
	// Update is called once per frame
	void LateUpdate () {
		float maxDist = 0;
		
		
		// Collect current position of each player
		for (int i = 0; i < players.Length; i++) 
			{
			collectivePos += players[i].transform.position;
			for (int j = i; j < players.Length; j++)
				{
					if (Vector3.Distance(players[i].transform.position,players[j].transform.position) > maxDist)
						{
						maxDist = Vector3.Distance(players[i].transform.position,players[j].transform.position);
						}
					if (Vector3.Distance(puck.transform.position,players[j].transform.position) > maxDist)
					{
						maxDist = Vector3.Distance(puck.transform.position,players[j].transform.position);
					}
				}
			}
			collectivePos /= (players.Length * 2);
			collectivePos += puck.transform.position;
			collectivePos /= 2;
			
			//collectivePos /= 2;
			yoffset = maxDist;
			yoffset *= distanceModifier;	
			//collectivePos = new Vector3(Mathf.Clamp(collectivePos.x,xExtrema.x,xExtrema.y),0,Mathf.Clamp(collectivePos.z,zExtrema.x,zExtrema.y));

		yoffset = Mathf.Clamp(yoffset,minYOffset,maxYOffset);
		if (puck != null)
		{
			//transform.position = Vector3.Lerp(transform.position,new Vector3(puck.transform.position.x,yoffset,puck.transform.position.z),Time.deltaTime * debugLerp);
			transform.position = Vector3.Lerp(transform.position,new Vector3(collectivePos.x,yoffset,collectivePos.z),Time.deltaTime * debugLerp);
			//transform.position = Vector3.Lerp(transform.position,collectivePos,Time.deltaTime*debugLerp);
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position,originalPosition,Time.deltaTime * debugLerp);
		}
		/*if (transform.position.y > maxCameraPos) {
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
		}*/
	}
	
	public void SeePuck(GameObject newpuck)
	{
		Debug.Log("Before setting puck: " + puck);
		puck = newpuck;
		Debug.Log("After setting puck: " + puck);
	}
	
	public void FindPlayers()
	{
		players = GameObject.FindGameObjectsWithTag("Player");
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(collectivePos,1);
	}
}
