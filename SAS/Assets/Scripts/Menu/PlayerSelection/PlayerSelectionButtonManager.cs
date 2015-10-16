using UnityEngine;
using InControl;

public class PlayerSelectionButtonManager : MonoBehaviour
{
	public PlayerSelectionButton focusedButton;

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

		// Move focus with directional inputs.
		if (filteredDirection.Up.WasPressed)
		{
			MoveFocusTo( focusedButton.up );
		}
			
		if (filteredDirection.Down.WasPressed)
		{
			MoveFocusTo( focusedButton.down );
		}
			
		if (filteredDirection.Left.WasPressed)
		{
			MoveFocusTo( focusedButton.left );
		}
			
		if (filteredDirection.Right.WasPressed)
		{
			MoveFocusTo( focusedButton.right );
		}
			
		if (inputDevice.Action1.WasPressed) 
		{
			LoadScene();
		}
		if (inputDevice.Action2.WasPressed) 
		{
			Application.LoadLevel("AdvancedMainMenuControl");
		}
	}

	void LoadScene()
	{
		Debug.Log ("Btn: " + focusedButton.name);
		if (focusedButton.name == "2 Players") 
		{
			PlayerPrefs.SetInt ("numOfPlayers", 2);
			Application.LoadLevel ("AdvancedPlayerColorSelection");
		}
		if (focusedButton.name == "3 Players") 
		{
			PlayerPrefs.SetInt ("numOfPlayers", 3);
			Application.LoadLevel ("AdvancedPlayerColorSelection");
		}
		if (focusedButton.name == "4 Players") 
		{
			PlayerPrefs.SetInt ("numOfPlayers", 4);
			Application.LoadLevel ("AdvancedPlayerColorSelection");
		}
		if (focusedButton.name == "Back") 
		{
			Application.LoadLevel ("AdvancedMainMenuControl");
		}
	}
		
		
	void MoveFocusTo( PlayerSelectionButton newFocusedButton )
	{
		if (newFocusedButton != null)
		{
			focusedButton = newFocusedButton;
		}
	}
}
