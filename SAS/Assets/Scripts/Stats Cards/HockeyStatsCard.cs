using UnityEngine;
using System.Collections;

public class HockeyStatsCard : StatsCard {

	/*--------------------HOCKEY--------------------*/
	public int PuckPossession; //Increases Everytime the player touches the puck

	
	public void HardResetStats () {
		ResetPuckPossession ();
		ResetStats ();
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

	public void PrintStats() {
		Debug.Log ("Kills: " +kills+ " Deaths: " +deaths+ " Jumps: " +jumps+ " Attempted Attacks: " +attemptedAttacks+ 
		           " Kill Streak: " +killStreak+ " Longest Kill Streak: " +longestKillStreak+ " Longest Time Alive: " +longestTimeAlive+ 
		           " Shortest Time Alive: " +shortestTimeAlive+ " Kill/Death Ratio: " +kDRatio+ " Puck Possession: " +PuckPossession);
	}
	
	/*--------------------END HOCKEY--------------------*/
}
