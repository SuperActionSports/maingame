using UnityEngine;

	public class LevelSelectionButtonFocus : MonoBehaviour
	{
		void Update()
		{
			// Get focused button.
			var focusedButton = GameObject.Find("Manager").GetComponent<LevelSelectionButtonManager>().focusedButton;

			// Move toward same position as focused button.
			transform.position = Vector3.MoveTowards( transform.position, focusedButton.transform.position, Time.deltaTime * 2500.0f );
		}
	}

