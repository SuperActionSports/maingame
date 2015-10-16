using System;
using UnityEngine;
using InControl;

	public class CreditsButton : MonoBehaviour
	{
		Renderer cachedRenderer;

		public CreditsButton up = null;
		public CreditsButton down = null;
		public CreditsButton left = null;
		public CreditsButton right = null;


		void Start()
		{
			cachedRenderer = GetComponent<Renderer>();
		}


		void Update()
		{
			// Find out if we're the focused button.
			bool hasFocus = transform.parent.GetComponent<CreditsButtonManager>().focusedButton == this;

			// Fade alpha in and out depending on focus.
			//var color = cachedRenderer.material.color;
			//color.a = Mathf.MoveTowards( color.a, hasFocus ? 1.0f : 0.5f, Time.deltaTime * 3.0f );
			//cachedRenderer.material.color = color;
		}
	}


