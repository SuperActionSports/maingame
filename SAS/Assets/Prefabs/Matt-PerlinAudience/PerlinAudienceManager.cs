using UnityEngine;
using System.Collections;
using System;

public class PerlinAudienceManager : MonoBehaviour {

	PerlinAudienceController[] members;
	public GameObject audienceMember;
	private float audienceMemberWidth;
	public int audienceCount;
	
	void Start () {
		audienceMemberWidth = audienceMember.transform.lossyScale.x;
		for (int i = 0; i < audienceCount; i++)
		{
			Vector3 position = new Vector3(transform.position.x + (audienceMemberWidth * i) + (i/10f),transform.position.y,transform.position.z);
			GameObject a = Instantiate(audienceMember,position,transform.rotation) as GameObject;
			a.transform.parent = this.transform;
		}
		members = GetComponentsInChildren<PerlinAudienceController>();
	}
	
	void Update () {
		/*
		if (Input.GetKeyDown(KeyCode.S))
		{
			SendSmallEvent();
		}	
		if (Input.GetKeyDown(KeyCode.B))
		{
			SendBigEvent();
		}

		if (Input.GetKeyDown(KeyCode.G))
		{
			Color[] c = {Color.blue,Color.green,Color.cyan};
			int[] s = {12,40,14};
			ChangeCrowdColor(c,s);
		}
		*/
	}
	
	public void SendSmallEvent()
	{
		foreach (PerlinAudienceController p in members)
		{
			p.SmallEvent();
		}
	}
	
	public void SendBigEvent()
	{
		foreach (PerlinAudienceController p in members)
		{
			p.BigEvent();
		}
	}
	
	public void ChangeCrowdColor(Color[] teamColors, int[] scores)
	{	
		bool[] visitedAudience = new bool[audienceCount];
		float totalScore = 0;  
		foreach (int i in scores)
		{
			totalScore += i;
		} 
		float[]crowdWeights = new float[teamColors.Length]; 
		for (int w = 0; w < scores.Length; w++)
		{
			crowdWeights[w] = (scores[w]/totalScore) * audienceCount;
		}
		int p = 0;
		for (int c = 0; c < scores.Length ; c++)
		{
			Array.Sort(crowdWeights,teamColors);
			while(p < crowdWeights[c])
			{
				int pick = (int)UnityEngine.Random.Range(0,members.Length);
				if (!visitedAudience[pick])
				{
					PerlinAudienceController a = members[pick];
					a.ChangeColor(teamColors[c]);
					visitedAudience[pick] = true;	
					p++;
				}
			}
		}
		for (int va = 0; va < visitedAudience.Length; va++)
		{
			if (!visitedAudience[va])
			{
				members[va].ChangeColor(teamColors[UnityEngine.Random.Range(0, teamColors.Length)]);
			}
		}
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Vector3 center = new Vector3((((audienceMemberWidth + .1f) * audienceCount)/2) - audienceMemberWidth/2f,transform.position.y,transform.position.z);
		Vector3 width = new Vector3(((audienceMemberWidth + .1f) * audienceCount),1,1);
		Gizmos.DrawWireCube(center,width);
	}
}
