using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGamePlayerCard : MonoBehaviour {

	public GameObject namePrefab;
	public GameObject valuePrefab;
	private string statName;
	public string Name
	{
		get { return statName; }
		set { statName = value; }
	}
	private string statValue;
	public string Value
	{
		get { return statValue; }
		set { statValue = value; }
	}
	
	/// <summary> 
	/// Sets the name and value of the statistic to the corresponding value in the statistic game object
	/// </summary>
	public void GenerateStatistic()
	{
		namePrefab.GetComponent<Text>().text = statName;
		valuePrefab.GetComponent<Text>().text = statValue;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
