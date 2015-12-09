using UnityEngine;
using InControl;

public class InfoScreenButtonManager : MonoBehaviour
{
	public InfoScreenButton focusedButton;
	public string levelName;

	public MenuAudioManager audioManager;

	TwoAxisInputControl filteredDirection;
			
	void Awake()
	{
		filteredDirection = new TwoAxisInputControl();
		filteredDirection.StateThreshold = 0.5f;
	}
	
	void Start()
	{
		audioManager = GetComponentInChildren<MenuAudioManager>();
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
			audioManager.PlayMoveSelection();
		}
		
		if (filteredDirection.Down.WasPressed)
		{
			MoveFocusTo( focusedButton.down );
			audioManager.PlayMoveSelection();
		}
		
		if (filteredDirection.Left.WasPressed)
		{
			MoveFocusTo( focusedButton.left );
			audioManager.PlayMoveSelection();
		}
		
		if (filteredDirection.Right.WasPressed)
		{
			MoveFocusTo( focusedButton.right );
			audioManager.PlayMoveSelection();
		}
			
		if (inputDevice.Action1.WasPressed) 
		{
			LoadScene();
		}
		if (inputDevice.Action2.WasPressed) 
		{
			Application.LoadLevel ("LevelSelection");
		}
	}

	void LoadScene()
	{
		Debug.Log ("Btn: " + focusedButton.name);
		if (focusedButton.name == "Play") 
		{
			audioManager.PlayConfirm();
			Application.LoadLevel (levelName);
		}
		if (focusedButton.name == "Back") 
		{
			audioManager.PlayDecline();
			Application.LoadLevel("LevelSelection");
		}
	}
		
		
	void MoveFocusTo( InfoScreenButton newFocusedButton )
	{
		if (newFocusedButton != null)
		{
			focusedButton = newFocusedButton;
		}
	}
}
