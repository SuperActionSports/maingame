using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScript : MonoBehaviour {

	public string nextLevelName;
	public Text loadingText;

	// Use this for initialization
	void Start () {
		loadingText = loadingText.GetComponent<UnityEngine.UI.Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		//The Application.GetStreamProgressForLevel() function return a float number between 0 and 1, you can use this to make a progress bar
		if(Application.GetStreamProgressForLevel(nextLevelName) ==1){
			Application.LoadLevel(nextLevelName);
		}
	}
}
