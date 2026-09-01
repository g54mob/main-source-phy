using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LevelCreator
{
	public class CycleSelector : Selectable, IMoveHandler, IEventSystemHandler
	{
		public struct CycleSelectorOption
		{
			public object value;

			public string displayName;
		}

		[SerializeField]
		private LocalizeText m_displayText;

		private List<CycleSelectorOption> m_options = new List<CycleSelectorOption>();

		private Action<object> m_onValueChanged;

		public int Index { get; private set; }

		public void Init(List<CycleSelectorOption> options, Action<object> onValueChanged, int index = 0)
		{
			m_options = options;
			Index = index;
			m_onValueChanged = onValueChanged;
			SetIndex(Index);
		}

		public void Increment(int delta)
		{
			Index = Utility.PositiveModulo(Index + delta, m_options.Count);
			SetIndex(Index);
		}

		public void SetIndex(int index)
		{
			Index = index;
			if (m_options != null && Index < m_options.Count)
			{
				CycleSelectorOption cycleSelectorOption = m_options[index];
				if (cycleSelectorOption.value != null)
				{
					m_displayText.LocaleID = cycleSelectorOption.displayName;
					m_onValueChanged?.Invoke(cycleSelectorOption.value);
				}
			}
		}

		public void SetIndexWithoutNotify(int index)
		{
			Index = index;
			if (m_options != null && Index < m_options.Count)
			{
				CycleSelectorOption cycleSelectorOption = m_options[index];
				if (cycleSelectorOption.value != null)
				{
					m_displayText.LocaleID = cycleSelectorOption.displayName;
				}
			}
		}

		public override void OnMove(AxisEventData eventData)
		{
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				Increment(-1);
				break;
			case MoveDirection.Up:
				base.OnMove(eventData);
				break;
			case MoveDirection.Right:
				Increment(1);
				break;
			case MoveDirection.Down:
				base.OnMove(eventData);
				break;
			case MoveDirection.None:
				base.OnMove(eventData);
				break;
			default:
				base.OnMove(eventData);
				break;
			}
		}
	}
}
