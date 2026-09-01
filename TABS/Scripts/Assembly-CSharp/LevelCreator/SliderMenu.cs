using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class SliderMenu : DMUIPanel
	{
		private void Start()
		{
			AssignInput(PlayerActions.Instance);
		}

		private void AssignInput(PlayerActions actions)
		{
			m_inputState.AddOnKeyDownListener(actions.m_back, delegate
			{
				DMUIManager.Instance.PopPanel();
			});
			m_inputState.AddOnKeyDownListener(actions.m_enterExitBattle, delegate
			{
				DMUIManager.Instance.PopPanel();
			});
			m_inputState.AddOnKeyDownListener(actions.m_toolToggleSliders, delegate
			{
				DMUIManager.Instance.PopPanel();
			});
			m_inputState.AddOnKeyDownListener(actions.m_accept, delegate
			{
				DMUIManager.Instance.PopPanel();
			});
		}

		public override void OnOpen()
		{
			base.OnOpen();
			LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
			Slider componentInChildren = GetComponentInChildren<Slider>();
			if (componentInChildren != null)
			{
				componentInChildren.Select();
			}
		}
	}
}
