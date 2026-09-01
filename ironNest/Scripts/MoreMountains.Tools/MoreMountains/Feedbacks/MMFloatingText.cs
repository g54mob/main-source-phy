using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	public class MMFloatingText : MonoBehaviour
	{
		[Header("Bindings")]
		[Tooltip("the part of the prefab that we'll move")]
		public Transform MovingPart;

		[Tooltip("the part of the prefab that we'll rotate to face the target camera")]
		public Transform Billboard;

		[Tooltip("the TextMesh used to display the value")]
		public TextMesh TargetTextMesh;

		[Header("Debug")]
		[Tooltip("the direction of this floating text, used for debug only")]
		[MMReadOnly]
		public Vector3 Direction;

		protected bool _useUnscaledTime;

		protected float _startedAt;

		protected float _lifetime;

		protected Vector3 _newPosition;

		protected Color _initialTextColor;

		protected bool _animateMovement;

		protected bool _animateX;

		protected AnimationCurve _animateXCurve;

		protected float _remapXZero;

		protected float _remapXOne;

		protected bool _animateY;

		protected AnimationCurve _animateYCurve;

		protected float _remapYZero;

		protected float _remapYOne;

		protected bool _animateZ;

		protected AnimationCurve _animateZCurve;

		protected float _remapZZero;

		protected float _remapZOne;

		protected MMFloatingTextSpawner.AlignmentModes _alignmentMode;

		protected Vector3 _fixedAlignment;

		protected Vector3 _movementDirection;

		protected Vector3 _movingPartPositionLastFrame;

		protected bool _alwaysFaceCamera;

		protected Camera _targetCamera;

		protected Quaternion _targetCameraRotation;

		protected bool _animateOpacity;

		protected AnimationCurve _animateOpacityCurve;

		protected float _remapOpacityZero;

		protected float _remapOpacityOne;

		protected bool _animateScale;

		protected AnimationCurve _animateScaleCurve;

		protected float _remapScaleZero;

		protected float _remapScaleOne;

		protected bool _animateColor;

		protected Gradient _animateColorGradient;

		protected Vector3 _newScale;

		protected Color _newColor;

		protected float _elapsedTime;

		protected float _remappedTime;

		public virtual float GetTime()
		{
			return 0f;
		}

		public virtual float GetDeltaTime()
		{
			return 0f;
		}

		protected virtual void OnEnable()
		{
		}

		public virtual void SetUseUnscaledTime(bool status, bool resetStartedAt)
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void UpdateFloatingText()
		{
		}

		protected virtual void HandleMovement()
		{
		}

		protected virtual void HandleColor()
		{
		}

		protected virtual void HandleOpacity()
		{
		}

		protected virtual void HandleScale()
		{
		}

		protected virtual void HandleAlignment()
		{
		}

		protected virtual void HandleBillboard()
		{
		}

		public virtual void SetProperties(string value, float lifetime, Vector3 direction, bool animateMovement, MMFloatingTextSpawner.AlignmentModes alignmentMode, Vector3 fixedAlignment, bool alwaysFaceCamera, Camera targetCamera, bool animateX, AnimationCurve animateXCurve, float remapXZero, float remapXOne, bool animateY, AnimationCurve animateYCurve, float remapYZero, float remapYOne, bool animateZ, AnimationCurve animateZCurve, float remapZZero, float remapZOne, bool animateOpacity, AnimationCurve animateOpacityCurve, float remapOpacityZero, float remapOpacityOne, bool animateScale, AnimationCurve animateScaleCurve, float remapScaleZero, float remapScaleOne, bool animateColor, Gradient animateColorGradient)
		{
		}

		public virtual void ResetPosition()
		{
		}

		public virtual void SetText(string newValue)
		{
		}

		public virtual void SetColor(Color newColor)
		{
		}

		public virtual void SetOpacity(float newOpacity)
		{
		}

		protected virtual void TurnOff()
		{
		}
	}
}
