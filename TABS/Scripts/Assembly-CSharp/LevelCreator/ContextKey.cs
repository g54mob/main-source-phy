using System;
using UnityEngine;

namespace LevelCreator
{
	[Serializable]
	public class ContextKey
	{
		public string ActionName;

		public string BoldContextInfo;

		[TextArea]
		public string ContextInfo;

		public Sprite Image;

		public Sprite XBOXImage;

		public Sprite SwitchImage;
	}
}
