﻿using UnityEngine;
using System.Collections;

public class GolfStatsCard : StatsCard {
	
	/*--------------------GOLF--------------------*/
	public int PuttsAttempted;
	public int PuttsMade;
	//public float longestPutt;
	//public float ShortestPutt;
	public float Accuracy;
	
	public void ResetStats() {
		ResetAttemptedPutts ();
		ResetMadePutts ();
		//ResetLongestPutt();
		//ResetShortestPutt();
		ResetAccuracy ();
	}
	
	public void AddAttemptedPutt()	{
		PuttsAttempted++;
		CalculateAccuracy ();
	}
	
	public void ResetAttemptedPutts()	{
		PuttsAttempted = 0;
	}
	
	public void AddMadePutt()	{
		PuttsMade++;
		AddAttemptedPutt ();
	}
	
	public void ResetMadePutts()	{
		PuttsMade = 0;
	}
	
	public void CalculateAccuracy()	{
		if (PuttsAttempted != 0) {
			Accuracy = PuttsMade / PuttsAttempted;
		}
	}
	
	public void ResetAccuracy()	{
		Accuracy = 0;
	}
	
	/*
	public float LongestPutt()
	{
		//Calculate
	}
	*/
	
	/*
	public float ResetLongestPutt()
	{
		//Calculate
	}
	*/
	
	/*
	public float ShortestPutt()
	{
		//Calculate
	}
	*/
	
	/*
	public float ResetShortestPutt()
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
	
	/*--------------------END GOLF--------------------*/
	
}
