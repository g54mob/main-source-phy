using System;
using UnityEngine;

namespace Battlehub.RTHandles
{
	public interface IScenePivot
	{
		Vector3 Pivot { get; set; }

		Vector3 SecondaryPivot { get; set; }

		Vector3 CameraPosition { get; set; }

		bool IsOrthographic { get; set; }

		float OrthographicSize { get; set; }

		[Obsolete]
		void Focus();

		void Focus(FocusMode mode = FocusMode.Selected);

		void Focus(Vector3 objPosition, float objSize);

		void SetCameraPositionAndPivot(Vector3 position, Vector3 pivot);
	}
}
