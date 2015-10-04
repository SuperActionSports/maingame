﻿using UnityEngine;
using System.Collections;

public class FencingCameraController : MonoBehaviour {
	
	public GameObject[] players;
	private Vector3[] positions;
	public float debugLerp;
	public float xOffset;
	public float yOffset;
	public float zOffset;
	public float zDebugMod;
	public float minimumZ;
	
	public float xWinOffset;
	public float yWinOffset;
	public float zWinOffset;
	private Vector3 winOffset;
	public bool win;
	
	//Camera Shake Variables
	Camera cam;
	public bool shake;
	public float shakeLength;
	public Vector2 shakeMagnitude;
	public float shakeIntensity;
	
	public float z;
	
	private Vector2 oldPosition;
	
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
		minimumZ = -14;
		yWinOffset = 1.76f;
		zWinOffset = -7.34f;
		winOffset = new Vector3(xWinOffset,yWinOffset,zWinOffset);
	}
	
	void Update() {
		if (Input.GetKey (KeyCode.Space)) {
			shake = true;
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		z = 0;
		if (players.Length > 1 && !win)
		{
			Vector2 averagePosition = GetAveragePositions();
			z = GetMaximumDistance();
			z /= -2.0f;
			if (z > minimumZ) 
			{
				z = minimumZ;
			}
			transform.position = Vector3.Lerp(transform.position,new Vector3(averagePosition.x+xOffset,averagePosition.y+yOffset,z),Time.deltaTime * debugLerp);
		}
		else
		{
			FollowWinner(players[0]);
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
	
	private float GetMaximumDistance()
	{
		float maxDist = 0;
		for (int i = 0; i < players.Length; i++) {
			for (int g = i; g < players.Length; g++)
			{
				if (Vector3.Distance(players[i].transform.position,players[g].transform.position) > maxDist)
				{
					maxDist = Vector3.Distance(players[i].transform.position,players[g].transform.position);
				}
			}
		}
		return maxDist;
	}
	
	private Vector2 GetAveragePositions()
	{
		float x = 0;
		float y = 0;
		Vector3[] positions = new Vector3[players.Length];
		for (int i = 0; i < players.Length; i++) {
			PlayerControllerMatt p = players[i].GetComponent<PlayerControllerMatt>();
			if (p.alive)
			{
				positions [i] = p.transform.position;
				x += players[i].transform.position.x;
				y += players[i].transform.position.y;
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
	
	public void FollowWinner(GameObject winner)
	{
		winOffset = new Vector3(xWinOffset,yWinOffset,zWinOffset);
		transform.position = (winner.transform.position + winOffset);
	}
}
