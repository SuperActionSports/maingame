using UnityEngine;
using System.Collections;

public class TennisStatsCard : StatsCard {

	/*--------------------TENNIS--------------------*/
	public int Swings;
	public int Contact; //Swings that hit the ball not other players
	public float Accuracy;
	
	public void ResetStats() {
		ResetSwings ();
		ResetContact();
		ResetAccuracy ();
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
			Accuracy = Contact / Swings;
		}
	}
	
	public void ResetAccuracy()	{
		Accuracy = 0;
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
