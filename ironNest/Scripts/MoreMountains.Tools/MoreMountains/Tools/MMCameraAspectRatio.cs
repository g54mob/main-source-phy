using UnityEngine;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("More Mountains/Tools/Camera/MMCameraAspectRatio")]
	public class MMCameraAspectRatio : MonoBehaviour
	{
		public enum Modes
		{
			Fixed = 0,
			ScreenRatio = 1
		}

		[Header("Camera")]
		[Tooltip("the camera to change the aspect ratio on")]
		public Camera TargetCamera;

		[Tooltip("the mode of choice, fixed will force a specified ratio, while ScreenRatio will adapt the camera's aspect to the current screen ratio")]
		public Modes Mode;

		[Tooltip("in fixed mode, the ratio to apply to the camera")]
		[MMEnumCondition("Mode", new int[] { 0 })]
		public Vector2 FixedAspectRatio;

		[Header("Automation")]
		[Tooltip("whether or not to apply the ratio automatically on Start")]
		public bool ApplyAspectRatioOnStart;

		[Tooltip("whether or not to apply the ratio automatically on enable")]
		public bool ApplyAspectRatioOnEnable;

		[Header("Debug")]
		[MMInspectorButton("ApplyAspectRatio")]
		public bool ApplyAspectRatioButton;

		protected float _defaultAspect;

		protected virtual void OnEnable()
		{
		}

		protected virtual void Start()
		{
		}

		public virtual void ApplyAspectRatio()
		{
		}
	}
}
