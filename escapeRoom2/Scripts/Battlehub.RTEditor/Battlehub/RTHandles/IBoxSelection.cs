using System;
using UnityEngine;

namespace Battlehub.RTHandles
{
	public interface IBoxSelection
	{
		bool IsDragging { get; }

		bool IsThresholdPassed { get; }

		Bounds SelectionBounds { get; }

		Canvas Canvas { get; }

		BoxSelectionMethod MethodOverride { get; set; }

		event EventHandler<BeginBoxSelectionArgs> Begin;

		event EventHandler<FilteringArgs> Filtering;

		event EventHandler<BoxSelectionArgs> Selection;
	}
}
