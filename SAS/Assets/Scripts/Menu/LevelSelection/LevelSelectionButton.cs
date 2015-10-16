using System;
using UnityEngine;
using InControl;

	public class LevelSelectionButton : MonoBehaviour
	{
		Renderer cachedRenderer;

		public LevelSelectionButton up = null;
		public LevelSelectionButton down = null;
		public LevelSelectionButton left = null;
		public LevelSelectionButton right = null;


		void Start()
		{
			cachedRenderer = GetComponent<Renderer>();
		}


		void Update()
		{
			// Find out if we're the focused button.
			bool hasFocus = transform.parent.GetComponent<LevelSelectionButtonManager>().focusedButton == this;

			// Fade alpha in and out depending on focus.
			//var color = cachedRenderer.material.color;
			//color.a = Mathf.MoveTowards( color.a, hasFocus ? 1.0f : 0.5f, Time.deltaTime * 3.0f );
			//cachedRenderer.material.color = color;
		}
	}


