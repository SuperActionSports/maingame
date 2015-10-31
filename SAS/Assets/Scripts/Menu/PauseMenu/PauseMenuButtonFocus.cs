using UnityEngine;

	public class PauseMenuButtonFocus : MonoBehaviour
	{

		void Start()
		{

		}

		void Update()
		{
			// Get focused button.
			var focusedButton = GameObject.Find ("Manager").GetComponent<PauseMenuButtonManager> ().focusedButton;

//			Debug.Log ("Focus: " + focusedButton);
			// Move toward same position as focused button.
			transform.position = Vector3.MoveTowards( transform.position, focusedButton.transform.position, Time.unscaledDeltaTime * 1000.0f );
		}
	}

