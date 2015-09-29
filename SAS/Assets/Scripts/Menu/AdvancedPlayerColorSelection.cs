using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdvancedPlayerColorSelection : MonoBehaviour {


	public Button skip;
	public Button back;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		skip.onClick.AddListener(() => LoadLevelSelection ());
		back.onClick.AddListener(() => Back ());
	}

	void LoadLevelSelection() {
		Debug.Log ("Game is loading the Level Selection Screen");
		Application.LoadLevel ("AdvancedLevelSelection");
	}

	void Back() {
		Debug.Log ("<<---Back--->>");
		Application.LoadLevel ("AdvancedPlayerSelection");
	}
}
