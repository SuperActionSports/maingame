using UnityEngine;

	public class ColorSelectionButtonFocus : MonoBehaviour
	{

		GameObject colorSelection;
		void Start()
		{
		//	Debug.Log ("Position of focus: " + transform.localPosition);
			colorSelection = transform.parent.gameObject;
		}
		void Update()
		{
			// Get focused button.
			//GameObject colorSelection = GameObject.Find ("ColorSelectionManager(Clone)");

			if (colorSelection != null) {
				var focusedButton = colorSelection.GetComponent<ColorSelectionButtonManager>().focusedButton;

				// Move toward same position as focused button.

				transform.localPosition = Vector3.MoveTowards( transform.localPosition, focusedButton.transform.localPosition, Time.deltaTime * 3000.0f );
			}
			

		}
	}

