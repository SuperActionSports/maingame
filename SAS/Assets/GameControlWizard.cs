using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using UnityEngine;
using System.IO;
using System;

public class GameControlWizard : MonoBehaviour {

	//Singlton design pattern
	public static GameControlWizard control;

	public float health;
	public float exp;

	// Use this for code that will execute before Start ()
	void Awake () { 
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != this){
			Destroy(gameObject);
		} 
	}

	void onGUI () {
		GUI.Label (new Rect (10, 10, 100, 30), "Health: " + health);
		GUI.Label (new Rect (10, 40, 100, 30), "Exp: " + exp);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
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
class PlayerData {
	public float health;
	public float exp;
}
