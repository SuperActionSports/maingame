﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdvancedLevelSelection : MonoBehaviour {

	public Button level_1;
	public Button level_2;
	public Button level_3;
	public Button level_4;
	public Button level_5;
	public Button back;
	public string level_1_string;
	public string level_2_string;
	public string level_3_string;
	public string level_4_string;
	public string level_5_string;

	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public void LoadLevel_1() {
		Debug.Log ("Game is loading level 1");
		//Application.LoadLevel (level_1_string);
	}

	public void LoadLevel_2() {
		Debug.Log ("Game is loading level 2");
		//Application.LoadLevel (level_2_string);
	}

	public void LoadLevel_3() {
		Debug.Log ("Game is loading level 3");
		//Application.LoadLevel (level_3_string);
	}

	public void LoadLevel_4() {
		Debug.Log ("Game is loading level 4");
		//Application.LoadLevel (level_4_string);
	}

	public void LoadLevel_5() {
		Debug.Log ("Game is loading level 5");
		//Application.LoadLevel (level_5_string);
	}

	public void Back() {
		//Debug.Log ("<<---Back--->>");
		Application.LoadLevel ("AdvancedPlayerColorSelection");
	}
}
