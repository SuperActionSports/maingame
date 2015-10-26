﻿using UnityEngine;
using System.Collections;

public class GolfWizard : MonoBehaviour,IWizard {
	
	public GameObject playerPrefab; 		//Provided by game
	public Player[] players;
	private Vector3[] respawnPointPositions; // Game
	public GameObject[] respawnPoints;		// Game
	private int totalPlayers;				 // Layla
	private int remainingPlayers;			// Game 
	private int winner;						// Shared
	public GameObject winnerPlayer;			// Game, private
	public GameObject victory;				// Game, private
	private float victoryDuration;			// Game, private
	private float gameWinTime;				// Layla, customized games
	private int matchCount;					// Layla, customized games
	private GolfCameraController cam;	// Game
	//private FencingPlayerManager inputManager;	// Layla
	//private InputDevice[] devices;				// Layla
	private GolfCameraController camScript;	// Game
	public GameObject layla;
	private GameControlLiaison liaison;
	
	public GameObject golfBall;
	public GameObject hole;
	
	public float holeSpawnRangeX = 16;
	public float holeSpawnRangeZ = 16;
	public float minDistToBallFromHole = 4;
	// Use this for initialization
	void Start () {
		if (layla == null) { layla = GameObject.Find("Layla");
		}
		liaison = layla.GetComponent<GameControlLiaison>();
		players = new Player[liaison.numberOfActivePlayers];
		for (int p = 0; p < players.Length; p++)
		{
			players[p] = liaison.players[p];
		}
		respawnPointPositions = new Vector3[respawnPoints.Length];
		for (int i = 0; i < respawnPoints.Length; i++)
		{
			respawnPointPositions[i] = respawnPoints[i].transform.position;
		}
		//players = liaison.players;
		Debug.Log("Wizard is setting " + players.Length +" players.");
		SetPlayers();
		SpawnBallAndHole();
		ResetBallAndHole();
		camScript = Camera.main.GetComponent<GolfCameraController>();
		gameWinTime = -1;
		victoryDuration = 3;
	}
	
	
	private void SetPlayers()
	{
		// Set players should be SPAWN players. SpawnPlayers should reset the position of the players to one of the given respawn points, set them to active and alive, etc
		// This should handle the device concern too. 	
		switch(players.Length)
		{
			
		case (1):
			if (ShouldBeSpawned(players[0])) Spawn(respawnPointPositions[0],players[0]);
			break;
		case(2):
			Debug.Log("Spawning two");
			if (ShouldBeSpawned(players[0])) Spawn(respawnPointPositions[0],players[0]);
			if (ShouldBeSpawned(players[1])) Spawn(respawnPointPositions[3],players[1]);
			break;
			
		case (3):
			if (ShouldBeSpawned(players[0])) Spawn(respawnPointPositions[0],players[0]);
			if (ShouldBeSpawned(players[1])) Spawn(respawnPointPositions[1],players[1]);
			if (ShouldBeSpawned(players[2])) Spawn(respawnPointPositions[2],players[2]);
			break;
			
		case(4):
			if (ShouldBeSpawned(players[0])) Spawn(respawnPointPositions[0],players[0]);
			if (ShouldBeSpawned(players[1])) Spawn(respawnPointPositions[1],players[1]);
			if (ShouldBeSpawned(players[2])) Spawn(respawnPointPositions[2],players[2]);
			if (ShouldBeSpawned(players[3])) Spawn(respawnPointPositions[3],players[3]);
			break;
			
		default:
			Debug.Log ("Invalid number of players [2,4]: " + players.Length);
			break;
		}
	}
	
	public void DisableMovement()
	{
		Debug.Log("Disable From Wizard");
		for (int i = 0; i < players.Length; i++)
		{
			players[i].control.MovementAllowed(false);
		}	
	}
	
	public void EnableMovement()
	{
		for (int i = 0; i < players.Length; i++)
		{
			players[i].control.MovementAllowed(true);
		}	
	}
	
	private bool ShouldBeSpawned(Player p)
	{
		if (p.control == null)
		{
			return true;	
		}
		else { return !p.control.Alive(); } 
	}
	
	private GolfPlayerController Spawn(Vector3 position, Player player)
	{
		GameObject p = Instantiate(playerPrefab,position,Quaternion.identity) as GameObject;
		player.gameObject = p;
		GolfPlayerController pController = p.GetComponent<GolfPlayerController>();
		player.control = pController;
		pController.color = player.color;
		pController.wizard = this;
		pController.device = player.device;
		pController.respawnPoint = position;
		pController.respawnTime = 1.5f;
		remainingPlayers++;
		return pController;
	}
	
	public void Celebrate(Color c)
	{
	
	}
	
	public void SpawnBallAndHole()
	{
		hole = GameObject.Instantiate(hole,Vector3.down,Quaternion.identity) as GameObject;
		golfBall = GameObject.Instantiate(golfBall,Vector3.down,Quaternion.identity) as GameObject;
		golfBall.GetComponent<GolfBall>().wizard = this;
	}
	
	public void ResetBallAndHole()
	{
		hole.transform.position = new Vector3(Random.Range (-holeSpawnRangeX,holeSpawnRangeX),transform.position.y,Random.Range (-holeSpawnRangeZ,holeSpawnRangeZ));
		golfBall.transform.position = new Vector3(Random.Range (-holeSpawnRangeX,holeSpawnRangeX),transform.position.y,Random.Range (-holeSpawnRangeZ,holeSpawnRangeZ));
		while(Vector3.Distance(hole.transform.position,golfBall.transform.position) < minDistToBallFromHole)
		{
			golfBall.transform.position = new Vector3(Random.Range (-holeSpawnRangeX,holeSpawnRangeX),transform.position.y,Random.Range (-holeSpawnRangeZ,holeSpawnRangeZ));
		}
		Debug.Log("Golfball: " + golfBall.transform.position);
		Debug.Log("Hole: " + hole.transform.position);
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireCube(transform.position,new Vector3(holeSpawnRangeX*2,1,holeSpawnRangeZ*2));
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(hole.transform.position,minDistToBallFromHole);
	}
	// Update is called once per frame
}
