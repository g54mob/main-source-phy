using System;

namespace Poly.UI
{
	[Serializable]
	public struct InspectorButton
	{
		[NonSerialized]
		public string text;

		[NonSerialized]
		public Action action;

		public InspectorButton(string label, Action action)
		{
			text = label;
			this.action = action;
		}
	}
}
