﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdvancedPlayerColorHueSelection2 : MonoBehaviour {

	public Slider player_slider;
	public Image splat;
	public float playerR;
	public float playerG;
	public float playerB;
	public float sliderVal;
	public int times;
	public float remainder;
	public int player_num;
	public int numOfPlayers;
	public float sliderPreSet;
	// Use this for initialization
	void Start () {
		player_slider.maxValue = 1785;
		player_slider.minValue = 0;
		player_slider.wholeNumbers = true;
		try {
			numOfPlayers = PlayerPrefs.GetInt ("numOfPlayers");
		}
		catch (Exception e) {
			Debug.Log("error loading numOfPlayers");
		}
		try {
			LoadPlayerPref();
		}
		catch (Exception e) {
			Debug.Log("error loading player colors");
		}
	}
	
	// Update is called once per frame
	void Update () {
		player_Color (player_slider.value);
		splat.color = new Color (playerR, playerG, playerB);
		sliderVal = (int)player_slider.value;
	
	}

	void player_Color(float slideNum){
		times = (int)(slideNum / 255f);
		remainder = slideNum % 255;

		switch (times) {
		case 0:
			if(remainder==0) {
				playerR = playerG = playerB = 0;
			}
			playerR = remainder/255f;
			playerG = 0;
			playerB = 0;
			break;
		case 1:
			playerR = 1;
			playerG = remainder/255f;
			playerB = 0;
			break;
		case 2:
			playerR = 1 - remainder/255f;
			playerG = 1;
			playerB = 0;
			break;
		case 3:
			playerR = 0;
			playerG = 1;
			playerB = remainder/255f;
			break;
		case 4:
			playerR = 0;
			playerG = 1 - remainder/255f;
			playerB = 1;
			break;
		case 5:
			playerR = remainder/255f;
			playerG = 0;
			playerB = 1;
			break;
		case 6:
			playerR = 1;
			playerG = 0;
			playerB = 1 - remainder/255f;
			break;
		case 7:
			playerR = playerG = playerB = 1;
			break;
		}
	}

	public void SavePlayerPref() {
		switch(player_num){
		case 1:
			PlayerPrefs.SetFloat("player_1_colorR",playerR);
			PlayerPrefs.SetFloat("player_1_colorG",playerG);
			PlayerPrefs.SetFloat("player_1_colorB",playerB);
			PlayerPrefs.SetFloat("player_1_color_SliderVal",sliderVal);
			break;
		case 2:
			PlayerPrefs.SetFloat("player_2_colorR",playerR);
			PlayerPrefs.SetFloat("player_2_colorG",playerG);
			PlayerPrefs.SetFloat("player_2_colorB",playerB);
			PlayerPrefs.SetFloat("player_2_color_SliderVal",sliderVal);
			break;
		case 3:
			PlayerPrefs.SetFloat("player_3_colorR",playerR);
			PlayerPrefs.SetFloat("player_3_colorG",playerG);
			PlayerPrefs.SetFloat("player_3_colorB",playerB);
			PlayerPrefs.SetFloat("player_3_color_SliderVal",sliderVal);
			break;
		case 4:
			PlayerPrefs.SetFloat("player_4_colorR",playerR);
			PlayerPrefs.SetFloat("player_4_colorG",playerG);
			PlayerPrefs.SetFloat("player_4_colorB",playerB);
			PlayerPrefs.SetFloat("player_4_color_SliderVal",sliderVal);
			break;
		}
	}

	public void LoadPlayerPref() {
		switch(player_num){
		case 1:
			sliderVal = PlayerPrefs.GetFloat("player_1_color_SliderVal");
			playerR = PlayerPrefs.GetFloat("player_1_colorR");
			playerG = PlayerPrefs.GetFloat("player_1_colorG");
			playerB = PlayerPrefs.GetFloat("player_1_colorB");
			break;
		case 2:
			sliderVal = PlayerPrefs.GetFloat("player_2_color_SliderVal");
			playerR = PlayerPrefs.GetFloat("player_2_colorR");
			playerG = PlayerPrefs.GetFloat("player_2_colorG");
			playerB = PlayerPrefs.GetFloat("player_2_colorB");
			break;
		case 3:
			sliderVal = PlayerPrefs.GetFloat("player_3_color_SliderVal");
			playerR = PlayerPrefs.GetFloat("player_3_colorR");
			playerG = PlayerPrefs.GetFloat("player_3_colorG");
			playerB = PlayerPrefs.GetFloat("player_3_colorB");
			break;
		case 4:
			sliderVal = PlayerPrefs.GetFloat("player_4_color_SliderVal");
			playerR = PlayerPrefs.GetFloat("player_4_colorR");
			playerG = PlayerPrefs.GetFloat("player_4_colorG");
			playerB = PlayerPrefs.GetFloat("player_4_colorB");
			break;
		}
		player_slider.value = sliderVal;
	}


}