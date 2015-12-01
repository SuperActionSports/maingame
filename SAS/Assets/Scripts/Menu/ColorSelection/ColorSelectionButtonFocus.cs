using UnityEngine;
using UnityEngine.UI;
	public class ColorSelectionButtonFocus : MonoBehaviour
	{
		GameObject colorSelection;
		public Text text;
		
		void Start()
		{
		//	Debug.Log ("Position of focus: " + transform.localPosition);
			colorSelection = transform.parent.gameObject;
			text = GetComponent<Text>();
		}
		void Update()
		{
			// Get focused button.
			//GameObject colorSelection = GameObject.Find ("ColorSelectionManager(Clone)");

			if (colorSelection != null) {
				var focusedButton = colorSelection.GetComponent<ColorSelectionButtonManager>().focusedButton;

				// Move toward same position as focused button.

				//transform.localPosition = Vector3.MoveTowards( transform.localPosition, focusedButton.transform.localPosition, Time.deltaTime * 3000.0f );
				transform.position = Vector3.MoveTowards( transform.position, new Vector3(focusedButton.transform.position.x, focusedButton.transform.position.y - 140, focusedButton.transform.position.z), Time.deltaTime * 3000.0f );
			}
			

		}
	}

