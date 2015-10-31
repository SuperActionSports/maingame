using UnityEngine;
using System.Collections;

public class BaseballStatsCard : StatsCard {

	/*--------------------BASEBALL--------------------*/
	public int HitsAttempted;
	public int HitsMade;
	//public int longestHit;
	public float BattingAverage;
	
	public void HardResetStats() {
		ResetHitsAttempted ();
		ResetHitsMade ();
		//ResetLongestHit ();
		ResetBattingAverage ();
		ResetStats ();
	}
	
	public void AddAttemptedHit () {
		HitsAttempted++;
		CalculateBattingAverage ();
		Debug.Log ("Added attempted hit. Total attempted hits: " + HitsAttempted);
	}
	
	public void ResetHitsAttempted() {
		HitsAttempted = 0;
	}
	
	public void AddMadeHit () {
		HitsMade++;
		AddAttemptedHit ();
		Debug.Log ("Added made hit. Total hits made: " + HitsMade);
	}
	
	public void ResetHitsMade() {
		HitsMade = 0;
	}
	
	public void CalculateBattingAverage()	{
		Debug.Log ("stats card before calculation: " + HitsMade + " / " + HitsAttempted);
		if (HitsAttempted != 0) {
			BattingAverage = (float)HitsMade / (float)HitsAttempted;
		}
		Debug.Log ("Current batting average: " + BattingAverage);
	}
	
	public void ResetBattingAverage()	{
		BattingAverage = 0;
	}
	
	/*
	public float LongestHit()
	{
		//Calculate
	}
	*/
	
	/*
	public float ResetLongestHit()
	{
		//Calculate
	}
	*/
	
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
	
	/*--------------------END BASEBALL--------------------*/
}
