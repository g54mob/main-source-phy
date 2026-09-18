using UnityEngine;
using UnityEngine.InputSystem;

namespace Boxophobic.Utility
{
	public class CamController : MonoBehaviour
	{
		public float movementSpeed = 5f;

		public float accelerationMultiplier = 2f;

		public float sensitivity = 2f;

		private float yaw;

		private float pitch;

		private InputAction moveAction;

		private InputAction lookAction;

		private InputAction shiftAction;

		private void OnEnable()
		{
			InputActionMap map = new InputActionMap("Cam Controller");
			lookAction = map.AddAction("look", InputActionType.Value, "<Mouse>/delta");
			moveAction = map.AddAction("move");
			shiftAction = map.AddAction("shift");
			moveAction.AddCompositeBinding("Dpad").With("Up", "<Keyboard>/w").With("Up", "<Keyboard>/upArrow")
				.With("Down", "<Keyboard>/s")
				.With("Down", "<Keyboard>/downArrow")
				.With("Left", "<Keyboard>/a")
				.With("Left", "<Keyboard>/leftArrow")
				.With("Right", "<Keyboard>/d")
				.With("Right", "<Keyboard>/rightArrow");
			shiftAction.AddBinding("<Keyboard>/leftShift");
			shiftAction.AddBinding("<Keyboard>/rightShift");
			lookAction.Enable();
			moveAction.Enable();
			shiftAction.Enable();
		}

		private void OnDisable()
		{
			lookAction?.Disable();
			moveAction?.Disable();
			shiftAction?.Disable();
		}

		private void Start()
		{
			yaw = base.transform.eulerAngles.y;
			float x = base.transform.eulerAngles.x;
			pitch = ((x > 180f) ? (x - 360f) : x);
		}

		private void Update()
		{
			float num = movementSpeed;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			float num5 = 0f;
			if (shiftAction.ReadValue<float>() > 0f)
			{
				num *= accelerationMultiplier;
			}
			Vector2 vector = moveAction.ReadValue<Vector2>();
			num2 = vector.x;
			num3 = vector.y;
			Vector2 vector2 = lookAction.ReadValue<Vector2>();
			num4 = vector2.x * 0.1f;
			num5 = vector2.y * 0.1f;
			base.transform.Translate(num2 * num * Time.deltaTime, 0f, num3 * num * Time.deltaTime);
			yaw += sensitivity * num4;
			pitch -= sensitivity * num5;
			pitch = Mathf.Clamp(pitch, -90f, 90f);
			base.transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
		}
	}
}
