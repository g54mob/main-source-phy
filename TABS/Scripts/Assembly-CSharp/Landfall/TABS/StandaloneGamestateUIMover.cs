using Landfall.TABS.GameState;
using UnityEngine;

namespace Landfall.TABS
{
	public class StandaloneGamestateUIMover : GameStateListener
	{
		public UIMovementAnimation m_UIAnimation;

		[SerializeField]
		private CodeAnimation m_CodeAnimator;

		public override void OnEnterBattleState()
		{
			if (m_UIAnimation != null)
			{
				m_UIAnimation.SetState(UIMovementAnimation.State.State01);
			}
			if (m_CodeAnimator != null)
			{
				m_CodeAnimator.PlayOut();
			}
		}

		public override void OnEnterPlacementState()
		{
			if (m_UIAnimation != null)
			{
				m_UIAnimation.SetState(UIMovementAnimation.State.State02);
			}
			if (m_CodeAnimator != null)
			{
				m_CodeAnimator.PlayIn();
			}
		}
	}
}
