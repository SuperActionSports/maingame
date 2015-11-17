using UnityEngine;

	public class InfoScreenButtonFocus : MonoBehaviour
	{
		void Update()
		{
			// Get focused button.
			var focusedButton = transform.parent.GetComponent<InfoScreenButtonManager>().focusedButton;

//			Debug.Log ("Focus: " + focusedButton);
			// Move toward same position as focused button.
			transform.position = Vector3.MoveTowards( transform.position, focusedButton.transform.position, Time.deltaTime * 1000.0f );
		}
	}
