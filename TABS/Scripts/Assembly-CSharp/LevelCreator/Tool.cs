using System;
using System.Collections.Generic;
using Landfall.TABS_Input;
using NaughtyAttributes;
using UnityEngine;

namespace LevelCreator
{
	public abstract class Tool : MonoBehaviour
	{
		[Serializable]
		public struct ContextMenuIcons
		{
			[SerializeField]
			[BoxGroup("Context Menu")]
			public Sprite m_primaryIcon;

			[SerializeField]
			[BoxGroup("Context Menu")]
			public Sprite m_secondaryIcon;

			[SerializeField]
			[BoxGroup("Context Menu")]
			public Sprite m_special1Icon;

			[SerializeField]
			[BoxGroup("Context Menu")]
			public Sprite m_special2Icon;

			[SerializeField]
			[BoxGroup("Context Menu")]
			public Sprite m_special3Icon;

			[SerializeField]
			[BoxGroup("Context Menu")]
			public Sprite m_special4Icon;

			[SerializeField]
			[BoxGroup("Context Menu")]
			public Sprite m_special5Icon;

			[SerializeField]
			[BoxGroup("Context Menu")]
			public Sprite m_special6Icon;

			[SerializeField]
			[BoxGroup("Context Menu")]
			public Sprite m_special7Icon;

			[SerializeField]
			[BoxGroup("Context Menu")]
			public Sprite m_special8Icon;

			[SerializeField]
			[BoxGroup("Context Menu")]
			public Sprite m_special9Icon;
		}

		[SerializeField]
		private DMEditor.VisualTargetMode m_visualTargetMode = DMEditor.VisualTargetMode.Dot;

		[SerializeField]
		[BoxGroup("Context Menu")]
		protected ContextMenuIcons m_contextIcons;

		[SerializeField]
		[BoxGroup("Tool settings UI")]
		private bool m_buildToolsOnStart = true;

		[SerializeField]
		[BoxGroup("Tool settings UI")]
		private bool m_preserveToolSettings = true;

		[SerializeField]
		[ReorderableList]
		[BoxGroup("Tool settings UI")]
		protected List<ToolToggleEntry> m_toggleElements = new List<ToolToggleEntry>();

		[SerializeField]
		[ReorderableList]
		[BoxGroup("Tool settings UI")]
		protected List<ToolSliderEntry> m_sliderElements = new List<ToolSliderEntry>();

		private static Dictionary<string, List<ToolControlUIEntry>> m_previousUIElements = new Dictionary<string, List<ToolControlUIEntry>>();

		protected InputState m_inputState;

		private InputState m_cursorState = new InputState("CursorState");

		[SerializeField]
		[BoxGroup("Sound")]
		private ContinousSoundData m_backgroundSound;

		protected PlayContinousSound m_backgroundSoundObject;

		protected virtual void Awake()
		{
			m_inputState = new InputState(base.name);
		}

		protected virtual void Start()
		{
			DMEditor.Instance.SetVisualTargetMode(m_visualTargetMode);
			AssignInput(PlayerActions.Instance);
			if (m_buildToolsOnStart)
			{
				BuildToolUI();
			}
			else if (DMEditor.Instance != null && DMEditor.Instance.toolControlsBuilder != null)
			{
				DMEditor.Instance.toolControlsBuilder.EnableSliderMenuHint(enabled: false);
			}
			InputManager.PushState(m_inputState);
			DMEditor.Instance.contextInfoMenu.ReplaceContextKeys();
			m_backgroundSoundObject = Utility.PlayContinousSound(m_backgroundSound, base.transform);
		}

		protected virtual void AssignInput(PlayerActions actions)
		{
			m_inputState.ClearAllEvents();
			if (m_sliderElements.Count > 0)
			{
				m_inputState.AddOnKeyDownListener(actions.m_toolToggleSliders, delegate
				{
					DMUIManager.Instance.OpenPanel(DMUIManager.UIPanels.SliderMenu);
				});
			}
		}

		private void CopyPreviousUIInitialStates()
		{
			if (!m_preserveToolSettings || !m_previousUIElements.TryGetValue(base.gameObject.name, out var value) || value == null)
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			foreach (ToolControlUIEntry item in value)
			{
				if (item != null)
				{
					if (item is ToolToggleEntry toolToggleEntry)
					{
						m_toggleElements[num].m_initialState = toolToggleEntry.m_initialState;
						num++;
					}
					else if (item is ToolSliderEntry toolSliderEntry)
					{
						m_sliderElements[num2].m_sliderInfo.initialValue = toolSliderEntry.m_sliderInfo.initialValue;
						num2++;
					}
				}
			}
		}

		protected virtual void BuildToolUI()
		{
			CopyPreviousUIInitialStates();
			List<ToolControlUIEntry> list = new List<ToolControlUIEntry>();
			list.AddRange(m_toggleElements);
			list.AddRange(m_sliderElements);
			DMEditor.Instance.toolControlsBuilder.BuildToolUI(list, m_inputState);
			if (m_sliderElements.Count <= 0)
			{
				return;
			}
			LeanTween.delayedCall(20f, (System.Action)delegate
			{
				if (this != null)
				{
					TutorialPopUps.SlidersHintPopUp(this);
				}
			});
		}

		protected virtual void OnDestroy()
		{
			if (m_backgroundSoundObject != null)
			{
				m_backgroundSoundObject.Stop(0.5f);
			}
			List<ToolControlUIEntry> value = null;
			if (DMEditor.Instance != null && DMEditor.Instance.toolControlsBuilder != null)
			{
				value = DMEditor.Instance.toolControlsBuilder.FetchEntries();
			}
			if (m_previousUIElements.ContainsKey(base.gameObject.name))
			{
				m_previousUIElements[base.gameObject.name] = value;
			}
			else
			{
				m_previousUIElements.Add(base.gameObject.name, value);
			}
			if (DMEditor.Instance != null && DMEditor.Instance.toolControlsBuilder != null)
			{
				DMEditor.Instance.toolControlsBuilder.ClearUI();
			}
			InputManager.RemoveState(m_inputState);
		}
	}
}
