using System;
using Battlehub.RTCommon;

namespace Battlehub.RTHandles
{
	public sealed class BoxSelectionRenderer : SelectionPicker
	{
		public BoxSelectionRenderer(RuntimeWindow window, Action<FilteringArgs> filterCallback = null)
			: base(window, filterCallback)
		{
		}
	}
}
