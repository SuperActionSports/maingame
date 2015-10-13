using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using UnityEngine;
using System.IO;
using InControl;
using System;

public class GameControlWizard : MonoBehaviour {

	//Singlton design pattern
	public static GameControlWizard control;

	public float health;
	public float exp;

	public int number_of_players;
	public GameObject Player_Prefab;

	public float[] player_1_color;
	public float[] player_2_color;
	public float[] player_3_color;
	public float[] player_4_color;

	public InputDevice player_1_controller;
	public InputDevice player_2_controller;
	public InputDevice player_3_controller;
	public InputDevice player_4_controller;

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

	// Use this for code that will execute before Start ()
	void Awake () { 
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != this){
			Destroy(gameObject);
		} 
	}
	
	public void LoadFencing() {
		//Load Level
		Application.LoadLevel (fensing_filename);
		//Load Respawn Points
		GameObject.Find (""); //Insert Respond Points Text

	}

	public void LoadBaseball() {
		//Load Level
		Application.LoadLevel (baseball_filename);
		//Load Respawn Points
		GameObject.Find (""); //Insert Respond Points Text

	}

	public void LoadGolf() {
		//Load Level
		Application.LoadLevel (golf_filename);
		//Load Respawn Points
		GameObject.Find (""); //Insert Respond Points Text

	}

	public void LoadTennis() {
		//Load Level
		Application.LoadLevel (tennis_filename);
		//Load Respawn Points
		GameObject.Find (""); //Insert Respond Points Text

	}

	public void LoadHockey() {
		//Load Level
		Application.LoadLevel (hockey_filename);
		//Load Respawn Points
		GameObject.Find (""); //Insert Respond Points Text

	}

	public void SetPlayerColor(int player_number) {

	}

	public void SetAllPlayerColor() {
		
	}

	public float[] GetPlayerColor(int player_number) {
		return null;
	}

	public float[] GetAllPlayerColor() {
		return null;
	}



















	public void Save() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData ();
		data.health = health;
		data.exp = exp;

		bf.Serialize (file, data);
		file.Close ();
	}

	public void Load() {
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			health = data.health;
			exp = data.exp;
		}
	}
}

[Serializable]
class GameData {
	public float health;
	public float exp;
}

[Serializable]
class Player {
	public int number;
	public float[] color;
	public InputDevice controller;
	public Vector3[] respawn_points;
}
