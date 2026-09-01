using System;
using System.Collections.Generic;
using Landfall.TABS;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LevelCreator
{
	public class DMUIManager : MonoBehaviour
	{
		public enum UIPanels
		{
			LevelSettings = 0,
			LevelMenu = 1,
			TopBar = 2,
			ItemBrowser = 3,
			SeedBrowser = 4,
			StartMenu = 5,
			NewLevelMenu = 6,
			SaveMenu = 7,
			SliderMenu = 8
		}

		private Dictionary<UIPanels, DMUIPanel> m_panels = new Dictionary<UIPanels, DMUIPanel>();

		private Stack<DMUIPanel> m_openPanels = new Stack<DMUIPanel>();

		public bool canOpen = true;

		private static DMUIManager m_instance;

		public bool IsOpen => m_openPanels.Count > 0;

		public DMUIPanel currentPanel
		{
			get
			{
				if (IsOpen)
				{
					return m_openPanels.Peek();
				}
				return null;
			}
		}

		public static DMUIManager Instance
		{
			get
			{
				if (m_instance == null)
				{
					m_instance = UnityEngine.Object.FindObjectOfType<DMUIManager>();
				}
				return m_instance;
			}
		}

		private void Awake()
		{
			m_instance = this;
			Init();
		}

		private void Init()
		{
			Canvas[] array = UnityEngine.Object.FindObjectsOfType<Canvas>();
			List<DMUIPanel> list = new List<DMUIPanel>();
			Canvas[] array2 = array;
			foreach (Canvas canvas in array2)
			{
				list.AddRange(canvas.GetComponentsInChildren<DMUIPanel>(includeInactive: true));
			}
			foreach (DMUIPanel item in list)
			{
				BindPanel(item, item.m_panelType);
				item.gameObject.SetActive(value: true);
				item.Hide(overrideDuration: true);
				SetPanelInteractive(item, interactive: false, 0f);
			}
		}

		public void BindPanel(DMUIPanel panel, UIPanels panelType)
		{
			if (!m_panels.ContainsKey(panelType))
			{
				m_panels.Add(panelType, panel);
			}
		}

		public void OpenPanel(UIPanels panelType, bool clearModalSelection = false)
		{
			if (m_panels.TryGetValue(panelType, out var value))
			{
				if (clearModalSelection)
				{
					ServiceLocator.GetService<ModalPanel>().ClearDelaySelectableGameObject();
				}
				PushPanel(value);
			}
		}

		public void PushPanel(DMUIPanel panel)
		{
			if (!canOpen)
			{
				return;
			}
			if (IsOpen)
			{
				if (EventSystem.current.currentSelectedGameObject != null)
				{
					currentPanel.m_lastSelectedObject = EventSystem.current.currentSelectedGameObject;
				}
				SetPanelInteractive(currentPanel, interactive: false);
			}
			m_openPanels.Push(panel);
			DMEditor.Instance.UpdateInputMode();
			SetPanelInteractive(currentPanel, interactive: true);
			InputManager.PushState(currentPanel.m_inputState);
			panel.OnOpen();
		}

		public void PopPanel()
		{
			if (IsOpen)
			{
				currentPanel.OnClose();
				currentPanel.m_lastSelectedObject = null;
				SetPanelInteractive(currentPanel, interactive: false);
				InputManager.RemoveState(currentPanel.m_inputState);
				m_openPanels.Pop();
				DMEditor.Instance.UpdateInputMode();
				if (IsOpen)
				{
					SetPanelInteractive(currentPanel, interactive: true);
				}
			}
		}

		public void PopAll()
		{
			while (IsOpen)
			{
				PopPanel();
			}
		}

		private void SetPanelInteractive(DMUIPanel panel, bool interactive, float fadeTime = 0.3f)
		{
			int num = (interactive ? 1 : 0);
			if (interactive)
			{
				panel.gameObject.SetActive(value: true);
			}
			CanvasGroup canvasGroup = panel.GetComponentInParent<CanvasGroup>();
			if (canvasGroup == null)
			{
				Debug.LogError(panel.gameObject.name + " doesn't have a CanvasGroup!");
				return;
			}
			canvasGroup.interactable = interactive;
			LeanTween.value(canvasGroup.alpha, num, fadeTime).setOnUpdate(delegate(float value)
			{
				if ((bool)canvasGroup)
				{
					canvasGroup.alpha = value;
				}
			}).setOnComplete((System.Action)delegate
			{
				panel.gameObject.SetActive(interactive);
			});
			if (interactive && panel.m_lastSelectedObject != null)
			{
				EventSystem.current.SetSelectedGameObject(panel.m_lastSelectedObject);
			}
		}
	}
}
