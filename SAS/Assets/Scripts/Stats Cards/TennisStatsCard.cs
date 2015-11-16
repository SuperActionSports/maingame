using UnityEngine;
using System.Collections;

public class TennisStatsCard : StatsCard {

	/*--------------------TENNIS--------------------*/
	public int Swings;
	public int Contact; //Swings that hit the ball not other players
	public float Accuracy;
	public int Points;
	
	public void HardResetStats() {
		ResetSwings ();
		ResetContact();
		ResetAccuracy ();
		ResetStats ();
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
	
	/*--------------------END TENNIS--------------------*/
}
