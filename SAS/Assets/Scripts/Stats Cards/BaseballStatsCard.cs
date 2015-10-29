using UnityEngine;
using System.Collections;

public class BaseballStatsCard : StatsCard {

	/*--------------------BASEBALL--------------------*/
	public int HitsAttempted;
	public int HitsMade;
	//public int longestHit;
	public float ERA;
	
	public void HardResetStats() {
		ResetHitsAttempted ();
		ResetHitsMade ();
		//ResetLongestHit ();
		ResetERA ();
		ResetStats ();
	}
	
	public void AddAttemptedHit () {
		HitsAttempted++;
	}
	
	public void ResetHitsAttempted() {
		HitsAttempted = 0;
	}
	
	public void AddMadeHit () {
		HitsMade++;
		AddAttemptedHit ();
	}
	
	public void ResetHitsMade() {
		HitsMade = 0;
	}
	
	public void CalculateERA()	{
		if (HitsAttempted != 0) {
			ERA = HitsMade / HitsAttempted;
		}
	}
	
	public void ResetERA()	{
		ERA = 0;
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
