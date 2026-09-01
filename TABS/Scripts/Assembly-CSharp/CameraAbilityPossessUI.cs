using InControl;
using Landfall.TABS.GameMode;
using Landfall.TABS_Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraAbilityPossessUI : MonoBehaviour
{
	public enum WeaponCooldownType
	{
		None = 0,
		Active = 1,
		Passive = 2
	}

	private float IconScalingFactor = 1f;

	private const float KeyboardIconScalingFactor = 4f;

	private const float GamepadIconScalingFactor = 1f;

	public Gradient healthGradient;

	public GameObject UI;

	public GameObject leftW;

	public GameObject rightW;

	public GameObject combatM;

	public Image leftI;

	public Image rightI;

	public Image combatI;

	public Image healthI;

	public TextMeshProUGUI leftT;

	public TextMeshProUGUI rightT;

	public TextMeshProUGUI combatT;

	public GameObject Dot;

	[SerializeField]
	private LocalizeText mainControlsPrompt;

	[SerializeField]
	private int textFontSize = 15;

	[SerializeField]
	private int iconFontSize = 40;

	[SerializeField]
	private ActionGlyph possessionInfo;

	public bool UIActive;

	private GlyphService glyphService;

	private string mainAttackPrompt;

	private string secondaryAttackPrompt;

	private string specialAttackPrompt;

	private PlayerActions playerActions;

	private void Start()
	{
		glyphService = ServiceLocator.GetService<GlyphService>();
		playerActions = PlayerActions.Instance;
		UpdatePrompts(playerActions.LastInputType);
		if (!ServiceLocator.GetService<GameModeService>().IsCurrentBaseGameModeType<LocalMultiplayerGameMode>())
		{
			playerActions.OnLastInputTypeChanged += UpdatePrompts;
		}
	}

	public void SetPlayerActions(PlayerActions actions)
	{
		playerActions = actions;
		if (ServiceLocator.GetService<GameModeService>().IsCurrentBaseGameModeType<LocalMultiplayerGameMode>())
		{
			UpdatePrompts(playerActions.GetAssumedBindingSourceAtInit());
			if (possessionInfo != null)
			{
				possessionInfo.OverrideAutomaticUpdate(playerActions);
			}
		}
	}

	public void StartPossess(WeaponCooldownType rightWeapon, WeaponCooldownType leftWeapon, WeaponCooldownType combatMove)
	{
		if (rightWeapon != WeaponCooldownType.None)
		{
			rightW.SetActive(value: true);
		}
		if (leftWeapon != WeaponCooldownType.None)
		{
			leftW.SetActive(value: true);
		}
		if (combatMove != WeaponCooldownType.None)
		{
			combatM.SetActive(value: true);
		}
		UI.GetComponent<CodeAnimation>().PlayIn();
		UIActive = true;
	}

	public void UpdateHealth(float healthPercentage)
	{
		healthI.color = healthGradient.Evaluate(healthPercentage);
		healthI.fillAmount = healthPercentage;
	}

	public void UpdateCooldowns(float leftCounter, float leftMax, float rightCounter, float rightMax, float moveCounter, float moveMax, bool moveIsInRange = false, bool attackLeftIsActive = false, bool attackRightIsActive = false)
	{
		leftI.fillAmount = leftCounter / leftMax;
		rightI.fillAmount = rightCounter / rightMax;
		combatI.fillAmount = moveCounter / moveMax;
		leftT.text = Mathf.Clamp(leftMax - leftCounter, 0f, float.PositiveInfinity).ToString("F0");
		rightT.text = Mathf.Clamp(rightMax - rightCounter, 0f, float.PositiveInfinity).ToString("F0");
		combatT.text = Mathf.Clamp(moveMax - moveCounter, 0f, float.PositiveInfinity).ToString("F0");
		if (leftI.fillAmount >= 1f)
		{
			leftT.text = mainAttackPrompt;
		}
		if (rightI.fillAmount >= 1f)
		{
			rightT.text = secondaryAttackPrompt;
			if (leftCounter == 0f && leftMax == 0f)
			{
				rightT.text = mainAttackPrompt;
			}
		}
		if (combatI.fillAmount >= 1f)
		{
			combatT.text = specialAttackPrompt;
		}
		if (moveIsInRange)
		{
			combatM.transform.localScale = Vector3.Lerp(combatM.transform.localScale, Vector3.one, Time.deltaTime * 10f);
		}
		else
		{
			combatM.transform.localScale = Vector3.Lerp(combatM.transform.localScale, Vector3.one * 0.5f, Time.deltaTime * 10f);
		}
		if (attackLeftIsActive)
		{
			leftW.transform.localScale = Vector3.Lerp(leftW.transform.localScale, Vector3.one, Time.deltaTime * 10f);
		}
		else
		{
			leftW.transform.localScale = Vector3.Lerp(leftW.transform.localScale, Vector3.one * 0.5f, Time.deltaTime * 10f);
		}
		if (attackRightIsActive)
		{
			rightW.transform.localScale = Vector3.Lerp(rightW.transform.localScale, Vector3.one, Time.deltaTime * 10f);
		}
		else
		{
			rightW.transform.localScale = Vector3.Lerp(rightW.transform.localScale, Vector3.one * 0.5f, Time.deltaTime * 10f);
		}
	}

	public void EndPossess()
	{
		if (UI != null)
		{
			UI.GetComponent<CodeAnimation>().PlayOut();
		}
		UIActive = false;
	}

	private void UpdatePrompts(BindingSourceType bindingSourceType)
	{
		InputType inputType = playerActions.GetInputType(bindingSourceType);
		GetInputTypeBinding(playerActions.m_possessAttack1, inputType);
		mainAttackPrompt = glyphService.GetActionGlyph(playerActions.m_possessAttack1, inputType, playerActions.LastDeviceStyle);
		GetInputTypeBinding(playerActions.m_possessAttack2, inputType);
		secondaryAttackPrompt = glyphService.GetActionGlyph(playerActions.m_possessAttack2, inputType, playerActions.LastDeviceStyle);
		GetInputTypeBinding(playerActions.m_possessAttack3, inputType);
		specialAttackPrompt = glyphService.GetActionGlyph(playerActions.m_possessAttack3, inputType, playerActions.LastDeviceStyle);
		switch (inputType)
		{
		case InputType.Controller:
		{
			string actionGlyph = glyphService.GetActionGlyph(playerActions.m_possessToggle, inputType);
			string actionGlyph2 = glyphService.GetActionGlyph(playerActions.m_possessToggleCamera, inputType);
			mainControlsPrompt.Args = new string[2] { actionGlyph, actionGlyph2 };
			IconScalingFactor = 1f;
			SetFontSizeForPrompts(iconFontSize);
			break;
		}
		case InputType.Keyboard:
		{
			BindingSource inputTypeBinding = GetInputTypeBinding(playerActions.m_possessToggle, inputType);
			string localizedBinding = PlayerActions.GetLocalizedBinding(inputTypeBinding.Name, inputTypeBinding.BindingSourceType);
			BindingSource inputTypeBinding2 = GetInputTypeBinding(playerActions.m_possessToggleCamera, inputType);
			string localizedBinding2 = PlayerActions.GetLocalizedBinding(inputTypeBinding2.Name, inputTypeBinding2.BindingSourceType);
			mainControlsPrompt.Args = new string[2] { localizedBinding, localizedBinding2 };
			IconScalingFactor = 4f;
			SetFontSizeForPrompts(textFontSize);
			break;
		}
		}
		mainControlsPrompt.LocaleID = "LABEL_POSSESS_INFO";
	}

	private BindingSource GetInputTypeBinding(PlayerAction action, InputType inputType)
	{
		foreach (BindingSource binding in action.Bindings)
		{
			if (playerActions.GetInputType(binding.BindingSourceType) == inputType)
			{
				return binding;
			}
		}
		return null;
	}

	private void SetFontSizeForPrompts(int size)
	{
		TextMeshProUGUI textMeshProUGUI = leftT;
		float fontSize = (rightT.fontSize = (float)size * IconScalingFactor);
		textMeshProUGUI.fontSize = fontSize;
	}

	private void OnDestroy()
	{
		if (playerActions != null)
		{
			playerActions.OnLastInputTypeChanged -= UpdatePrompts;
		}
	}
}
