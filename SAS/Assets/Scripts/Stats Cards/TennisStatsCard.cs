﻿using UnityEngine;
using System.Collections;

public class TennisStatsCard : StatsCard {

	/*--------------------TENNIS--------------------*/
	public int Swings;
	public int Contact; //Swings that hit the ball not other players
	public float Accuracy;
	public int Points;
	private int random;
	private float ts;
	
	public TennisStatsCard()
	{
		ts = 0;
		HardResetStats();
		random = (int)(Random.Range (0f, 4.999f));
	}
	
	public void Start() {
		random = (int)(Random.Range (0.000f, 4.999f));
	}

	public void HardResetStats() {
		//Debug.Log("Resetting stats in stat card");
		ResetSwings ();
		ResetContact();
		ResetAccuracy ();
		ResetStats ();
		ResetPoints();
	}
	
	public void ResetPoints()
	{
	Debug.Log("Points reset");
		Points = 1;
	}
	
	public void AddSwing()	{
		Swings++;
	}
	
	public void ResetSwings()	{
		Swings = 0;
	}
	
	public void AddContact()	{
		Contact++;
		AddSwing ();
	}
	
	public void ResetContact()	{
		Contact = 0;
	}
	
	public void CalculateAccuracy()	{
		if (Swings != 0) {
			Accuracy = (float)Contact / (float)Swings;
		}
	}
	
	public void ResetAccuracy()	{
		Accuracy = 0;
	}
	
	public void Score(int pts)
	{
		Points += pts;
		ts = Points;
		Debug.Log("Points is now " + Points + " ts is " + ts + "*********************************");
	}	
	
	/*--------------------END TENNIS--------------------*/
	public override float TotalScore() {
		//Debug.Log("At the beginning of Total Score, Points is " + Points);
		float Pw = 0.5f;
		float KDRw = 0.32f;
		float KSw = 0.1f;
		float ACCw = 0.05f;
		float RANw = 0.03f;
		
		
		float RAN;
		switch (random) {
		case 0:
			RAN = jumps;
			break;
		case 1:
			RAN = attemptedAttacks;
			break;
		case 2: 
			RAN = attackSuccessRate;
			break;
		case 3:
			RAN = longestTimeAlive;
			break;
		case 4:
			RAN = shortestTimeAlive;
			break;
		default:
			RAN = 0;
			break;
		}
		
		float tsP = Points * Pw;
		float tsKDR = kDR * KDRw;
		float tsKS = longestKillStreak * KSw;
		float tsACC = Accuracy * ACCw;
		float tsRAN = RAN * RANw;
		
		//Debug.Log("Tennis score: " + tsP + "(" + Points + " * " + Pw  + ") + " + tsKDR + " + " + tsKS + " + " + tsACC + " + " + tsRAN);
		// ts = tsP + tsKDR + tsKS + tsACC + tsRAN;
		//ts = Points;
		Debug.Log("For the in game gui, I return " + ts);
		return ts;
	}
}
