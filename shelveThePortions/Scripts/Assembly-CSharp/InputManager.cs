using UnityEngine;
using UnityEngine.InputSystem;
using VInspector;

public class InputManager : Singleton<InputManager>
{
	private FirstPersonController fps;

	private TutorialManager tutorialManager;

	private CharacterInventory characterInventory;

	private QuickPuzzleSolutionPanelUI quickPuzzleSolutionPanelUI;

	[SerializeField]
	private PlayerInput playerInput;

	[ReadOnly]
	[SerializeField]
	private bool isInputOn;

	[SerializeField]
	private bool isDebugOn;

	[Space(5f)]
	[ReadOnly]
	[SerializeField]
	private Vector2 cameraDirction;

	[ReadOnly]
	[SerializeField]
	private bool isSprinting;

	[ReadOnly]
	public bool IsUsingGamepad;

	[ReadOnly]
	[SerializeField]
	private bool moveUp;

	[ReadOnly]
	[SerializeField]
	private bool moveDown;

	[ReadOnly]
	[SerializeField]
	private bool moveLeft;

	[ReadOnly]
	[SerializeField]
	private bool moveRight;

	[ReadOnly]
	[SerializeField]
	private float playerDirectionHor;

	[ReadOnly]
	[SerializeField]
	private float playerDirectionVer;

	private bool invertMouseX;

	private bool invertMouseY;

	private bool invertControllerX;

	private bool invertControllerY;

	private int scrollUp = 1;

	private int scrollDown = -1;

	private Vector2 moveDirection;

	protected override void Awake()
	{
		base.Awake();
		OnControlsChangedEvent(playerInput);
		UpdateInverts();
		EventManager.GamePause += DisableInput;
		EventManager.GameResume += EnableInput;
		EventManager.MainMenuLoaded += DisableInput;
		EventManager.PuzzleUILoaded += DisableInput;
		EventManager.CatUILoaded += DisableInput;
		EventManager.GameStart += EnableInput;
		EventManager.GameEnd += DisableInput;
		EventManager.PlaytestEnd += DisableInput;
		EventManager.OnCutsceneStart += DisableInput;
		EventManager.OnCutsceneEnd += EnableInput;
	}

	private void OnDisable()
	{
		EventManager.GamePause -= DisableInput;
		EventManager.GameResume -= EnableInput;
		EventManager.MainMenuLoaded -= DisableInput;
		EventManager.PuzzleUILoaded -= DisableInput;
		EventManager.CatUILoaded -= DisableInput;
		EventManager.GameStart -= EnableInput;
		EventManager.GameEnd -= DisableInput;
		EventManager.PlaytestEnd -= DisableInput;
		EventManager.OnCutsceneStart -= DisableInput;
		EventManager.OnCutsceneEnd -= EnableInput;
	}

	public void UpdateInverts()
	{
		invertMouseX = SaveSystem.GetInvertMouseXSetting();
		invertMouseY = SaveSystem.GetInvertMouseYSetting();
		invertControllerX = SaveSystem.GetInvertControllerXSetting();
		invertControllerY = SaveSystem.GetInvertControllerYSetting();
		scrollUp = ((!SaveSystem.GetInvertMouseWheelSetting()) ? 1 : (-1));
		scrollDown = (SaveSystem.GetInvertMouseWheelSetting() ? 1 : (-1));
	}

	public void GetInstances()
	{
		fps = SceneSingleton<FirstPersonController>.Instance;
		tutorialManager = SceneSingleton<TutorialManager>.Instance;
		characterInventory = SceneSingleton<CharacterInventory>.Instance;
		quickPuzzleSolutionPanelUI = SceneSingleton<QuickPuzzleSolutionPanelUI>.Instance;
		playerInput = GetComponent<PlayerInput>();
	}

	public void EnableInput()
	{
		GetInstances();
		isInputOn = true;
		if (playerInput.enabled)
		{
			playerInput.SwitchCurrentActionMap(playerInput.actions.actionMaps[0].name);
		}
		ResetInputManagerValues();
	}

	public void DisableInput()
	{
		isInputOn = false;
		if (quickPuzzleSolutionPanelUI != null)
		{
			quickPuzzleSolutionPanelUI.KeyUp();
		}
		if (playerInput.enabled)
		{
			playerInput.SwitchCurrentActionMap(playerInput.actions.actionMaps[1].name);
		}
		ResetInputManagerValues();
	}

