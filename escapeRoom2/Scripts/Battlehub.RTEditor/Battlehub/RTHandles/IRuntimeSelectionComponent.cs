using System;
using Battlehub.RTCommon;
using UnityEngine;

namespace Battlehub.RTHandles
{
	public interface IRuntimeSelectionComponent : IScenePivot
	{
		PositionHandle PositionHandle { get; }

		RotationHandle RotationHandle { get; }

		ScaleHandle ScaleHandle { get; }

		RectTool RectTool { get; }

		BaseHandle CustomHandle { get; set; }

		BoxSelection BoxSelection { get; }

		bool IsPositionHandleEnabled { get; set; }

		bool IsRotationHandleEnabled { get; set; }

		bool IsScaleHandleEnabled { get; set; }

		bool IsRectToolEnabled { get; set; }

		bool IsBoxSelectionEnabled { get; set; }

		bool IsSelectionVisible { get; set; }

		bool CanSelect { get; set; }

		bool CanSelectAll { get; set; }

		float SizeOfGrid { get; set; }

		bool IsGridVisible { get; set; }

		bool IsGridEnabled { get; set; }

		bool GridZTest { get; set; }

		RuntimeWindow Window { get; }

		IRuntimeSelection Selection { get; set; }

		event EventHandler<RuntimeSelectionFilteringArgs> Filtering;

		event EventHandler<RuntimeSelectionChangingArgs> SelectionChanging;

		event EventHandler SelectionChanged;

		Transform[] GetHandleTargets();
	}
}
