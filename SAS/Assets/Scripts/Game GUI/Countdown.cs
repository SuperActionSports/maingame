using UnityEngine;
using System.Collections;

public class Countdown : MonoBehaviour {

	// Use this for initialization
	public IWizard wizard;
	void Start () {
	Debug.Log("Wizard: " + GameObject.FindGameObjectWithTag("Wizard"));
		wizard = GameObject.FindGameObjectWithTag("Wizard").GetComponent<IWizard>();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void StartPlay()
	{
		Debug.Log("Plee");
		if (wizard != null)
		{
			wizard.EnableMovement();
		}
	}
	
	public void NoPlay()
	{
		Debug.Log("No plee");
		if (wizard != null)
		{
			wizard.DisableMovement();
		}
	}
}
