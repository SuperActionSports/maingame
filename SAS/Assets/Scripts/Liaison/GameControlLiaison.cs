using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using UnityEngine;
using System.IO;
using InControl;
using System;

public class GameControlLiaison : MonoBehaviour {
	
	public static GameControlLiaison liaison;

	public Player[] players;

	public int number_of_players = 0;
	public int numberOfActivePlayers = 0;
	public GameObject Player_Prefab;
	public Boolean tournament_mode;
	public Boolean team_mode;

	public float[] player_1_color;
	public float[] player_2_color;
	public float[] player_3_color;
	public float[] player_4_color;
	public float[] home_team_color;
	public float[] away_team_color;
	
	public InputDevice player_1_controller;
	public InputDevice player_2_controller;
	public InputDevice player_3_controller;
	public InputDevice player_4_controller;

	public int player_1_team;
	public int player_2_team;
	public int player_3_team;
	public int player_4_team;

	public Vector3[] player_1_respawnpoints;
	public Vector3[] player_2_respawnpoints;
	public Vector3[] player_3_respawnpoints;
	public Vector3[] player_4_respawnpoints;

	public string fensing_filename;
	public string baseball_filename;
	public string golf_filename;
	public string tennis_filename;
	public string hockey_filename;

	public GameObject[] home_team;
	public GameObject[] away_team;
	public Vector3[] home_team_respawn_points;
	public Vector3[] away_team_respawn_points;

	public int player_1_score;
	public int player_2_score;
	public int player_3_score;
	public int player_4_score;
	public int home_team_score;
	public int away_team_score;

	public int player_1_tournament_score;
	public int player_2_tournament_score;
	public int player_3_tournament_score;
	public int player_4_tournament_score;
	public int home_team_tournament_score;
	public int away_team_tournament_score;

	public int winner;
	public float game_win_time;
	public int match_count;

	public IInputHandler input_manager;
	public InputDevice[] player_controllers;
	
	public InputDevice only;

	// Use this for code that will execute before Start ()
	void Awake () { 
		//Singlton design pattern
		if (liaison == null) {
			DontDestroyOnLoad (gameObject);
			liaison = this;
		} else if (liaison != this){
			Destroy(gameObject);
		} 
		Debug.Log("Layla awake");
	}
	
	void Start()
	{
		players = new Player[4];
		Debug.Log("Layla start");
		number_of_players = 0;
		if (players[0] != null)
		{
			Debug.Log(players[0].device.Name);
		}
	}
	
	public void CreatePlayer(InputDevice device)
	{
		//Debug.Log("Player at " + number_of_players + " is currently " + players[number_of_players]);
		Player p = new Player(device);
		//Debug.Log("P is " + p + " -- ");
		//p.device = device;
		players[number_of_players] = p;
		//players[number_of_players] = new Player();
		//Debug.Log("Is now " + players[number_of_players]);
		//players[number_of_players].device = device;
		//Debug.Log("And their device is " + players[number_of_players].device);
		//only = device;
		//Debug.Log("Player at " + number_of_players + " - " + players[number_of_players]);
		//Debug.Log("Player at " + number_of_players + "'s color: " + players[number_of_players].color);
		number_of_players++;
	
	}
	
	public bool AllPlayersActive()
	{
		return number_of_players == numberOfActivePlayers;
	}
	
	public void SetNumberOfPlayers(int num) 	{ number_of_players = num; }

	public int GetNumberOfPlayers() 			{ return number_of_players; }

	public void SetPlayerColor(int player_number, float[] player_color) {
		if (!team_mode && player_color.Length == 3 && player_number < 5 && player_number > 1) {
			switch (player_number) {
			case 1:
				player_1_color [0] = player_color [0];
				player_1_color [1] = player_color [1];
				player_1_color [2] = player_color [2];
				break;
			case 2:
				player_2_color [0] = player_color [0];
				player_2_color [1] = player_color [1];
				player_2_color [2] = player_color [2];
				break;
			case 3:
				player_3_color [0] = player_color [0];
				player_3_color [1] = player_color [1];
				player_3_color [2] = player_color [2];
				break;
			case 4:
				player_4_color [0] = player_color [0];
				player_4_color [1] = player_color [1];
				player_4_color [2] = player_color [2];
				break;
			}
		}
	}
	
