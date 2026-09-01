using System.Collections.ObjectModel;
using InControl;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class DisplayKeybinds : MonoBehaviour
	{
		private Text m_textComponent;

		private string m_displayText;

		private void Start()
		{
			m_textComponent = GetComponentInChildren<Text>();
			ReadOnlyCollection<PlayerAction> actions = PlayerActions.Instance.Actions;
			for (int i = 75; i < actions.Count; i++)
			{
				m_displayText = m_displayText + actions[i].Name + ": ";
				for (int j = 0; j < actions[i].Bindings.Count; j++)
				{
					m_displayText = m_displayText + "<b>" + actions[i].Bindings[j].Name + "</b>";
					if (j < actions[i].Bindings.Count - 1)
					{
						m_displayText += ", ";
					}
				}
				m_displayText += "\n";
			}
			m_textComponent.text = m_displayText;
		}

		private void Update()
		{
		}
	}
}
