using System;
using System.Collections.Generic;
using UnityEngine;

namespace MalbersAnimations
{
	public class Readme : ScriptableObject
	{
		[Serializable]
		public class Section
		{
			public string heading;

			public string text;

			public string linkText;

			public string url;

			public UnityEngine.Object reference;
		}

		public Texture2D icon;

		public string title;

		public Section[] sections;

		public List<string> packages = new List<string> { "com.unity.cinemachine", "com.unity.mathematics" };
	}
}
