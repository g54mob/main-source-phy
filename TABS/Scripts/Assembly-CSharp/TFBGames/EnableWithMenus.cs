using System.Collections.Generic;
using UnityEngine;

namespace TFBGames
{
	public class EnableWithMenus : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Enable the GameObjects when these menus are open, and disable them when the menus are closed. (These menus must implement IMenuWithEvents.)")]
		protected GameObject[] m_menus;

		[SerializeField]
		[Tooltip("Only enable the GameObjects when at least one of the menus' EnableWithMenusParameter is in this list. (This list is ignored if it is empty.)")]
		protected int[] m_paramters;

		[SerializeField]
		[Tooltip("GameObjects to enable/disable.")]
		protected GameObject[] m_gameObjects;

		private IMenuWithEvents[] m_menuEvents;

		private List<int> m_paramtersList;

		private void Awake()
		{
			m_paramtersList = new List<int>(m_paramters);
			int num = m_menus.Length;
			m_menuEvents = new IMenuWithEvents[num];
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = m_menus[i];
				m_menuEvents[i] = ((gameObject != null) ? gameObject.GetComponent<IMenuWithEvents>() : null);
			}
			SubscribeToMenuEvents(subscribe: true);
			CheckMenus();
		}

		private void OnDestroy()
		{
			SubscribeToMenuEvents(subscribe: false);
		}

		private void SubscribeToMenuEvents(bool subscribe)
		{
			int i = 0;
			for (int num = m_menuEvents.Length; i < num; i++)
			{
				IMenuWithEvents menuWithEvents = m_menuEvents[i];
				if (menuWithEvents != null)
				{
					if (subscribe)
					{
						menuWithEvents.MenuOpened += OnMenuOpened;
						menuWithEvents.MenuClosed += OnMenuClosed;
						menuWithEvents.EnableWithMenusParameterChanged += OnEnableWithMenusParameterChanged;
					}
					else
					{
						menuWithEvents.MenuOpened -= OnMenuOpened;
						menuWithEvents.MenuClosed -= OnMenuClosed;
						menuWithEvents.EnableWithMenusParameterChanged -= OnEnableWithMenusParameterChanged;
					}
				}
			}
		}

		private void OnMenuOpened(IMenuWithEvents menu)
		{
			CheckMenus();
		}

		private void OnMenuClosed(IMenuWithEvents menu)
		{
			CheckMenus();
		}

		private void OnEnableWithMenusParameterChanged(IMenuWithEvents menu, int parameter)
		{
			CheckMenus();
		}

		private void CheckMenus()
		{
			bool enable = false;
			int count = m_paramtersList.Count;
			int i = 0;
			for (int num = m_menuEvents.Length; i < num; i++)
			{
				IMenuWithEvents menuWithEvents = m_menuEvents[i];
				if (menuWithEvents != null && menuWithEvents.IsOpen && (count <= 0 || m_paramtersList.Contains(menuWithEvents.EnableWithMenusParameter)))
				{
					enable = true;
					break;
				}
			}
			EnableObjects(enable);
		}

		private void EnableObjects(bool enable)
		{
			int i = 0;
			for (int num = m_gameObjects.Length; i < num; i++)
			{
				GameObject gameObject = m_gameObjects[i];
				if (gameObject != null && gameObject.activeSelf != enable)
				{
					gameObject.SetActive(enable);
				}
			}
		}
	}
}
