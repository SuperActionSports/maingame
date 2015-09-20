using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class creditsScript : MonoBehaviour {

	public Button backButton;
	
	// Use this for initialization
	void Start () {
		backButton = backButton.GetComponent<Button> ();
	}

	public void MainMenu() {
		Application.LoadLevel ("TitleScreen");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
