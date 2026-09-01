using System.Collections.Generic;
using System.Linq;
using InControl;
using UnityEngine;
using UnityEngine.Events;

namespace LevelCreator
{
	public class InputState
	{
		private List<InputKey> m_keysInternal = new List<InputKey>();

		private UnityEvent m_onStateRemoved = new UnityEvent();

		private UnityEvent m_onReceiveFocus = new UnityEvent();

		private UnityEvent m_onLoseFocus = new UnityEvent();

		public string Name { get; private set; }

		public List<InputKey> Keys => m_keysInternal;

		public InputState(string name)
		{
			Name = name;
		}

		public void OnStateRemoved()
		{
			m_onStateRemoved.Invoke();
		}

		public void OnReceiveFocus()
		{
			m_onReceiveFocus.Invoke();
		}

		public void OnLoseFocus()
		{
			m_onLoseFocus.Invoke();
		}

		public InputKey GetKey(PlayerAction playerAction, bool addKeyOnFailedGet = true, string description = null, Sprite contextIcon = null)
		{
			IEnumerable<InputKey> source = m_keysInternal.Where((InputKey x) => x.playerAction == playerAction);
			if (source.Count() == 0 && addKeyOnFailedGet)
			{
				InputKey inputKey = new InputKey
				{
					playerAction = playerAction,
					description = description,
					contextIcon = contextIcon,
					onKeyDown = new UnityEvent(),
					onKeyUp = new UnityEvent()
				};
				m_keysInternal.Add(inputKey);
				return inputKey;
			}
			return source.FirstOrDefault();
		}

		public void AddOnKeyDownListener(PlayerAction playerAction, UnityAction action)
		{
			GetKey(playerAction).onKeyDown.AddListener(action);
		}

		public void AddOnKeyDownListener(PlayerAction playerAction, UnityAction action, string description)
		{
			GetKey(playerAction, addKeyOnFailedGet: true, description).onKeyDown.AddListener(action);
		}

		public void AddOnKeyDownListener(PlayerAction playerAction, UnityAction action, Sprite contextIcon)
		{
			GetKey(playerAction, addKeyOnFailedGet: true, null, contextIcon).onKeyDown.AddListener(action);
		}

		public void AddOnKeyUpListener(PlayerAction playerAction, UnityAction action)
		{
			GetKey(playerAction).onKeyUp.AddListener(action);
		}

		public void AddOnKeyUpListener(PlayerAction playerAction, UnityAction action, string description)
		{
			GetKey(playerAction, addKeyOnFailedGet: true, description).onKeyUp.AddListener(action);
		}

		public void AddOnKeyUpListener(PlayerAction playerAction, UnityAction action, Sprite contextIcon)
		{
			GetKey(playerAction, addKeyOnFailedGet: true, null, contextIcon).onKeyUp.AddListener(action);
		}

		public void AddOnStateRemovedListener(UnityAction action)
		{
			m_onStateRemoved.AddListener(action);
		}

		public void AddOnStateReceiveFocusListener(UnityAction action)
		{
			m_onReceiveFocus.AddListener(action);
		}

		public void AddOnStateLoseFocusListener(UnityAction action)
		{
			m_onLoseFocus.AddListener(action);
		}

		public void RemoveOnKeyDownListener(PlayerAction playerAction, UnityAction action)
		{
			GetKey(playerAction).onKeyDown.RemoveListener(action);
		}

		public void RemoveOnKeyUpListener(PlayerAction playerAction, UnityAction action)
		{
			GetKey(playerAction).onKeyUp.RemoveListener(action);
		}

		public void RemoveOnStateRemovedListener(UnityAction action)
		{
			m_onStateRemoved.RemoveListener(action);
		}

		public void RemoveOnStateReceiveFocusListener(UnityAction action)
		{
			m_onReceiveFocus.RemoveListener(action);
		}

		public void RemoveOnStateLoseFocusListener(UnityAction action)
		{
			m_onLoseFocus.RemoveListener(action);
		}

		public void ClearAllEvents()
		{
			foreach (InputKey item in m_keysInternal)
			{
				item.onKeyDown.RemoveAllListeners();
				item.onKeyUp.RemoveAllListeners();
			}
			m_onStateRemoved.RemoveAllListeners();
			m_onReceiveFocus.RemoveAllListeners();
			m_onLoseFocus.RemoveAllListeners();
			m_keysInternal.Clear();
		}
	}
}
