using System.Collections.Generic;
using InControl;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace LevelCreator
{
	public class Hotbar : MonoBehaviour
	{
		[SerializeField]
		private Transform m_hotbarItemsTransform;

		[SerializeField]
		private HotbarButtonItem m_hotbarItemPrefab;

		[SerializeField]
		private HotbarButtonFoldoutItem m_hotbarFoldoutItemPrefab;

		[SerializeField]
		private GameObject m_edgeArrows;

		public List<HotbarItem> hotbarItems = new List<HotbarItem>();

		public List<HotbarButton> hotbarButtons = new List<HotbarButton>();

		private int m_hotbarIndex;

		public bool invokeOnCycle;

		public bool useFoldout;

		private PlayerAction m_invokeAction;

		public PlayerAction cycleRight;

		public PlayerAction cycleLeft;

		private HotbarButton ItemPrefab
		{
			get
			{
				if (!useFoldout)
				{
					return m_hotbarItemPrefab;
				}
				return m_hotbarFoldoutItemPrefab;
			}
		}

		private bool HasItemsOutsideHotbar => hotbarItems.Count > GetVisibleItemCount();

		private void AssertionCheck()
		{
		}

		public void AssignInput(InputState inputState)
		{
			if (inputState != null)
			{
				UnityAction action = delegate
				{
					CycleHotbar(right: true);
				};
				UnityAction action2 = delegate
				{
					CycleHotbar(right: false);
				};
				if (m_invokeAction == null)
				{
					m_invokeAction = PlayerActions.Instance.m_invokeHotbar;
				}
				if (cycleRight == null)
				{
					cycleRight = PlayerActions.Instance.m_cycleHotbarRight;
				}
				if (cycleLeft == null)
				{
					cycleLeft = PlayerActions.Instance.m_cycleHotbarLeft;
				}
				inputState.RemoveOnKeyDownListener(cycleRight, action);
				inputState.AddOnKeyDownListener(cycleRight, action);
				inputState.RemoveOnKeyDownListener(cycleLeft, action2);
				inputState.AddOnKeyDownListener(cycleLeft, action2);
			}
		}

		public void SetData(List<HotbarItem> data)
		{
			hotbarItems = data;
			CenterHotbarIndex();
			RebuildHotbar();
		}

		public void SetData(List<HotbarItem> data, int index)
		{
			hotbarItems = data;
			CenterHotbarIndex();
			int num = Utility.PositiveModulo(index - GetCenterIndex(), hotbarItems.Count);
			for (int i = 0; i < num; i++)
			{
				hotbarItems.Shift(down: true);
			}
			RebuildHotbar();
		}

		private int GetVisibleItemCount()
		{
			AssertionCheck();
			float width = GetComponent<RectTransform>().rect.width;
			float spacing = m_hotbarItemsTransform.GetComponent<HorizontalLayoutGroup>().spacing;
			int num = Mathf.FloorToInt(width / (spacing + ItemPrefab.itemWidth));
			return Mathf.Min(useFoldout ? num : (num - 2), hotbarItems.Count);
		}

		private int GetCenterIndex()
		{
			return Mathf.FloorToInt((float)Mathf.Min(GetVisibleItemCount(), hotbarItems.Count) * 0.5f);
		}

		private void CenterHotbarIndex()
		{
			if (!useFoldout)
			{
				m_hotbarIndex = GetCenterIndex();
			}
		}

		public void SetIndex(int index)
		{
			m_hotbarIndex = index;
			UpdateHotbarItems();
		}

		private void UpdateEdgeArrows()
		{
			m_edgeArrows.SetActive(HasItemsOutsideHotbar);
		}

		public void RebuildHotbar()
		{
			AssertionCheck();
			DestroyHotbar();
			BuildHotbar();
			UpdateEdgeArrows();
		}

		private void DestroyHotbar()
		{
			for (int i = 0; i < hotbarButtons.Count; i++)
			{
				Object.DestroyImmediate(hotbarButtons[i].gameObject);
			}
			hotbarButtons.Clear();
		}

		private void BuildHotbar()
		{
			foreach (HotbarItem hotbarItem in hotbarItems)
			{
				GenerateHotbarItem(hotbarItem);
			}
			UpdateHotbarItems();
		}

		public static string ToSizeInfo(float normalizedSize)
		{
			if (normalizedSize == 0f)
			{
				return "";
			}
			if (normalizedSize < 0.2f)
			{
				return "*";
			}
			if (normalizedSize < 0.4f)
			{
				return "**";
			}
			if (normalizedSize < 0.6f)
			{
				return "***";
			}
			if (normalizedSize < 0.8f)
			{
				return "****";
			}
			return "*****";
		}

		private void GenerateHotbarItem(HotbarItem itemInfo)
		{
			HotbarButton component = Object.Instantiate(useFoldout ? m_hotbarFoldoutItemPrefab.gameObject : m_hotbarItemPrefab.gameObject, m_hotbarItemsTransform).GetComponent<HotbarButton>();
			component.Icon.sprite = itemInfo.icon;
			component.Icon.gameObject.SetActive(value: true);
			component.Name.LocaleID = itemInfo.name;
			component.hotbarItemsTransform = m_hotbarItemsTransform.GetComponent<RectTransform>();
			if (itemInfo.normalizedSize > 0f)
			{
				component.SizeInfo.text = ToSizeInfo(Mathf.Sqrt(itemInfo.normalizedSize));
			}
			hotbarButtons.Add(component);
		}

		public void InvokeHotbarButton()
		{
			if (base.gameObject.activeSelf)
			{
				hotbarItems[m_hotbarIndex].callback();
			}
		}

		private void CycleHotbar(bool right)
		{
			if (base.gameObject.activeSelf)
			{
				int num = m_hotbarIndex + (right ? 1 : (-1));
				int centerIndex = GetCenterIndex();
				int visibleItemCount = GetVisibleItemCount();
				int num2 = visibleItemCount / 2;
				if ((num < centerIndex - num2 || num > centerIndex + num2) && HasItemsOutsideHotbar)
				{
					ShiftItem(right);
				}
				else
				{
					m_hotbarIndex = num;
				}
				if (!HasItemsOutsideHotbar)
				{
					m_hotbarIndex = Utility.PositiveModulo(m_hotbarIndex, visibleItemCount);
				}
				m_hotbarIndex = Mathf.Clamp(m_hotbarIndex, 0, visibleItemCount);
				if (invokeOnCycle)
				{
					InvokeHotbarButton();
				}
				UpdateHotbarItems();
			}
		}

		private void ShiftItem(bool right)
		{
			hotbarButtons.Shift(right);
			hotbarItems.Shift(right);
			if (right)
			{
				m_hotbarItemsTransform.GetChild(0).SetAsLastSibling();
			}
			else
			{
				m_hotbarItemsTransform.GetChild(m_hotbarItemsTransform.childCount - 1).SetAsFirstSibling();
			}
		}

		private void UpdateHotbarItems()
		{
			if (hotbarItems.Count <= 1)
			{
				return;
			}
			int visibleItemCount = GetVisibleItemCount();
			for (int i = 0; i < hotbarButtons.Count; i++)
			{
				HotbarButton hotbarButton = hotbarButtons[i];
				if (hotbarButton == null)
				{
					continue;
				}
				if (i > visibleItemCount)
				{
					hotbarButton.gameObject.SetActive(value: false);
					continue;
				}
				hotbarButton.gameObject.SetActive(value: true);
				if (i == m_hotbarIndex)
				{
					hotbarButton.Select();
				}
				else
				{
					hotbarButton.Deselect();
				}
			}
		}

		public void EnableHotbar(InputState inputState)
		{
			base.gameObject.SetActive(value: true);
			UpdateHotbarItems();
			AssignInput(inputState);
		}

		public void DisableHotbar()
		{
			if ((bool)this && (bool)base.gameObject)
			{
				base.gameObject.SetActive(value: false);
			}
		}

		public string CurrentTempId()
		{
			return hotbarItems[m_hotbarIndex].temp_id;
		}
	}
}
