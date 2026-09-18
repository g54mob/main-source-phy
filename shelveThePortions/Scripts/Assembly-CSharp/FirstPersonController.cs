using UnityEngine;
using VInspector;

public class FirstPersonController : SceneSingleton<FirstPersonController>
{
	private Rigidbody rb;

	[SerializeField]
	private LoopAudioSource walkLoopAudioSource;

	[SerializeField]
	private LoopAudioSource runLoopAudioSource;

	[Foldout("Camera Movement")]
	public Camera playerCamera;

	public float currentFov = 60f;

	public float minFov = 60f;

	public float maxFov = 110f;

	public bool invertCamera;

	public bool cameraCanMove = true;

	public float minMouseSensitivity = 0.3f;

	public float maxMouseSensitivity = 1f;

	[Space]
	public float minControllerSensitivity = 0.3f;

	public float maxControllerSensitivity = 1f;

	[ReadOnly]
	[SerializeField]
	private float currentMouseSensitivity = 2f;

	[ReadOnly]
	[SerializeField]
	private float currentControllerSensitivity = 2f;

	public float maxLookAngle = 50f;

	public bool crosshair = true;

	public Sprite crosshairImage;

	public Color crosshairColor = Color.white;

	private float yaw;

	private float pitch;

	[Foldout("Camera Zoom")]
	public bool enableZoom = true;

	public bool holdToZoom;

	public float zoomFOV = 30f;

	public float zoomStepTime = 5f;

	[SerializeField]
	[ReadOnly]
	private bool isZoomed;

	[Foldout("Movement")]
	public bool playerCanMove = true;

	public float walkSpeed = 5f;

	public float maxVelocityChange = 10f;

	private bool isWalking;

	[Foldout("Sprint")]
	public bool enableSprint = true;

	public bool unlimitedSprint;

	public float sprintSpeed = 7f;

	public float sprintDuration = 5f;

	public float sprintCooldown = 0.5f;

	private float sprintFOV = 80f;

	public float sprintFOVStepTime = 10f;

	public bool useSprintBar = true;

	public bool hideBarWhenFull = true;

	public float sprintBarWidthPercent = 0.3f;

	public float sprintBarHeightPercent = 0.015f;

	private bool isSprinting;

	private float sprintRemaining;

	private float sprintBarWidth;

	private float sprintBarHeight;

	private bool isSprintCooldown;

	private float sprintCooldownReset;

	[Foldout("Jump")]
	public bool enableJump = true;

	public float jumpPower = 5f;

	private bool isGrounded;

	[Foldout("Crouch")]
	public bool enableCrouch = true;

	public bool holdToCrouch = true;

	public KeyCode crouchKey = KeyCode.LeftControl;

	public float crouchHeight = 0.75f;

	public float speedReduction = 0.5f;

	private bool isCrouched;

	private Vector3 originalScale;

	[Foldout("Head Bob")]
	public bool enableHeadBobPuzzle = true;

	private bool enableHeadBob = true;

	public Transform joint;

	public float bobSpeed = 10f;

	public Vector3 bobAmount = new Vector3(0.15f, 0.05f, 0f);

	private Vector3 jointOriginalPos;

	private float timer;

	private void Start()
	{
		UpdateOptions();
		rb = GetComponent<Rigidbody>();
		yaw = base.transform.localEulerAngles.y;
		pitch = playerCamera.transform.localEulerAngles.x;
		playerCamera.fieldOfView = currentFov;
		originalScale = base.transform.localScale;
		jointOriginalPos = joint.localPosition;
		if (!unlimitedSprint)
		{
			sprintRemaining = sprintDuration;
			sprintCooldownReset = sprintCooldown;
		}
		if (useSprintBar)
		{
			float num = Screen.width;
			float num2 = Screen.height;
			sprintBarWidth = num * sprintBarWidthPercent;
			sprintBarHeight = num2 * sprintBarHeightPercent;
			_ = hideBarWhenFull;
		}
	}

	private void Update()
	{
		CheckGround();
		HeadBob();
	}

	public void UpdateMovement(Vector3 targetVelocity, bool isSprint)
	{
		if (!playerCanMove)
		{
			return;
		}
		if (targetVelocity.x != 0f || (targetVelocity.z != 0f && isGrounded))
		{
			isWalking = true;
			walkLoopAudioSource.PlayAudio();
		}
		else
		{
			isWalking = false;
			walkLoopAudioSource.StopAudio();
		}
		if (enableSprint && isSprint && sprintRemaining > 0f && !isSprintCooldown)
		{
			targetVelocity = base.transform.TransformDirection(targetVelocity) * sprintSpeed;
			Vector3 linearVelocity = rb.linearVelocity;
			Vector3 force = targetVelocity - linearVelocity;
			force.x = Mathf.Clamp(force.x, 0f - maxVelocityChange, maxVelocityChange);
			force.z = Mathf.Clamp(force.z, 0f - maxVelocityChange, maxVelocityChange);
			force.y = 0f;
			if (force.x != 0f || force.z != 0f)
			{
				isSprinting = true;
				walkLoopAudioSource.StopAudio();
				runLoopAudioSource.PlayAudio();
				if (isCrouched)
				{
					Crouch();
				}
			}
			rb.AddForce(force, ForceMode.VelocityChange);
		}
		else
		{
			isSprinting = false;
			runLoopAudioSource.StopAudio();
			targetVelocity = base.transform.TransformDirection(targetVelocity) * walkSpeed;
			Vector3 linearVelocity2 = rb.linearVelocity;
			Vector3 force2 = targetVelocity - linearVelocity2;
			force2.x = Mathf.Clamp(force2.x, 0f - maxVelocityChange, maxVelocityChange);
			force2.z = Mathf.Clamp(force2.z, 0f - maxVelocityChange, maxVelocityChange);
			force2.y = 0f;
			rb.AddForce(force2, ForceMode.VelocityChange);
		}
		UpdateSprint();
	}