	public void ResetInputManagerValues()
	{
		if (isDebugOn)
		{
			Debug.Log("ResetInputManagerValues");
		}
		if (fps != null)
		{
			fps.SetIsZoom(x: false);
		}
	}

	private void Update()
	{
		if (isInputOn && !(fps == null))
		{
			fps.UpdateCameraMovement(cameraDirction.x, cameraDirction.y, IsUsingGamepad);
		}
	}

	private void FixedUpdate()
	{
		if (!isInputOn || fps == null)
		{
			return;
		}
		if (IsUsingGamepad)
		{
			if (moveDirection.x == 0f && moveDirection.y == 0f)
			{
				isSprinting = false;
			}
			tutorialManager.CompleteTutorialStep(TutorialStepType.Tutorial_WASD_Movement);
			fps.UpdateMovement(new Vector3(moveDirection.x, 0f, moveDirection.y), isSprinting);
		}
		else
		{
			SetPlayerDirection();
		}
		fps.UpdateCameraZoom();
	}

	public static Vector2 GetMousePosition()
	{
		return Mouse.current.position.ReadValue();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		moveDirection = context.ReadValue<Vector2>();
		if (isDebugOn)
		{
			Debug.Log("direction : " + moveDirection);
		}
	}

	public void OnMoveUp(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			moveUp = true;
		}
		if (context.canceled)
		{
			moveUp = false;
		}
		if (isDebugOn)
		{
			Debug.Log("OnMoveUp Value: " + moveUp);
		}
	}

	public void OnMoveDown(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			moveDown = true;
		}
		if (context.canceled)
		{
			moveDown = false;
		}
		if (isDebugOn)
		{
			Debug.Log("OnMoveDown Value: " + moveDown);
		}
	}

	public void OnMoveLeft(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			moveLeft = true;
		}
		if (context.canceled)
		{
			moveLeft = false;
		}
		if (isDebugOn)
		{
			Debug.Log("OnMoveLeft Value: " + moveLeft);
		}
	}

	public void OnMoveRight(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			moveRight = true;
		}
		if (context.canceled)
		{
			moveRight = false;
		}
		if (isDebugOn)
		{
			Debug.Log("OnMoveRight Value: " + moveRight);
		}
	}

	private void SetPlayerDirection()
	{
		playerDirectionHor = 0f;
		playerDirectionVer = 0f;
		if (moveUp && moveDown)
		{
			playerDirectionVer = 0f;
		}
		else if (moveUp)
		{
			playerDirectionVer = 1f;
		}
		else if (moveDown)
		{
			playerDirectionVer = -1f;
		}
		if (moveRight && moveLeft)
		{
			playerDirectionHor = 0f;
		}
		else if (moveRight)
		{
			playerDirectionHor = 1f;
		}
		else if (moveLeft)
		{
			playerDirectionHor = -1f;
		}
		if (playerDirectionVer == 0f && playerDirectionHor == 0f)
		{
			isSprinting = false;
		}
		else
		{
			tutorialManager.CompleteTutorialStep(TutorialStepType.Tutorial_WASD_Movement);
		}
		fps.UpdateMovement(new Vector3(playerDirectionHor, 0f, playerDirectionVer).normalized, isSprinting);
	}

	public void OnLook(InputAction.CallbackContext context)
	{
		cameraDirction = context.ReadValue<Vector2>();
		if (IsUsingGamepad)
		{
			if (invertControllerX)
			{
				cameraDirction.x = -1f * cameraDirction.x;
			}
			if (invertControllerY)
			{
				cameraDirction.y = -1f * cameraDirction.y;
			}
		}
		else
		{
			if (invertMouseX)
			{
				cameraDirction.x = -1f * cameraDirction.x;
			}
			if (invertMouseY)
			{
				cameraDirction.y = -1f * cameraDirction.y;
			}
		}
		if (cameraDirction != Vector2.zero)
		{
			tutorialManager.CompleteTutorialStep(TutorialStepType.Tutorial_Camera_Movement);
		}
	}

	public void OnToggleRun(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			isSprinting = true;
			if (isDebugOn)
			{
				Debug.Log("OnToggleRun");
			}
		}
	}

	public void OnInteractPickupItem(InputAction.CallbackContext context)
	{
		if (isInputOn && context.performed)
		{
			if (isDebugOn)
			{
				Debug.Log("OnInteractPickupItem");
			}
			if (characterInventory != null)
			{
				characterInventory.InteractWithItem();
			}
		}
	}

	public void OnDropItem(InputAction.CallbackContext context)
	{
		if (isInputOn && context.performed)
		{
			if (isDebugOn)
			{
				Debug.Log("OnDropItem");
			}
			if (characterInventory != null)
			{
				characterInventory.DropItem();
			}
		}
	}

	public void OnScroll(InputAction.CallbackContext context)
	{
		if (!context.performed)
		{
			return;
		}
		if (characterInventory != null)
		{
			if (context.ReadValue<Vector2>().y > 0f)
			{
				characterInventory.ShuffleItems(scrollUp);
			}
			else
			{
				characterInventory.ShuffleItems(scrollDown);
			}
		}
		if (isDebugOn)
		{
			Debug.Log("OnScroll");
		}
	}

	public void OnScrollUP(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			if (isDebugOn)
			{
				Debug.Log("OnScrollUP");
			}
			if (characterInventory != null)
			{
				characterInventory.ShuffleItems(1);
			}
		}
	}

	public void OnScrollDown(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			if (isDebugOn)
			{
				Debug.Log("OnScrollDown");
			}
			if (characterInventory != null)
			{
				characterInventory.ShuffleItems(-1);
			}
		}
	}

	public void OnZoom(InputAction.CallbackContext context)
	{
		if (!isInputOn)
		{
			return;
		}
		if (context.started)
		{
			if (fps != null)
			{
				fps.SetIsZoom(x: true);
			}
			tutorialManager.CompleteTutorialStep(TutorialStepType.Tutorial_Camera_Zoom);
			if (isDebugOn)
			{
				Debug.Log("OnZoom Value: Pressed");
			}
		}
		if (context.canceled)
		{
			if (fps != null)
			{
				fps.SetIsZoom(x: false);
			}
			if (isDebugOn)
			{
				Debug.Log("OnZoom Value: Canceeled ");
			}
		}
	}

	public void OnQuickPuzzleSolution(InputAction.CallbackContext context)
	{
		if (!isInputOn)
		{
			return;
		}
		if (context.started)
		{
			quickPuzzleSolutionPanelUI.KeyDown();
			Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Click);
			tutorialManager.CompleteTutorialStep(TutorialStepType.Tutorial_QuickSolution);
			if (isDebugOn)
			{
				Debug.Log("OnQuickPuzzleSolution Value: Pressed");
			}
		}
		if (context.canceled)
		{
			quickPuzzleSolutionPanelUI.KeyUp();
			if (isDebugOn)
			{
				Debug.Log("OnQuickPuzzleSolution Value: Canceeled ");
			}
		}
	}

	public void OnHighlightAbility(InputAction.CallbackContext context)
	{
		if (isInputOn && context.performed)
		{
			if (isDebugOn)
			{
				Debug.Log("OnHighlightAbility");
			}
			SceneSingleton<AbilitiesManager>.Instance.ActivateHighlightAbility();
		}
	}

	public void OnShelveHighlightAbility(InputAction.CallbackContext context)
	{
		if (isInputOn && context.performed)
		{
			if (isDebugOn)
			{
				Debug.Log("OnShelveHighlightAbility");
			}
			SceneSingleton<AbilitiesManager>.Instance.ActivateShelveHighlightAbility();
		}
	}

	public void OnAssembleAbility(InputAction.CallbackContext context)
	{
		if (isInputOn && context.performed)
		{
			if (isDebugOn)
			{
				Debug.Log("OnAssembleAbility");
			}
			SceneSingleton<AbilitiesManager>.Instance.ActivateAssembleAbility();
		}
	}

	public void OnControlsChangedEvent(PlayerInput player)
	{
		IsUsingGamepad = player.currentControlScheme == "Gamepad";
		EventManager.ActivateEvent(EventTypes.OnControlsChange);
		if (isDebugOn)
		{
			Debug.Log("Control Scheme Changed: " + player.currentControlScheme);
		}
	}

	public void OnCancelInteractCamera(InputAction.CallbackContext context)
	{
		if (!ScenesManager.isSceneLoading && context.performed)
		{
			if (!UIBackKeyManager.IsEmpty())
			{
				UIBackKeyManager.BackUI();
			}
			if (isDebugOn)
			{
				Debug.Log("OnBackUI Clicked");
			}
		}
	}

	public void OnBackUI(InputAction.CallbackContext context)
	{
		if (!ScenesManager.isSceneLoading && context.performed)
		{
			if (!UIBackKeyManager.IsEmpty())
			{
				UIBackKeyManager.BackUI();
			}
			if (isDebugOn)
			{
				Debug.Log("OnBackUI Clicked");
			}
		}
	}
}
