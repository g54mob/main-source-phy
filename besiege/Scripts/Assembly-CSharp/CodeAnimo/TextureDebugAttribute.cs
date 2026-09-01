using System;
using UnityEngine;

namespace CodeAnimo
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
	public class TextureDebugAttribute : PropertyAttribute
	{
		public bool inputBox = true;

		public bool materialSelector;

		public bool openInViewerButton;

		public float previewWidth = 200f;
	}
}
