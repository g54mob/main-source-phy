using System;
using UnityEngine;

namespace Battlehub.RTHandles
{
	public interface IRuntimeSceneComponent : IRuntimeSelectionComponent, IScenePivot
	{
		RectTransform SceneGizmoTransform { get; }

		bool IsSceneGizmoEnabled { get; set; }

		SceneGizmo SceneGizmo { get; }

		[Obsolete("Use CanRotate instead")]
		bool CanOrbit { get; set; }

		bool CanRotate { get; set; }

		bool CanZoom { get; set; }

		float FreeRotationSmoothSpeed { get; set; }

		bool RotationInvertX { get; set; }

		bool RotationInvertY { get; set; }

		float FreeMovementSmoothSpeed { get; set; }

		[Obsolete]
		float ZoomSpeed { get; set; }

		bool ConstantZoomSpeed { get; set; }

		float ZoomSensitivity { get; set; }

		float MoveSensitivity { get; set; }

		float RotationSensitivity { get; set; }

		bool ChangeOrthographicSizeOnly { get; set; }

		bool CanPan { get; set; }

		bool CanFreeMove { get; set; }

		float BoundingSphereRadius { get; set; }

		float MinOrthoSize { get; set; }

		float MaxOrthoSize { get; set; }

		GameObject GameObject { get; }
	}
}
