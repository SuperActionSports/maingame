using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndGameGUIStatistic : MonoBehaviour {

	public GameObject statNamePrefab;
	public GameObject statValuePrefab;
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
		statNamePrefab.GetComponent<Text>().text = statName;
		Debug.Log("Generating " + statName + " stat");
		Debug.Log("Stat value: (" + statValue + ")");
		Debug.Log(" statValuePrefab: (" + statValuePrefab + ")");
		statValuePrefab.GetComponent<Text>().text = statValue;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
