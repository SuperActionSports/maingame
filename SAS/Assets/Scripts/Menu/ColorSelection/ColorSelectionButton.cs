using System;
using UnityEngine;
using InControl;

	public class ColorSelectionButton : MonoBehaviour
	{
		Renderer cachedRenderer;

		public ColorSelectionButton up = null;
		public ColorSelectionButton down = null;
		public ColorSelectionButton left = null;
		public ColorSelectionButton right = null;
		
		public bool focusedUponTheNightWhenTheHorsesAreFree = false;
		
		public bool isColor = true;
		
		public String levelToLoad;

		void Start()
		{
			cachedRenderer = GetComponent<Renderer>();
			focusedUponTheNightWhenTheHorsesAreFree = false;
		}


		void Update()
		{
			// Find out if we're the focused button.
			GameObject colorSelection = GameObject.Find ("ColorSelectionManager(Clone)");
			if (colorSelection != null) {
				bool hasFocus = colorSelection.GetComponent<ColorSelectionButtonManager> ().focusedButton == this;
			}
			// Fade alpha in and out depending on focus.
			//var color = cachedRenderer.material.color;
			//color.a = Mathf.MoveTowards( color.a, hasFocus ? 1.0f : 0.5f, Time.deltaTime * 3.0f );
			//cachedRenderer.material.color = color;
		}
	}