	private void UpdateSprint()
	{
		if (!enableSprint)
		{
			return;
		}
		if (isSprinting)
		{
			isZoomed = false;
			playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, sprintFOV, sprintFOVStepTime * Time.deltaTime);
			if (!unlimitedSprint)
			{
				sprintRemaining -= 1f * Time.deltaTime;
				if (sprintRemaining <= 0f)
				{
					isSprinting = false;
					isSprintCooldown = true;
				}
			}
		}
		else
		{
			sprintRemaining = Mathf.Clamp(sprintRemaining += 1f * Time.deltaTime, 0f, sprintDuration);
		}
		if (isSprintCooldown)
		{
			sprintCooldown -= 1f * Time.deltaTime;
			if (sprintCooldown <= 0f)
			{
				isSprintCooldown = false;
			}
		}
		else
		{
			sprintCooldown = sprintCooldownReset;
		}
		if (useSprintBar)
		{
			_ = unlimitedSprint;
		}
	}

	public void SetIsZoom(bool x)
	{
		if (enableZoom && holdToZoom && !isSprinting)
		{
			isZoomed = x;
			if (x)
			{
				SceneSingleton<HoveredPotionController>.Instance.SetSetIsCameraZoomed(flag: true);
			}
			else
			{
				SceneSingleton<HoveredPotionController>.Instance.SetSetIsCameraZoomed(flag: false);
			}
		}
	}

	public void UpdateCameraZoom()
	{
		if (enableZoom)
		{
			if (isZoomed)
			{
				playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomFOV, zoomStepTime * Time.deltaTime);
			}
			else if (!isZoomed && !isSprinting)
			{
				playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, currentFov, zoomStepTime * Time.deltaTime);
			}
		}
	}

	public void UpdateCameraMovement(float mouseX, float mouseY, bool isController)
	{
		if (cameraCanMove)
		{
			float num = (isController ? currentControllerSensitivity : currentMouseSensitivity);
			yaw += mouseX * num;
			if (!invertCamera)
			{
				pitch -= num * mouseY;
			}
			else
			{
				pitch += num * mouseY;
			}
			pitch = Mathf.Clamp(pitch, 0f - maxLookAngle, maxLookAngle);
			rb.MoveRotation(Quaternion.Euler(0f, yaw, 0f));
			playerCamera.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
		}
	}

	public void UpdateOptions()
	{
		currentFov = GetFOV();
		sprintFOV = currentFov + 10f;
		currentControllerSensitivity = Mathf.Lerp(minControllerSensitivity, maxControllerSensitivity, SaveSystem.GetControllerSensitivitySlider());
		currentMouseSensitivity = Mathf.Lerp(minMouseSensitivity, maxMouseSensitivity, SaveSystem.GetMouseSensitivitySlider());
		enableHeadBob = SaveSystem.GetHeadBobSetting();
	}

	public int GetFOV()
	{
		return (int)Mathf.Lerp(minFov, maxFov, SaveSystem.GetFOVSlider());
	}

	public void Jump()
	{
		if (enableJump && isGrounded)
		{
			if (isGrounded)
			{
				rb.AddForce(0f, jumpPower, 0f, ForceMode.Impulse);
				isGrounded = false;
			}
			if (isCrouched && !holdToCrouch)
			{
				Crouch();
			}
		}
	}

	public void UpdateCrouch(bool isCrouch)
	{
		if (enableCrouch)
		{
			if (isCrouch && !holdToCrouch)
			{
				Crouch();
			}
			if (isCrouch && holdToCrouch)
			{
				isCrouched = false;
				Crouch();
			}
			else if (isCrouch && holdToCrouch)
			{
				isCrouched = true;
				Crouch();
			}
		}
	}

	private void Crouch()
	{
		if (isCrouched)
		{
			base.transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
			walkSpeed /= speedReduction;
			isCrouched = false;
		}
		else
		{
			base.transform.localScale = new Vector3(originalScale.x, crouchHeight, originalScale.z);
			walkSpeed *= speedReduction;
			isCrouched = true;
		}
	}

	private void CheckGround()
	{
		Vector3 vector = new Vector3(base.transform.position.x, base.transform.position.y - base.transform.localScale.y * 0.5f, base.transform.position.z);
		Vector3 vector2 = base.transform.TransformDirection(Vector3.down);
		float num = 1f;
		if (Physics.Raycast(vector, vector2, out var _, num))
		{
			Debug.DrawRay(vector, vector2 * num, Color.red);
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
		}
	}

	private void HeadBob()
	{
		if (!enableHeadBobPuzzle || !enableHeadBob)
		{
			return;
		}
		if (isWalking)
		{
			if (isSprinting)
			{
				timer += Time.deltaTime * (bobSpeed + sprintSpeed);
			}
			else if (isCrouched)
			{
				timer += Time.deltaTime * (bobSpeed * speedReduction);
			}
			else
			{
				timer += Time.deltaTime * bobSpeed;
			}
			joint.localPosition = new Vector3(jointOriginalPos.x + Mathf.Sin(timer) * bobAmount.x, jointOriginalPos.y + Mathf.Sin(timer) * bobAmount.y, jointOriginalPos.z + Mathf.Sin(timer) * bobAmount.z);
		}
		else
		{
			timer = 0f;
			joint.localPosition = new Vector3(Mathf.Lerp(joint.localPosition.x, jointOriginalPos.x, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.y, jointOriginalPos.y, Time.deltaTime * bobSpeed), Mathf.Lerp(joint.localPosition.z, jointOriginalPos.z, Time.deltaTime * bobSpeed));
		}
	}
}
