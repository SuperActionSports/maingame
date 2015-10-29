using UnityEngine;
using System.Collections;

public class HockeyStatsCard : StatsCard {

	/*--------------------HOCKEY--------------------*/
	public float PuckPossession; //Increases Everytime the player touches the puck
	
	public void ResetStats () {
		ResetPuckPossession ();
	}
	
	public void AddPuckPossession() {
		PuckPossession++;
	}
	
	public void ResetPuckPossession() {
		PuckPossession = 0;
	}
	
	/*
	public float IndividualScoring()
	{
		//Calculate
	}
	*/
	
	/*
	public float TeamScoring()
	{
		//Calculate
	}
	*/
	
	
	/*--------------------END HOCKEY--------------------*/
}
