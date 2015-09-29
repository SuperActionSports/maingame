using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdvancedCredits : MonoBehaviour {

	public Button back;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		back.onClick.AddListener(() => Back ());
	}

	void Back() {
		Debug.Log ("<<---Back--->>");
		Application.LoadLevel ("AdvancedMainMenu");
	}
}
