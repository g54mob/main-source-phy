using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Kamgam.SettingsGenerator.Examples
{
	public class SimplePlayer : MonoBehaviour
	{
		public Button OpenMenuButton;

		public float JumpForce;

		public float MoveForce;

		public InputActionAsset InputActionAsset;

		protected Rigidbody _rigidbody;

		protected InputAction _moveAction;

		protected PlayerInput _playerInput;

		protected bool _isNearGround;

		protected bool _jumpRequested;

		protected bool _movementRequested;

		protected Vector2 _movementDirection;

		public Rigidbody Rigidbody => null;

		public InputAction MoveAction => null;

		public PlayerInput PlayerInput => null;

		public void Start()
		{
		}

		public void Update()
		{
		}

		public void OnJump()
		{
		}

		public void OnSpecialMove()
		{
		}

		public void OnOpenMenu()
		{
		}

		public void FixedUpdate()
		{
		}

		public void SetControlsIdle(bool idle)
		{
		}
	}
}
