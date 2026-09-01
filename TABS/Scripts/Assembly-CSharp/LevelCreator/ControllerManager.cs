using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;

namespace LevelCreator
{
	public class ControllerManager : MonoBehaviour
	{
		public float speed;

		public float sprintMultiplier = 2f;

		[Range(0f, 1f)]
		public float groundMovementSpeedReduction = 0.25f;

		public float jumpSpeed;

		public float gravity;

		public float outOfBoundsHeight = -200f;

		public bool isFlying;

		public float maxCameraLean = 89.9f;

		public float sensitivity = 0.5f;

		private const float velocityLerpSpeed = 1E-14f;

		private const float rotationLerpSpeed = 1E-22f;

		private const float moveToAreaVelocityLerpSpeed = 0.01f;

		private const float moveToAreaRotationLerpSpeed = 0.01f;

		public Vector3 areaViewPosition = new Vector3(60f, 80f, 32f);

		public Vector3 areaViewEulerAngles = new Vector3(70f, 0f, 0f);

		private CameraScript cameraScript;

		public Transform eyeInitial;

		private Transform eye;

		protected InputService inputService;

		private PopUp movementPopUp;

		private CharacterController controller;

		private Vector3 direction;

		private Vector3 eulerAngles;

		private bool isSprinting;

		private float currentSpeed;

		private bool rotationLock;

		private bool movementLock;

		private bool moveToAreaView;

		private float m_timeSinceMoved;

		private GlobalSettingsHandler m_settingsInstance;

		private void Start()
		{
			eye = eyeInitial;
			cameraScript = DMEditor.Instance.playerCamera.GetComponent<CameraScript>();
			controller = GetComponent<CharacterController>();
			controller.transform.position = areaViewPosition;
			eulerAngles = areaViewEulerAngles;
			inputService = ServiceLocator.GetService<InputService>();
			if (inputService != null)
			{
				inputService.InputChanged += OnInputSourceChanged;
			}
			m_settingsInstance = ServiceLocator.GetService<GlobalSettingsHandler>();
		}

		private void OnDisable()
		{
			if (inputService != null)
			{
				inputService.InputChanged -= OnInputSourceChanged;
			}
		}

		public void ResetView()
		{
			SetFlying(enabled: true);
			moveToAreaView = true;
		}

		private void Update()
		{
			if (moveToAreaView)
			{
				controller.transform.position = areaViewPosition;
				eulerAngles = areaViewEulerAngles;
				if (Vector3.SqrMagnitude(cameraScript.transform.position - eye.transform.position) < 50f)
				{
					moveToAreaView = false;
				}
			}
			else
			{
				Move();
				if (base.transform.position.y < outOfBoundsHeight)
				{
					base.transform.position += new Vector3(0f, 0f - outOfBoundsHeight + 50f, 0f);
					SetFlying(enabled: true);
				}
			}
			base.transform.rotation = Quaternion.Euler(0f, eulerAngles.y, 0f);
			eye.transform.localRotation = Quaternion.Euler(eulerAngles.x, 0f, eulerAngles.z);
			cameraScript.SetTarget(eye.transform, moveToAreaView ? 0.01f : 1E-14f, moveToAreaView ? 0.01f : 1E-22f);
			if (m_timeSinceMoved > 20f)
			{
				m_timeSinceMoved = 0f;
				movementPopUp = TutorialPopUps.MovementPopUp(this);
			}
			else if (!DMUIManager.Instance.IsOpen)
			{
				m_timeSinceMoved += Time.unscaledDeltaTime;
			}
		}

		private void Move()
		{
			if (movementLock)
			{
				return;
			}
			PlayerActions instance = PlayerActions.Instance;
			if (!rotationLock)
			{
				AddRotation(new Vector3(0f - instance.m_aim.Y, instance.m_aim.X, 0f));
			}
			if (instance.InputType == InputType.Controller)
			{
				if (instance.m_moveFast.WasPressed)
				{
					isSprinting = !isSprinting;
				}
			}
			else if (instance.m_moveFast.WasPressed)
			{
				isSprinting = true;
			}
			else if (instance.m_moveFast.WasReleased)
			{
				isSprinting = false;
			}
			if (controller.isGrounded || isFlying)
			{
				direction = instance.m_move.Y * base.transform.forward;
				direction += instance.m_move.X * base.transform.right;
				if (direction.sqrMagnitude > 1f)
				{
					direction.Normalize();
				}
				if (instance.m_flyUp.IsPressed && !Input.GetKey(KeyCode.E))
				{
					direction.y = jumpSpeed * (isSprinting ? sprintMultiplier : 1f);
				}
				if (instance.m_flyDown.IsPressed && !Input.GetKey(KeyCode.Q))
				{
					direction.y = (0f - jumpSpeed) * (isSprinting ? sprintMultiplier : 1f);
				}
				if (isSprinting)
				{
					currentSpeed = speed * sprintMultiplier;
				}
				else
				{
					currentSpeed = speed;
				}
				currentSpeed *= (isFlying ? 1f : (1f - groundMovementSpeedReduction));
			}
			if (direction.x != 0f || direction.z != 0f)
			{
				m_timeSinceMoved = 0f;
			}
			if (!isFlying)
			{
				direction.y -= gravity * Time.deltaTime;
			}
			if (instance.InputType == InputType.Controller && direction.sqrMagnitude < 0.01f)
			{
				isSprinting = false;
			}
			controller.Move(new Vector3(direction.x * currentSpeed, direction.y, direction.z * currentSpeed) * Time.deltaTime);
		}

		private void AddRotation(Vector3 deltaEulerAngles)
		{
			float normalizedSliderValue = m_settingsInstance.GetSettingsInstance("CONTROL_LOOK").NormalizedSliderValue;
			eulerAngles += deltaEulerAngles * sensitivity * normalizedSliderValue;
			eulerAngles.x = Mathf.Clamp(eulerAngles.x, 0f - maxCameraLean, maxCameraLean);
		}

		private void SetFlying(bool enabled)
		{
			isFlying = enabled;
		}

		public void SetRotationLock(bool locked)
		{
			rotationLock = locked;
		}

		public void SetMovementLock(bool locked)
		{
			movementLock = locked;
		}

		private void OnInputSourceChanged(InputType inputType)
		{
			if (!(movementPopUp == null))
			{
				movementPopUp.lifeTime = 0f;
				movementPopUp = TutorialPopUps.MovementPopUp(this);
			}
		}
	}
}
