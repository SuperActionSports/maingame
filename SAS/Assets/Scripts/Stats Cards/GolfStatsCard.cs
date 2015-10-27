using UnityEngine;
using System.Collections;

public class GolfStatsCard : StatsCard {
	
	public int puttsAttempted;
	public int puttsMade;
	//public int longestPutt;
	public float accuracy;

	// Use this for initialization
	void Start () {
		puttsAttempted = 0;
		puttsMade = 0;
		//longestPutt = 0;
	}
	
	public void AddAttemptedPutt()	{
		puttsAttempted++;
		CalculateAccuracy ();
	}

	public void resetAttemptedPutts()	{
		puttsAttempted = 0;
	}

	public void AddMadePutt()	{
		puttsMade++;
		CalculateAccuracy ();
	}

	public void ResetMadePutts()	{
		puttsMade = 0;
	}

	public float CalculateAccuracy()	{
		if (puttsAttempted != 0) {
			accuracy = puttsMade / puttsAttempted;
		}
		return accuracy;
	}

	//Calculate Longest Putt
	
}
