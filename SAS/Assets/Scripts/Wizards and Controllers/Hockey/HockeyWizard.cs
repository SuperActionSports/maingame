using UnityEngine;
using System.Collections;

public class HockeyWizard : MonoBehaviour, IWizard {

	public GameObject playerPrefab; 		//Provided by game
	public Player[] players;
	private Vector3[] respawnPointPositions; // Game
	public GameObject[] respawnPoints;		// Game
	private int totalPlayers;				 // Layla
	private int remainingPlayers;			// Game 
	private int winner;						// Shared
	public GameObject winnerPlayer;			// Game, private
	public GameObject victory;				// Game, private
	private float gameStartTime;
	private float victoryDuration;			// Game, private
	private float gameWinTime = 10;				// Layla, customized games
	private float puckRespawnTime;
	private int matchCount;					// Layla, customized games
	private HockeyCamera cam;	// Game
	//private FencingPlayerManager inputManager;	// Layla
	//private InputDevice[] devices;				// Layla
	public GameObject layla;
	private GameControlLiaison liaison;
	
	public GameObject puck;
	public GameObject puckRespawn;
	
	private bool finished = false;
	public GameObject endGame;
	public GameObject inGame;

	// Use this for initialization
	void Start () {
		gameStartTime = Time.time;
		cam = Camera.main.GetComponent<HockeyCamera>();
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
		ResetExistingPlayers();
		SetPlayers();
		SpawnPuck();
		ResetPuck();
		victoryDuration = 3;
		
		//cam.FindPlayers();
		
		inGame.GetComponent<InGamePlayerBoard>().SetPlayers = players;
		UpdateStatCards();
	}
	
	void ResetExistingPlayers()
	{
		foreach (Player p in players)
		{
			p.control = null;
		}
	}
	
	void Update()
	{
		if ((Time.time > gameStartTime + gameWinTime) && !finished)
		{
			DisableMovement();
			for (int p = 0; p < players.Length; p++)
			{
				players[p].statCard = ((HockeyPlayerController)players[p].control).stats;
				
			}
			endGame.GetComponentInChildren<EndgameGUIStatGenerator>().SetPlayers = players;
			finished = true;
			inGame.GetComponent<InGamePlayerBoard>().KillInGameBoard();
		}
	}
	
	private void UpdateStatCards()
	{
		for (int p = 0; p < players.Length; p++)
		{
			players[p].statCard = ((HockeyPlayerController)players[p].control).Stats;
		}
	}
	
	private void SetPlayers()
	{
		// Set players should be SPAWN players. SpawnPlayers should reset the position of the players to one of the given respawn points, set them to active and alive, etc
		// This should handle the device concern too. 	
		switch(players.Length)
		{
			
		case (1):
			Debug.Log("Spawning one." + ShouldBeSpawned(players[0]));
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
		Debug.Log("Enable From Wizard");
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
	
	private HockeyPlayerController Spawn(Vector3 position, Player player)
	{
		GameObject p = Instantiate(playerPrefab,position,Quaternion.identity) as GameObject;
		p.transform.eulerAngles = new Vector3(0,0,0);
		player.gameObject = p;
		HockeyPlayerController pController = p.GetComponent<HockeyPlayerController>();
		player.control = pController;
		pController.color = player.color;
		pController.wizard = this;
		pController.device = player.device;
		pController.respawnPoint = position;
		pController.respawnTime = 1.5f;
		remainingPlayers++;
		pController.InitializeStatCard();
		return pController;
	}
	
	public void Celebrate(Color c)
	{
		
	}
	
	public void SpawnPuck()
	{
		puck = GameObject.Instantiate(puck,puckRespawn.transform.position,Quaternion.identity) as GameObject;
		puck.GetComponent<PuckMovement>().respawnDelay = 2.5f;
		puck.GetComponent<PuckMovement>().respawnPoint = puckRespawn;
		cam.SeePuck(puck);
	}
	
	public void ResetPuck()
	{
	}
	
	public void FindPlayers()
	{
		cam.FindPlayers();
	}
	
	// Update is called once per frame
}