	public void SetPlayerColor(InputDevice device, Color player_color) {
			if (FindPlayerWithDevice(device).color == new Color(0,0,0,0) )
			{
				numberOfActivePlayers++;
			}
			FindPlayerWithDevice(device).color = player_color;
			
		}
		
	public void ErasePlayerColor(InputDevice device)
	{
		if (FindPlayerWithDevice(device).color != new Color(0,0,0,0) )
		{
			numberOfActivePlayers--;
		}
		FindPlayerWithDevice(device).color = new Color(0,0,0,0);
		
	}
	
	public Player FindPlayerWithDevice(InputDevice device)
	{
		int x = 0;
		while (players[x].device != device)
		{
			x++;
		}
		return players[x];
	}
	

	public void SetAllPlayerColor(float[] player_color) {
		if (!team_mode && player_color.Length == 12) {
			if(number_of_players == 4) {
				player_4_color [0] = player_color [9];
				player_4_color [1] = player_color [10];
				player_4_color [2] = player_color [11];
			}
			if(number_of_players >= 3) {
				player_3_color [0] = player_color [6];
				player_3_color [1] = player_color [7];
				player_3_color [2] = player_color [8];
			}
			if(number_of_players >= 2) {
				player_2_color [0] = player_color [3];
				player_2_color [1] = player_color [4];
				player_2_color [2] = player_color [5];
				player_1_color [0] = player_color [0];
				player_1_color [1] = player_color [1];
				player_1_color [2] = player_color [2];
			}
		}
	}

	public float[] GetPlayerColor(int player_number) {
		float[] color = new float[3];
		switch (player_number) {
		case 1:
			color [0] = player_1_color [0];
			color [1] = player_1_color [1];
			color [2] = player_1_color [2];
			break;
		case 2:
			color [0] = player_2_color [0];
			color [1] = player_2_color [1];
			color [2] = player_2_color [2];
			break;
		case 3:
			color [0] = player_3_color [0];
			color [1] = player_3_color [1];
			color [2] = player_3_color [2];
			break;
		case 4:
			color [0] = player_4_color [0];
			color [1] = player_4_color [1];
			color [2] = player_4_color [2];
			break;
		}
		return color;
	}

	public float[] GetAllPlayerColor() {
		float[] color = new float[12];
		color [0] = player_1_color [0];
		color [1] = player_1_color [1];
		color [2] = player_1_color [2];
		color [3] = player_2_color [0];
		color [4] = player_2_color [1];
		color [5] = player_2_color [2];
		color [6] = player_3_color [0];
		color [7] = player_3_color [1];
		color [8] = player_3_color [2];
		color [9] = player_4_color [0];
		color [10] = player_4_color [1];
		color [11] = player_4_color [2];
		return color;
	}

	public void SetPlayerController(int player_number, InputDevice player_controller) {
		switch (player_number) {
		case 1:
			player_1_controller = player_controller;
			break;
		case 2:
			player_2_controller = player_controller;
			break;
		case 3:
			player_3_controller = player_controller;
			break;
		case 4:
			player_4_controller = player_controller;
			break;
		}
	}

	public void SetAllPlayerController(InputDevice[] player_controller) {
		if(number_of_players == 4) {
			player_4_controller = player_controller[3];
		}
		if(number_of_players >= 3) {
			player_3_controller = player_controller[2];
		}
		if(number_of_players >= 2) {
			player_2_controller = player_controller[1];
			player_1_controller = player_controller[0];
		}
	}

	public InputDevice GetPlayerController(int player_number) {
		switch (player_number) {
		case 1:
			return player_1_controller;
		case 2:
			return player_2_controller;
		case 3:
			return player_3_controller;
		case 4:
			return player_4_controller;
		default:
			return null;
		}
	}
	
	public InputDevice[] GetAllPlayerController() {
		InputDevice[] player_controller = new InputDevice[4];
		if(number_of_players == 4) {
			player_controller[3] = player_4_controller;
		}
		if(number_of_players >= 3) {
			player_controller[2] = player_3_controller;
		}
		if(number_of_players >= 2) {
			player_controller[1] = player_2_controller;
			player_controller[0] = player_1_controller;
		}
		return player_controller;
	}

