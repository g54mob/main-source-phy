using System.Collections.Generic;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class LevelSettings : DMUIPanel
	{
		[Header("Level Presets")]
		[SerializeField]
		private Button m_presetButtonPrefab;

		[SerializeField]
		private RectTransform m_presetButtonsParent;

		[Header("Weather")]
		public CycleSelector m_weatherSelector;

		[SerializeField]
		private Transform weatherParent;

		[Header("Water")]
		[SerializeField]
		private Slider m_waterLevelSlider;

		[Header("Music")]
		public CycleSelector m_musicSelector;

		private void AssertionCheck()
		{
		}

		private void Start()
		{
			AssertionCheck();
			InitPresetButtons();
			InitWeatherList();
			InitMusicList();
			AssignInput();
		}

		private void AssignInput()
		{
			PlayerActions instance = PlayerActions.Instance;
			m_inputState.AddOnKeyDownListener(instance.m_enterExitBattle, delegate
			{
				DMUIManager.Instance.PopPanel();
			});
			m_inputState.AddOnKeyDownListener(instance.m_back, delegate
			{
				DMUIManager.Instance.PopPanel();
			});
		}

		public override void OnOpen()
		{
			base.OnOpen();
			base.transform.parent.SetAsLastSibling();
		}

		public override void OnClose()
		{
			base.OnClose();
		}

		private void InitWeatherList()
		{
			List<CycleSelector.CycleSelectorOption> list = new List<CycleSelector.CycleSelectorOption>();
			foreach (Transform item in weatherParent)
			{
				list.Add(new CycleSelector.CycleSelectorOption
				{
					displayName = item.name,
					value = item.GetSiblingIndex()
				});
			}
			m_weatherSelector.Init(list, delegate(object index)
			{
				DMEditor.Instance.SetWeather((int)index);
			}, m_weatherSelector.Index);
		}

		private void InitMusicList()
		{
			List<CycleSelector.CycleSelectorOption> list = new List<CycleSelector.CycleSelectorOption>();
			for (int i = 0; i < AudioManager.MusicClips.Length; i++)
			{
				string value = AudioManager.MusicClips[i];
				string displayName = AudioManager.LocalizedMusicClips[i];
				list.Add(new CycleSelector.CycleSelectorOption
				{
					displayName = displayName,
					value = value
				});
			}
			m_musicSelector.Init(list, delegate(object music)
			{
				DMEditor.Instance.SetMusic((string)music);
			}, m_musicSelector.Index);
		}

		private void InitPresetButtons()
		{
			LevelPresetData[] allPresets = LevelPresetData.GetAllPresets();
			foreach (LevelPresetData preset in allPresets)
			{
				Button button = Object.Instantiate(m_presetButtonPrefab, m_presetButtonsParent);
				button.onClick.AddListener(delegate
				{
					DMEditor.Instance.SetPreset(preset);
				});
				button.GetComponentInChildren<LocalizeText>().LocaleID = preset.LocalizedName;
				button.GetComponentsInChildren<Image>()[1].sprite = preset.PresetIcon;
			}
		}

		public void LoadPreset(LevelPresetData presetData)
		{
			DMEditor.Instance.SetPreset(presetData);
		}

		public void SetWaterLevel(float value)
		{
			DMEditor.Instance.SetWaterLevel(value);
		}

		public void SetWeather(int index)
		{
			foreach (Transform item in weatherParent)
			{
				item.gameObject.SetActive(index == item.GetSiblingIndex());
			}
		}
	}
}
