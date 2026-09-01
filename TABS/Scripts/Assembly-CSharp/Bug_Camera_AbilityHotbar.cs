using System;
using System.Collections.Generic;
using InControl;
using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;

public class Bug_Camera_AbilityHotbar : MonoBehaviour
{
	private Bug_CameraAbility currentAbility;

	private const float TARGET_VALUE = 0.6f;

	private const int ABILITY_COUNT = 5;

	private const int ABILITY_START = 49;

	private InputService inputService;

	private PlayerActions playerActions;

	private InControlInputModule inputModule;

	private static readonly Dictionary<KeyCode, Action> AbilityAlphaKeys = new Dictionary<KeyCode, Action>();

	private int currentSelectedAbilityIndex;

	protected void Awake()
	{
		inputService = ServiceLocator.GetService<InputService>();
		inputService.InputChanged += OnInputChange;
		playerActions = PlayerActions.Instance;
	}

	private void Start()
	{
		RegisterAlphaActions();
		DisableAll();
		Switch(0);
	}

	private void Update()
	{
		AbilitySelection();
		CycleAbilities();
	}

	private void RegisterAlphaActions()
	{
		AbilityAlphaKeys.Clear();
		int num = 0;
		for (int i = 49; i < 54; i++)
		{
			KeyCode key = (KeyCode)i;
			int index = num;
			AbilityAlphaKeys.Add(key, delegate
			{
				Switch(index);
			});
			num++;
		}
	}

	private void AbilitySelection()
	{
		foreach (KeyValuePair<KeyCode, Action> abilityAlphaKey in AbilityAlphaKeys)
		{
			if (Input.GetKeyDown(abilityAlphaKey.Key) && inputService.CurrentState == InputService.InputState.Gameplay)
			{
				abilityAlphaKey.Value();
			}
		}
	}

	private void CycleAbilities()
	{
		int num = 4;
		if (playerActions.m_CycleAbilitiesLeft.WasPressed && inputService.CurrentState == InputService.InputState.Gameplay)
		{
			int id = ((currentSelectedAbilityIndex - 1 < 0) ? num : (currentSelectedAbilityIndex - 1));
			Switch(id);
		}
		else if (playerActions.m_CycleAbilitiesRight.WasPressed && inputService.CurrentState == InputService.InputState.Gameplay)
		{
			int id2 = ((currentSelectedAbilityIndex + 1 <= num) ? (currentSelectedAbilityIndex + 1) : 0);
			Switch(id2);
		}
	}

	private void Switch(int id)
	{
		currentSelectedAbilityIndex = id;
		Bug_CameraAbility component = base.transform.GetChild(id).GetComponent<Bug_CameraAbility>();
		if (!(component == currentAbility))
		{
			if ((bool)currentAbility)
			{
				currentAbility.Disable();
				currentAbility.GetComponent<ScaleShake>().SetTarget(0.6f);
				currentAbility.IsActive = false;
			}
			currentAbility = component;
			currentAbility.GetComponent<ScaleShake>().SetTarget(1f);
			currentAbility.Enable();
			currentAbility.IsActive = true;
			ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect("Bugs/SwitchDevTool", 1f, base.transform.position);
		}
	}

	private void DisableAll()
	{
		for (int i = 0; i < 5; i++)
		{
			base.transform.GetChild(i).GetComponent<ScaleShake>().SetTarget(0.6f);
		}
	}

	private void OnInputChange(InputType inputType)
	{
		if (inputType > InputType.Keyboard)
		{
			throw new ArgumentOutOfRangeException();
		}
	}

	private void OnDisable()
	{
		inputService.InputChanged -= OnInputChange;
	}
}
