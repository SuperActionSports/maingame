using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {

	public int playerNumber;
	public int numOfPlayers;
	public float[] player_1_color;
	public float[] player_2_color;
	public float[] player_3_color;
	public float[] player_4_color;
	public float playerR;
	public float playerG;
	public float playerB;
	
	// Use this for initialization
	void Start () {
		playerNumber = 0;
		numOfPlayers = 0;
		player_1_color = new float[3];
		player_2_color = new float[3];
		player_3_color = new float[3];
		player_4_color = new float[3];

		numOfPlayers = PlayerPrefs.GetInt("numOfPlayers");

		player_1_color[0] = PlayerPrefs.GetFloat ("player_1_colorR");
		player_1_color[1] = PlayerPrefs.GetFloat ("player_1_colorG");
		player_1_color[2] = PlayerPrefs.GetFloat ("player_1_colorB");

		player_2_color[0] = PlayerPrefs.GetFloat ("player_2_colorR");
		player_2_color[1] = PlayerPrefs.GetFloat ("player_2_colorG");
		player_2_color[2] = PlayerPrefs.GetFloat ("player_2_colorB");

		player_3_color[0] = PlayerPrefs.GetFloat ("player_3_colorR");
		player_3_color[1] = PlayerPrefs.GetFloat ("player_3_colorG");
		player_3_color[2] = PlayerPrefs.GetFloat ("player_3_colorB");

		player_4_color[0] = PlayerPrefs.GetFloat ("player_4_colorR");
		player_4_color[1] = PlayerPrefs.GetFloat ("player_4_colorG");
		player_4_color[2] = PlayerPrefs.GetFloat ("player_4_colorB");

		playerR = PlayerPrefs.GetFloat ("player_1_colorR");
		playerG = PlayerPrefs.GetFloat ("player_1_colorG");
		playerB = PlayerPrefs.GetFloat ("player_1_colorB");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
