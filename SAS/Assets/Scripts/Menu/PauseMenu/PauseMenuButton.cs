using System;
using UnityEngine;
using InControl;

	public class PauseMenuButton : MonoBehaviour
	{
		Renderer cachedRenderer;

		public PauseMenuButton up = null;
		public PauseMenuButton down = null;
		public PauseMenuButton left = null;
		public PauseMenuButton right = null;


		void Start()
		{
			cachedRenderer = GetComponent<Renderer>();
		}


		void Update()
		{
			// Find out if we're the focused button.
			if (transform.parent.GetComponent<PauseMenuButton>() != null)
			{
				bool hasFocus = transform.parent.GetComponent<PauseMenuButtonManager>().focusedButton == this;
			}

			// Fade alpha in and out depending on focus.
			//var color = cachedRenderer.material.color;
			//color.a = Mathf.MoveTowards( color.a, hasFocus ? 1.0f : 0.5f, Time.deltaTime * 3.0f );
			//cachedRenderer.material.color = color;
		}
	}