	public void SetPlayerRespawnPoints(int player_number, Vector3[] respawn_points) {
		player_1_respawnpoints = respawn_points;
		switch (player_number) {
		case 1:
			player_1_respawnpoints = respawn_points;
			break;
		case 2:
			player_2_respawnpoints = respawn_points;
			break;
		case 3:
			player_3_respawnpoints = respawn_points;
			break;
		case 4:
			player_4_respawnpoints = respawn_points;
			break;
		}
	}
	
	public void SetAllPlayerRespawnPoints(Vector3[] respawn_points) {
		if(number_of_players == 4) {
			player_4_respawnpoints = respawn_points;
		}
		if(number_of_players >= 3) {
			player_3_respawnpoints = respawn_points;
		}
		if(number_of_players >= 2) {
			player_2_respawnpoints = respawn_points;
			player_1_respawnpoints = respawn_points;
		}
	}

	public void SetHomeTeamRespawnPoints(Vector3[] respawn_points) 	{ home_team_respawn_points = respawn_points; }
	
	public void SetAwayTeamRespawnPoints(Vector3[] respawn_points) 	{ away_team_respawn_points = respawn_points; }	

	public Vector3[] GetPlayerRespawnPoints(int player_number) {
		switch (player_number) {
		case 1:
			return player_1_respawnpoints;
		case 2:
			return player_2_respawnpoints;
		case 3:
			return player_3_respawnpoints;
		case 4:
			return player_4_respawnpoints;
		default:
			return null;
		}
	}
	
	//public Vector3[,] GetAllPlayerRespawnPoints() {}

	public Vector3[] GetHomeTeamRespawnPoints() 	{ return home_team_respawn_points; }

	public Vector3[] GetAwayTeamRespawnPoints() 	{ return away_team_respawn_points; }

	public void SetPlayerTeam(int player_number, int team_number) {
		// 0 - Home | 1 - nuetral/no team | 2 - Away
		if (team_number >= 0 && team_number < 3) {
			switch (player_number) {
			case 1:
				player_1_team = team_number;
				break;
			case 2:
				player_2_team = team_number;
				break;		
			case 3:
				player_3_team = team_number;
				break;		
			case 4:
				player_4_team = team_number;
				break;
			}
		}
	}

	public int GetPlayerTeam(int player_number) {
		// 0 - Home | 1 - nuetral/no team | 2 - Away
		switch (player_number) {
		case 1:
			return player_1_team;
		case 2:
			return player_2_team;
		case 3:
			return player_3_team;
		case 4:
			return player_4_team;
		default:
			return -1;
		}
	}

	public void SetTeamColor(int team, float[] team_color) {
		switch (team) {
		case 0:
			home_team_color = team_color;
			break;
		case 2:
			away_team_color = team_color;
			break;
		}
	}

	public float[] GetTeamColor(int team) {
			switch (team) {
			case 0:
				return home_team_color;
			case 2:
				return away_team_color;
			default:
				return null;
			}
		}

	public void SetWinner(int player_number)		{	winner = player_number;	}

	public int GetWinner()							{	return winner;	}

	public void SetGameWinTime(float gameWinTime)	{	game_win_time = gameWinTime;	}

	public float GetGameWinTime()					{	return game_win_time;	}

	public void SetMatchCount(int count)			{	match_count = count;	}

	public int GetMatchCount()						{	return match_count;	}


	public void Save() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData ();
		//data.health = health;
		//data.exp = exp;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load() {
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			//PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			//health = data.health;
			//exp = data.exp;
		}
	}
}

[Serializable]
public class Team {
	public int team;
	//public Player[] team_members;
	public float[] team_color;
	public Vector3[] respawn_points;
}

[Serializable]
public class Player
{
	public int number;
	public Color color;
	public InputDevice device;
	public Vector3[] respawn_points;
	public Team team;
	public IPlayerController control;
	public GameObject gameObject;
	public StatsCard statCard;
	
	public Player()
	{
		Debug.Log("New player, yo.");
	}
	
	public Player(InputDevice device)
	{
		this.device = device;
		Debug.Log(this + " is new. Device is " + this.device);
	}

	public void SetStatCard(StatsCard card) {

	}
}
