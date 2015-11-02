using UnityEngine;
using InControl;

public class PauseMenuButtonManager : MonoBehaviour
{
	public PauseMenuButton focusedButton;

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
			Resume ();
		}

	}

	void LoadScene()
	{
		Debug.Log ("Btn: " + focusedButton.name);
		if (focusedButton.name == "Resume") 
		{
			Resume ();
		}
		if (focusedButton.name == "Level Select") 
		{
			Application.LoadLevel("LevelSelection");
		}
		if (focusedButton.name == "Quit") 
		{
			Application.Quit();
		}
	}

	void Resume()
	{
		GameObject menu = transform.parent.gameObject;
		menu.SetActive(false);
		Time.timeScale = 1.0f;
	}
	
		
	void MoveFocusTo( PauseMenuButton newFocusedButton )
	{
		if (newFocusedButton != null)
		{
			focusedButton = newFocusedButton;
		}
	}
}
