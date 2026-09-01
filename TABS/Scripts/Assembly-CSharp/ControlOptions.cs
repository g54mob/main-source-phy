using System;
using System.Collections.Generic;
using System.Linq;
using InControl;
using Landfall.TABS_Input;
using UnityEngine;

public class ControlOptions : MonoBehaviour
{
	[SerializeField]
	private GameObject m_templateAction;

	[SerializeField]
	private Transform m_grid;

	[SerializeField]
	private string[] m_excludePlayerActions;

	private Dictionary<PlayerAction, UIControlAction> m_actionDict = new Dictionary<PlayerAction, UIControlAction>();

	public void SetupControls()
	{
		PlayerActionSet instance = PlayerActions.Instance;
		PlayerActionSetup(instance, "Ground", 4);
	}

	public void UpdateControls()
	{
		foreach (UIControlAction value in m_actionDict.Values)
		{
			value.ResetPlayerAction();
		}
	}

	public void ResetToDefault()
	{
		PlayerActions.Instance.Reset();
	}

	private void PlayerActionSetup(PlayerActionSet playerActions, string headerText, int excludeActions = 0)
	{
		m_actionDict.Clear();
		playerActions.ListenOptions.OnBindingFound = delegate(PlayerAction action, BindingSource binding)
		{
			if (binding.BindingSourceType != BindingSourceType.KeyBindingSource && binding.BindingSourceType != BindingSourceType.MouseBindingSource && binding.BindingSourceType != BindingSourceType.DeviceBindingSource)
			{
				return false;
			}
			if (binding == new KeyBindingSource(Key.Escape))
			{
				m_actionDict[action].ResetToggles();
				action.StopListeningForBinding();
				return false;
			}
			return true;
		};
		BindingListenOptions listenOptions = playerActions.ListenOptions;
		listenOptions.OnBindingAdded = (Action<PlayerAction, BindingSource>)Delegate.Combine(listenOptions.OnBindingAdded, (Action<PlayerAction, BindingSource>)delegate(PlayerAction action, BindingSource binding)
		{
			m_actionDict[action].ResetToggles();
			foreach (KeyValuePair<PlayerAction, UIControlAction> item in m_actionDict)
			{
				item.Value.SetPlayerAction(item.Key);
			}
		});
		BindingListenOptions listenOptions2 = playerActions.ListenOptions;
		listenOptions2.OnBindingRejected = (Action<PlayerAction, BindingSource, BindingSourceRejectionType>)Delegate.Combine(listenOptions2.OnBindingRejected, (Action<PlayerAction, BindingSource, BindingSourceRejectionType>)delegate
		{
		});
		int num = playerActions.Actions.Count - excludeActions;
		for (int num2 = 0; num2 < num; num2++)
		{
			PlayerAction playerAction = playerActions.Actions[num2];
			if (!m_excludePlayerActions.Contains(playerAction.Name))
			{
				GameObject obj = UnityEngine.Object.Instantiate(m_templateAction, m_grid, worldPositionStays: false);
				obj.SetActive(value: true);
				UIControlAction component = obj.GetComponent<UIControlAction>();
				component.SetPlayerAction(playerAction);
				component.SetLocalizedText(playerAction.Name);
				obj.name = playerAction.Name;
				m_actionDict.Add(playerAction, component);
			}
		}
	}
}
