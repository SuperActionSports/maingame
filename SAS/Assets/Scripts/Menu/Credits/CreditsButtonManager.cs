using UnityEngine;
using InControl;

public class CreditsButtonManager : MonoBehaviour
{
	public CreditsButton focusedButton;

	TwoAxisInputControl filteredDirection;
			
	void Awake()
	{
		filteredDirection = new TwoAxisInputControl();
		filteredDirection.StateThreshold = 0.5f;
	}

	void Update()
	{
		// Use last device which provided input.
		var inputDevice = InputManager.ActiveDevice;
		filteredDirection.Filter( inputDevice.Direction, Time.deltaTime );
		// filteredDirection = inputDevice.Direction;

		if (filteredDirection.Left.WasRepeated)
		{
			Debug.Log( "!!!" );
		}
			
		if (inputDevice.Action1.WasPressed || inputDevice.Action2.WasPressed) 
		{
			LoadScene();
		}
	}

	void LoadScene()
	{
		Debug.Log ("Btn: " + focusedButton.name);
		if (focusedButton.name == "Back") 
		{
			Application.LoadLevel ("MainMenu");
		}
	}
		
		
	void MoveFocusTo( CreditsButton newFocusedButton )
	{
		if (newFocusedButton != null)
		{
			focusedButton = newFocusedButton;
		}
	}
}
