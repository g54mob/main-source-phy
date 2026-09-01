using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleCharacterMotor : MonoBehaviour
{
	public CursorLockMode cursorLockMode;

	public bool cursorVisible;

	[Header("Movement")]
	public float walkSpeed;

	public float runSpeed;

	public float gravity;

	[Space]
	[Header("Look")]
	public Transform cameraPivot;

	public float lookSpeed;

	public bool invertY;

	[Space]
	[Header("Smoothing")]
	public float movementAcceleration;

	private CharacterController controller;

	private Vector3 movement;

	private Vector3 finalMovement;

	private float speed;

	private Quaternion targetRotation;

	private Quaternion targetPivotRotation;

	private void Awake()
	{
	}

	private void Update()
	{
	}

	private void UpdateLookRotation()
	{
	}

	private void UpdateTranslation()
	{
	}
}
