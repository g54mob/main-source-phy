using UnityEngine;

namespace Landfall.TABS.UI.WinConditions
{
	public class PanelSlider : MonoBehaviour
	{
		private CodeAnimation m_codeAnimation;

		private void Awake()
		{
			m_codeAnimation = GetComponent<CodeAnimation>();
			m_codeAnimation.currentState = CodeAnimationInstance.AnimationUse.Out;
		}

		public void TweenInLeftPanel()
		{
			if (m_codeAnimation.currentState != CodeAnimationInstance.AnimationUse.Out)
			{
				m_codeAnimation.PlayOut();
			}
		}

		public void TweenInRightPanel()
		{
			if (m_codeAnimation.currentState != CodeAnimationInstance.AnimationUse.In)
			{
				m_codeAnimation.PlayIn();
			}
		}
	}
}
