using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdvancedMainMenu : MonoBehaviour {
	
	public Button playButton;
	public Button quitButton;
	public Button creditsButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public void Play () {
		//Debug.Log ("Game is loading the Player Selection Screen");
		Application.LoadLevel ("AdvancedPlayerSelection");
	}

	public void Quit() {
		//Debug.Log ("Game is Quitting");
		Application.Quit ();
	}	

	public void Credits () {
		//Debug.Log ("Game is loading the Credits Screen");
		Application.LoadLevel ("AdvancedCredits");
	}
}
