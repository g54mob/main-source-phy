using UnityEngine;
using UnityEngine.InputSystem;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Camera/MMGhostCamera")]
	public class MMGhostCamera : MonoBehaviour
	{
		[Header("Speed")]
		public float MovementSpeed;

		public float RunFactor;

		public float Acceleration;

		public float Deceleration;

		public float RotationSpeed;

		[Header("Controls")]
		public InputAction HorizontalAction;

		public InputAction VerticalAction;

		public InputAction MousePositionAction;

		public Key ActivateKey;

		public Key UpKey;

		public Key DownKey;

		public Key ControlsModeSwitchKey;

		public Key TimescaleModificationKey;

		public Key RunKey;

		[Header("Mouse")]
		public float MouseSensitivity;

		public float MobileStickSensitivity;

		[Header("Timescale Modification")]
		public float TimescaleModifier;

		[Header("Settings")]
		public bool AutoActivation;

		public bool MovementEnabled;

		public bool RotationEnabled;

		[MMReadOnly]
		public bool Active;

		[MMReadOnly]
		public bool TimeAltered;

		[Header("Virtual Joysticks")]
		public bool UseMobileControls;

		[MMCondition("UseMobileControls", true)]
		public GameObject LeftStickContainer;

		[MMCondition("UseMobileControls", true)]
		public GameObject RightStickContainer;

		[MMCondition("UseMobileControls", true)]
		public MMTouchJoystick LeftStick;

		[MMCondition("UseMobileControls", true)]
		public MMTouchJoystick RightStick;

		protected Vector3 _currentInput;

		protected Vector3 _lerpedInput;

		protected Vector3 _normalizedInput;

		protected float _acceleration;

		protected float _deceleration;

		protected Vector3 _movementVector;

		protected float _speedMultiplier;

		protected Vector3 _newEulerAngles;

		protected Vector2 _mouseInput;

		protected virtual void Start()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void GetInput()
		{
		}

		protected virtual void HandleMobileControls()
		{
		}

		protected virtual void Translate()
		{
		}

		protected virtual void Rotate()
		{
		}

		protected virtual void Move()
		{
		}

		protected virtual void ToggleSlowMotion()
		{
		}

		protected virtual void ToggleFreeCamera()
		{
		}
	}
}
