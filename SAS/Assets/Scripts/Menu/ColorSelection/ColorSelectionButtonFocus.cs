using UnityEngine;

	public class ColorSelectionButtonFocus : MonoBehaviour
	{

		void Start()
		{
			Debug.Log ("Position of focus: " + transform.localPosition);
		}
		void Update()
		{
			// Get focused button.
			GameObject colorSelection = GameObject.Find ("ColorSelectionManager(Clone)");

			if (colorSelection != null) {
				var focusedButton = colorSelection.GetComponent<ColorSelectionButtonManager>().focusedButton;

				// Move toward same position as focused button.

				transform.position = Vector3.MoveTowards( transform.position, focusedButton.transform.position, Time.deltaTime * 3000.0f );
			}
			

		}
	}

