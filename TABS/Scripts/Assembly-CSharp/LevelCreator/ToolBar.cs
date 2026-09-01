using System.Collections.Generic;
using InControl;
using Landfall.TABS_Input;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class ToolBar : MonoBehaviour
	{
		[SerializeField]
		private GameObject m_hotbarItemKeybindPrefab;

		[SerializeField]
		private Hotbar m_hotbarPrefab;

		[SerializeField]
		private RectTransform m_leftBackground;

		[SerializeField]
		private RectTransform m_centerBackground;

		[SerializeField]
		private RectTransform m_rightBackground;

		[SerializeField]
		private RectTransform m_browseButton;

		[ReorderableList]
		[SerializeField]
		private HotbarCategory[] m_categories;

		private Hotbar m_categoryHotbar;

		private int m_categoryCount;

		private List<Hotbar> m_hotbars = new List<Hotbar>();

		private List<TextMeshProUGUI> m_hotbarKeybinds = new List<TextMeshProUGUI>();

		private PopUp m_previousPopUp;

		private float m_showPosition;

		private float m_hidePosition;

		private Color m_showColor;

		private Color m_hideColor;

		private float m_lerpTime = 0.35f;

		private static PlayerAction hotbar0;

		private static PlayerAction hotbar1;

		private static PlayerAction hotbar2;

		private static PlayerAction hotbar3;

		private static PlayerAction hotbar4;

		private void Awake()
		{
		}

		private void Start()
		{
			m_showPosition = base.transform.position.y;
			m_hidePosition = m_showPosition - 500f;
			m_showColor = m_leftBackground.GetComponent<Image>().color;
			m_hideColor = new Color(m_showColor.r, m_showColor.g, m_showColor.b, 0f);
		}

		private void AssignInput()
		{
			if (hotbar0 == null)
			{
				PlayerActions instance = PlayerActions.Instance;
				hotbar0 = new PlayerAction("Hotbar 0", instance);
				hotbar1 = new PlayerAction("Hotbar 1", instance);
				hotbar2 = new PlayerAction("Hotbar 2", instance);
				hotbar3 = new PlayerAction("Hotbar 3", instance);
				hotbar4 = new PlayerAction("Hotbar 4", instance);
				hotbar0.AddDefaultBinding(Key.Key1);
				hotbar1.AddDefaultBinding(Key.Key2);
				hotbar2.AddDefaultBinding(Key.Key3);
				hotbar3.AddDefaultBinding(Key.Key4);
				hotbar4.AddDefaultBinding(Key.Key5);
			}
			DMEditor.inputState.AddOnKeyDownListener(hotbar0, delegate
			{
				SwitchHotbar(0);
			});
			DMEditor.inputState.AddOnKeyDownListener(hotbar1, delegate
			{
				SwitchHotbar(1);
			});
			DMEditor.inputState.AddOnKeyDownListener(hotbar2, delegate
			{
				SwitchHotbar(2);
			});
			DMEditor.inputState.AddOnKeyDownListener(hotbar3, delegate
			{
				SwitchHotbar(3);
			});
			DMEditor.inputState.AddOnKeyDownListener(hotbar4, delegate
			{
				SwitchHotbar(4);
			});
		}

		public void BuildCategoryHotbar(int categoryReduction = 0)
		{
			DestroyHotbars();
			m_categoryHotbar = Object.Instantiate(m_hotbarPrefab, m_leftBackground);
			m_categoryCount = m_categories.Length - categoryReduction;
			List<HotbarItem> list = new List<HotbarItem>();
			for (int i = 0; i < m_categoryCount; i++)
			{
				string group = m_categories[i].Group;
				list.Add(new HotbarItem
				{
					icon = m_categories[i].Icon,
					group = group,
					name = m_categories[i].LocalizedName,
					callback = delegate
					{
						SwitchHotbar(group);
					}
				});
			}
			m_categoryHotbar.gameObject.name = "CategoryHotbar";
			m_categoryHotbar.invokeOnCycle = true;
			m_categoryHotbar.useFoldout = true;
			m_categoryHotbar.cycleRight = PlayerActions.Instance.m_cycleHotbarCategoryRight;
			m_categoryHotbar.cycleLeft = PlayerActions.Instance.m_cycleHotbarCategoryLeft;
			m_categoryHotbar.SetData(list);
			m_categoryHotbar.EnableHotbar(DMEditor.inputState);
			m_categoryHotbar.SetIndex(0);
			AssignInput();
			for (int num = 0; num < m_categoryCount; num++)
			{
				CreateKeyInstance("Hotbar " + num, num);
			}
			void CreateKeyInstance(string action, int index)
			{
				HotbarButton hotbarButton = m_categoryHotbar.hotbarButtons[index];
				GameObject obj = Object.Instantiate(m_hotbarItemKeybindPrefab, hotbarButton.transform);
				obj.gameObject.AddComponent<EnabledByInputMode>().inputType = InputType.Keyboard;
				TextMeshProUGUI componentInChildren = obj.GetComponentInChildren<TextMeshProUGUI>();
				componentInChildren.GetComponent<DMActionGlyph>().SetAction(action);
				m_hotbarKeybinds.Add(componentInChildren);
			}
		}

		public void BuildSubHotbars()
		{
			for (int i = 0; i < m_categoryCount; i++)
			{
				HotbarCategory category = m_categories[i];
				Hotbar hotbar = Object.Instantiate(m_hotbarPrefab, m_rightBackground);
				hotbar.gameObject.name = "Hotbar_" + category.Group;
				hotbar.invokeOnCycle = true;
				List<HotbarItem> items = new List<HotbarItem>();
				DMEditor.Instance.toolTable.ForEachRow(delegate(string key, ToolTableRow row)
				{
					if (category.Group == row.group && (row.category == ToolTableRow.ToolTableCategory.Tools || row.category == ToolTableRow.ToolTableCategory.Experimental))
					{
						items.Add(new HotbarItem
						{
							icon = row.icon,
							group = row.group,
							callback = delegate
							{
								if (DMEditor.Instance != null)
								{
									DMEditor.Instance.SwitchAction(row);
								}
							}
						});
					}
				});
				hotbar.SetData(items);
				hotbar.EnableHotbar(DMEditor.inputState);
				m_hotbars.Add(hotbar);
			}
			m_categoryHotbar.InvokeHotbarButton();
		}

		public void DestroyHotbars()
		{
			foreach (Transform item in base.transform)
			{
				Object.Destroy(item.gameObject);
			}
			m_hotbars.Clear();
			m_hotbarKeybinds.Clear();
		}

		public void SwitchHotbar(int index)
		{
			index = Mathf.Clamp(index, 0, m_categoryCount - 1);
			m_categoryHotbar.SetIndex(index);
			for (int i = 0; i < m_hotbars.Count; i++)
			{
				if (i == index)
				{
					m_hotbars[i].gameObject.SetActive(value: true);
					m_hotbars[i].InvokeHotbarButton();
					m_hotbarKeybinds[i].color = DMEditorColors.NormalColor;
				}
				else
				{
					m_hotbars[i].gameObject.SetActive(value: false);
					m_hotbarKeybinds[i].color = DMEditorColors.NormalColor;
				}
				m_hotbarKeybinds[i].GetComponentInChildren<TMPMaskable>().UpdateMasking();
			}
		}

		private void SwitchHotbar(string group)
		{
			for (int i = 0; i < m_hotbars.Count; i++)
			{
				if (m_hotbars[i].hotbarItems[0].group == group)
				{
					m_hotbars[i].gameObject.SetActive(value: true);
					m_hotbars[i].InvokeHotbarButton();
					m_hotbarKeybinds[i].color = DMEditorColors.NormalColor;
				}
				else
				{
					m_hotbars[i].gameObject.SetActive(value: false);
					m_hotbarKeybinds[i].color = DMEditorColors.NormalColor;
				}
				m_hotbarKeybinds[i].GetComponentInChildren<TMPMaskable>().UpdateMasking();
			}
		}

		public void Show()
		{
			LeanTween.color(m_leftBackground.gameObject, m_showColor, m_lerpTime);
			LeanTween.color(m_centerBackground.gameObject, m_showColor, m_lerpTime);
			LeanTween.color(m_rightBackground.gameObject, m_showColor, m_lerpTime);
			LeanTween.moveY(m_leftBackground, m_showPosition, m_lerpTime).setEaseOutExpo();
			LeanTween.moveY(m_centerBackground, m_showPosition, m_lerpTime).setEaseOutExpo();
			LeanTween.moveY(m_rightBackground, m_showPosition, m_lerpTime).setEaseOutExpo();
		}

		public void Hide()
		{
			LeanTween.color(m_leftBackground.gameObject, m_hideColor, m_lerpTime);
			LeanTween.color(m_centerBackground.gameObject, m_hideColor, m_lerpTime);
			LeanTween.color(m_rightBackground.gameObject, m_hideColor, m_lerpTime);
			LeanTween.moveY(m_leftBackground, m_hidePosition, m_lerpTime).setEaseInExpo();
			LeanTween.moveY(m_centerBackground, m_hidePosition, m_lerpTime).setEaseInExpo();
			LeanTween.moveY(m_rightBackground, m_hidePosition, m_lerpTime).setEaseInExpo();
		}
	}
}
