using UnityEngine;
using System.Collections;

public class PlayerActivation : MonoBehaviour {

	public int numOfPlayers;
	public GameObject Player1;
	public GameObject Player2;
	public GameObject Player3;
	public GameObject Player4;


	// Use this for initialization
	void Start () {
		//numOfPlayers = PlayerPrefs.GetInt ("numOfPlayers");
		numOfPlayers = 2;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (numOfPlayers == 2) {
			Player1.SetActive(true);
			Player2.SetActive(true);
			Player3.SetActive(false);
			Player4.SetActive(false);
		} else if (numOfPlayers == 3) {
			Player1.SetActive(true);
			Player2.SetActive(true);
			Player3.SetActive(true);
			Player4.SetActive(false);
		} else if (numOfPlayers == 4) {
			Player1.SetActive(true);
			Player2.SetActive(true);
			Player3.SetActive(true);
			Player4.SetActive(true);
		} 
	}


}
