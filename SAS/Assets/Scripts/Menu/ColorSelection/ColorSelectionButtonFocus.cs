using UnityEngine;

	public class ColorSelectionButtonFocus : MonoBehaviour
	{
		void Update()
		{
			// Get focused button.
			var focusedButton = transform.parent.GetComponent<ColorSelectionButtonManager>().focusedButton;

			// Move toward same position as focused button.
			transform.position = Vector3.MoveTowards( transform.position, focusedButton.transform.position, Time.deltaTime * 2500.0f );
		}
	}

