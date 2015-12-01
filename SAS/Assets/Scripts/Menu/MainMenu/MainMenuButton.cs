using System;
using UnityEngine;
using InControl;

	public class MainMenuButton : MonoBehaviour
	{
		Renderer cachedRenderer;

		public MainMenuButton up = null;
		public MainMenuButton down = null;
		public MainMenuButton left = null;
		public MainMenuButton right = null;

		public string levelToLoad;

		void Start()
		{
			cachedRenderer = GetComponent<Renderer>();
		}


		void Update()
		{
			// Find out if we're the focused button.
			if (transform.parent.GetComponent<MainMenuButton>() != null)
			{
				bool hasFocus = transform.parent.GetComponent<MainMenuButtonManager>().focusedButton == this;
			}

			// Fade alpha in and out depending on focus.
			//var color = cachedRenderer.material.color;
			//color.a = Mathf.MoveTowards( color.a, hasFocus ? 1.0f : 0.5f, Time.deltaTime * 3.0f );
			//cachedRenderer.material.color = color;
		}
	}


