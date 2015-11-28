using UnityEngine;
using System.Collections;

public class TennisWizard : MonoBehaviour, IWizard {

	public GameObject playerPrefab; 		//Provided by game
	public Player[] players;
	private Vector3[] respawnPointPositions; // Game
	public GameObject[] respawnPoints;		// Game
	private int totalPlayers;				 // Layla
	private int remainingPlayers;			// Game 
	private int matchCount;					// Layla, customized games
	public GameObject layla;
	private GameControlLiaison liaison;
	public GameObject inGame;
	public TennisBallLauncher launcher;
	public GameObject valueProjector;
	private BallValueCounter valueCounter;
	
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
	ResetExistingPlayers();
	SetPlayers();
	UpdateStatCards();
	inGame.GetComponent<InGamePlayerBoard>().SetPlayers = players;
	
	launcher.wizard = this;
	valueCounter = valueProjector.GetComponent<BallValueCounter>();
	}
	
	public void BallHitWall()
	{
		valueCounter.BallHitBrick();
	}
	
	public void BallHitTurfTwice()
	{
		valueCounter.Reset();
	}
	
	void ResetExistingPlayers()
	{
		foreach (Player p in players)
		{
			p.control = null;
		}
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
			Debug.Log("Spawning one");
			if (ShouldBeSpawned(players[0])) Spawn(respawnPointPositions[0],players[0]);
			Debug.Log("Spawning two");
			if (ShouldBeSpawned(players[1])) Spawn(respawnPointPositions[3],players[1]);
			break;
			
		case (3):
			if (ShouldBeSpawned(players[0])) Spawn(respawnPointPositions[0],players[0]);
			if (ShouldBeSpawned(players[1])) Spawn(respawnPointPositions[2],players[1]);
			if (ShouldBeSpawned(players[2])) Spawn(respawnPointPositions[3],players[2]);
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
		for (int i = 0; i < players.Length; i++)
		{
			Debug.Log("Disable movement of player " + i + " from Wizard");	
			players[i].control.MovementAllowed(false);
		}	
	}
	
	public void EnableMovement()
	{
		launcher.LaunchTennisBall();
		Debug.Log("Enable movement from Wizard");
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
	
	private TennisControllerGans Spawn(Vector3 position, Player player)
	{/*
		GameObject p = Instantiate(playerPrefab,position,Quaternion.identity) as GameObject;
		player.gameObject = p;
		TennisControllerGans pController = p.GetComponent<TennisControllerGans>();
		player.control = pController;
		pController.color = player.color;
		pController.wizard = this;
		pController.device = player.device;
		remainingPlayers++;
		pController.InitializeStatCard();
		*/
		GameObject p = Instantiate(playerPrefab,position,Quaternion.identity) as GameObject;
		player.gameObject = p;
		TennisControllerGans pController = p.GetComponent<TennisControllerGans>();
		player.control = pController;
		pController.color = player.color;
		pController.wizard = this;
		pController.respawnPoint = position;
		//pController.respawnPoint = position;
		pController.InitializeStatCard();
		p.GetComponent<TennisInputHandlerGans>().device = player.device;
		//UpdateStatCards();
		return pController;
	}
	
	private void UpdateStatCards()
	{
		for (int p = 0; p < players.Length; p++)
		{
			players[p].statCard = ((TennisControllerGans)players[p].control).stats;
		}
	}
	
	public void KillBall()
	{
		launcher.LaunchTennisBall();
	}
}